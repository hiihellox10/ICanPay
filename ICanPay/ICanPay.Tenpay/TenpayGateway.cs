using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ICanPay.Core;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif


namespace ICanPay.Tenpay
{
    /// <summary>
    /// 财付通网关
    /// </summary>
    public sealed class TenpayGateway : GatewayBase, IPaymentUrl, IPaymentForm, IQueryNow
    {

        #region 私有字段

        const string payGatewayUrl = "https://gw.tenpay.com/gateway/pay.htm";
        const string verifyNotifyGatewayUrl = "https://gw.tenpay.com/gateway/verifynotifyid.xml";
        const string queryGatewayUrl = "https://gw.tenpay.com/gateway/normalorderquery.xml";
        static Encoding pageEncoding = Encoding.GetEncoding("GB2312");

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化财付通网关
        /// </summary>
        public TenpayGateway()
        {
        }


        /// <summary>
        /// 初始化财付通网关
        /// </summary>
        /// <param name="gatewayParameterData">网关通知的数据集合</param>
        public TenpayGateway(List<GatewayParameter> gatewayParameterData)
            : base(gatewayParameterData)
        {
        }

        #endregion


        #region 属性

        /// <summary>
        /// 网关名称
        /// </summary>
        public override GatewayType GatewayType
        {
            get
            {
                return GatewayType.Tenpay;
            }
        }


        public override PaymentNotifyMethod PaymentNotifyMethod
        {
            get
            {
#if NET35
                string requestType = HttpContext.Current.Request.RequestType;
                string userAgent = HttpContext.Current.Request.UserAgent;
#elif NETSTANDARD2_0
                string requestType = HttpContext.Current.Request.Headers["RequestType"];
                string userAgent = HttpContext.Current.Request.Headers["UserAgent"];
#endif

                // 通过RequestType、UserAgent来判断是否为服务器通知
                if (string.Compare(requestType, "GET") == 0 &&
                    string.IsNullOrEmpty(userAgent))
                {
                    return PaymentNotifyMethod.ServerNotify;
                }

                return PaymentNotifyMethod.AutoReturn;
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 支付订单数据的Url
        /// </summary>
        public string BuildPaymentUrl()
        {
            InitOrderParameter();
            return string.Format("{0}?{1}", payGatewayUrl, GetPaymentQueryString());
        }


        public string BuildPaymentForm()
        {
            InitOrderParameter();
            return GetFormHtml(payGatewayUrl);
        }


        /// <summary>
        /// 初始化订单参数
        /// </summary>
        private void InitOrderParameter()
        {
            SetGatewayParameterValue("body", Order.Subject);
            SetGatewayParameterValue("fee_type", "1");
            SetGatewayParameterValue("notify_url", Merchant.NotifyUrl);
            SetGatewayParameterValue("out_trade_no", Order.Id);
            SetGatewayParameterValue("partner", Merchant.UserName);
            SetGatewayParameterValue("return_url", Merchant.NotifyUrl);
#if NET35
            SetGatewayParameterValue("spbill_create_ip", HttpContext.Current.Request.UserHostAddress);
#elif NETSTANDARD2_0
            SetGatewayParameterValue("spbill_create_ip", HttpContext.Current.Request.Host.Value);
#endif
            SetGatewayParameterValue("total_fee", Order.Amount * 100);
            SetGatewayParameterValue("input_charset", "GBK");
            SetGatewayParameterValue("sign", GetOrderSign());    // 签名需要在最后设置，以免缺少参数。
        }


        private string GetPaymentQueryString()
        {
            StringBuilder url = new StringBuilder();
            foreach (GatewayParameter item in GatewayParameterData)
            {
                url.AppendFormat("{0}={1}&", item.Name, item.Value);
            }

            return url.ToString().TrimEnd('&');
        }


        private string GetOrderSign()
        {
            StringBuilder signParameterBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> item in GetSortedGatewayParameter())
            {
                // 空值的参数跟sign签名参数不参加签名
                if (!string.IsNullOrEmpty(item.Value) && string.Compare(item.Key, "sign") != 0)
                {
                    signParameterBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
                }
            }

            // 获得MD5值时需要使用GB2312编码，否则主题中有中文时会提示签名异常
            return Utility.GetMD5(signParameterBuilder.Append("key=" + Merchant.Key).ToString(), pageEncoding);
        }


        /// <summary>
        /// 验证订单是否支付成功
        /// </summary>
        /// <remarks>这里处理查询订单的网关通知跟支付订单的网关通知</remarks>
        protected override bool CheckNotifyData()
        {
            if (IsSuccessResult())
            {
                ReadNotifyOrder();
                return true;
            }

            return false;
        }


        /// <summary>
        /// 是否是已成功支付的支付通知
        /// </summary>
        /// <returns></returns>
        private bool IsSuccessResult()
        {
            if (ValidateNotifyParameter())
            {
                if (ValidateNotifyId())
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 检查支付通知，是否支付成功，货币类型是否为RMB，签名是否正确。
        /// </summary>
        /// <returns></returns>
        private bool ValidateNotifyParameter()
        {
            if (string.Compare(GetGatewayParameterValue("trade_state"), "0") == 0 &&
                string.Compare(GetGatewayParameterValue("trade_mode"), "1") == 0 &&
                string.Compare(GetGatewayParameterValue("fee_type"), "1") == 0 &&
                string.Compare(GetGatewayParameterValue("sign"), GetOrderSign()) == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 读取通知中的订单金额、订单编号
        /// </summary>
        private void ReadNotifyOrder()
        {
            Order.Amount = Convert.ToDouble(GetGatewayParameterValue("total_fee")) * 0.01;
            Order.Id = GetGatewayParameterValue("out_trade_no");
        }



        public override void WriteSucceedFlag()
        {
            if (PaymentNotifyMethod == PaymentNotifyMethod.ServerNotify)
            {
                string success = "success";
#if NET35
                HttpContext.Current.Response.Write(success);
#elif NETSTANDARD2_0
                HttpContext.Current.Response.WriteAsync(success).GetAwaiter();
#endif
            }
        }


        /// <summary>
        /// 验证通知Id
        /// </summary>
        /// <returns></returns>
        private bool ValidateNotifyId()
        {
            string resultXml = Utility.ReadPage(GetValidateNotifyUrl(), pageEncoding);
            // 需要先备份并清除之前接收到的网关的通知的数据，否者会对数据的验证造成干扰。
            List<GatewayParameter> gatewayParameterData = BackupAndClearGatewayParameter();
            ReadResultXml(resultXml);
            bool result = ValidateNotifyParameter();
            RestoreGatewayParameter(gatewayParameterData);   // 验证通知Id后还原之前的通知的数据。

            return result;
        }


        /// <summary>
        /// 验证订单金额、订单号是否与之前的通知的金额、订单号相符
        /// </summary>
        /// <returns></returns>
        private bool ValidateOrder()
        {
            if (Order.Amount == Convert.ToDouble(GetGatewayParameterValue("total_fee")) * 0.01 &&
               string.Compare(Order.Id, GetGatewayParameterValue("out_trade_no")) == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 获得验证通知的URL
        /// </summary>
        /// <returns></returns>

        private string GetValidateNotifyUrl()
        {
            return string.Format("{0}?{1}&sign={2}", verifyNotifyGatewayUrl, GetValidateNotifyQueryString(),
                                 Utility.GetMD5(GetValidateNotifyQueryString() + "&key=" + Merchant.Key, pageEncoding));
        }


        /// <summary>
        /// 获得验证通知的查询字符串
        /// </summary>
        /// <returns></returns>
        private string GetValidateNotifyQueryString()
        {
            return string.Format("notify_id={0}&partner={1}", GetGatewayParameterValue("notify_id"), Merchant.UserName);
        }


        /// <summary>
        /// 读取结果的XML
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private void ReadResultXml(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(xml);
            }
            catch (XmlException) { }

            foreach (XmlNode rootNode in xmlDocument.ChildNodes)
            {
                foreach (XmlNode item in rootNode.ChildNodes)
                {
                    SetGatewayParameterValue(item.Name, item.InnerText);
                }
            }
        }


        /// <summary>
        /// 备份并清除网关的参数
        /// </summary>
        private List<GatewayParameter> BackupAndClearGatewayParameter()
        {
            List<GatewayParameter> gatewayParameterData = new List<GatewayParameter>(GatewayParameterData);
            GatewayParameterData.Clear();
            return gatewayParameterData;
        }


        /// <summary>
        /// 还原网关的参数
        /// </summary>
        /// <param name="gatewayParameterData">网关的数据的集合</param>
        private void RestoreGatewayParameter(List<GatewayParameter> gatewayParameterData)
        {
            foreach (GatewayParameter item in gatewayParameterData)
            {
                SetGatewayParameterValue(item.Name, item.Value, item.RequestMethod);
            }
        }


        public bool QueryNow()
        {
            ReadResultXml(Utility.ReadPage(GetQueryOrderUrl(), pageEncoding));
            if (ValidateNotifyParameter() && ValidateOrder())
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 获得查询订单的Url
        /// </summary>
        /// <returns></returns>
        private string GetQueryOrderUrl()
        {
            return string.Format("{0}?{1}&sign={2}", queryGatewayUrl, GetQueryOrderQueryString(),
                                 Utility.GetMD5(GetQueryOrderQueryString() + "&key=" + Merchant.Key, pageEncoding));
        }


        /// <summary>
        /// 获得查询订单的查询字符串
        /// </summary>
        /// <returns></returns>
        private string GetQueryOrderQueryString()
        {
            return string.Format("out_trade_no={0}&partner={1}", Order.Id, Merchant.UserName);
        }


        #endregion

    }
}
