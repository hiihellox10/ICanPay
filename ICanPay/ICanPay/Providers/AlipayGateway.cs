using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ICanPay.Providers
{
    /// <summary>
    /// 支付宝网关
    /// </summary>
    /// <remarks>
    /// 当前支付宝的实现仅支持MD5密钥。
    /// </remarks>
    public sealed class AlipayGateway : GatewayBase, IPaymentForm, IPaymentUrl
    {

        #region 私有字段

        const string payGatewayUrl = "https://mapi.alipay.com/gateway.do";
        const string emailRegexString = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        static Encoding pageEncoding = Encoding.GetEncoding("gb2312");

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化支付宝网关
        /// </summary>
        public AlipayGateway()
        {
        }


        /// <summary>
        /// 初始化支付宝网关
        /// </summary>
        /// <param name="gatewayParameterData">网关通知的数据集合</param>
        public AlipayGateway(List<GatewayParameter> gatewayParameterData)
            : base(gatewayParameterData)
        {
        }

        #endregion


        #region 属性

        public override GatewayType GatewayType
        {
            get
            {
                return GatewayType.Alipay;
            }
        }


        public override PaymentNotifyMethod PaymentNotifyMethod
        {
            get
            {
                // 通过RequestType、UserAgent来判断是否为服务器通知
                if (string.Compare(HttpContext.Current.Request.RequestType, "POST") == 0 &&
                    string.Compare(HttpContext.Current.Request.UserAgent, "Mozilla/4.0") == 0)
                {
                    return PaymentNotifyMethod.ServerNotify;
                }

                return PaymentNotifyMethod.AutoReturn;
            }
        }


        #endregion


        #region 方法

        public string BuildPaymentForm()
        {
            ValidatePaymentOrderParameter();
            InitOrderParameter();

            return GetFormHtml(payGatewayUrl);
        }


        /// <summary>
        /// 初始化订单参数
        /// </summary>
        private void InitOrderParameter()
        {
            SetGatewayParameterValue("service", "create_direct_pay_by_user");
            SetGatewayParameterValue("partner", Merchant.UserName);
            SetGatewayParameterValue("notify_url", Merchant.NotifyUrl);
            SetGatewayParameterValue("return_url", Merchant.NotifyUrl);
            SetGatewayParameterValue("sign_type", "MD5");
            SetGatewayParameterValue("subject", Order.Subject);
            SetGatewayParameterValue("out_trade_no", Order.Id);
            SetGatewayParameterValue("total_fee", Order.Amount);
            SetGatewayParameterValue("payment_type", "1");
            SetGatewayParameterValue("_input_charset", "gb2312");
            SetGatewayParameterValue("sign", GetOrderSign());    // 签名需要在最后设置，以免缺少参数。
        }


        public string BuildPaymentUrl()
        {
            ValidatePaymentOrderParameter();
            InitOrderParameter();

            return string.Format("{0}?{1}", payGatewayUrl, GetPaymentQueryString());
        }


        private string GetPaymentQueryString()
        {
            StringBuilder urlBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> item in GetSortedGatewayParameter())
            {
                urlBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
            }

            return urlBuilder.ToString().TrimEnd('&');
        }


        /// <summary>
        /// 获得用于签名的参数字符串
        /// </summary>
        private string GetSignParameter()
        {
            StringBuilder signBuilder = new StringBuilder();
            foreach(KeyValuePair<string, string> item in GetSortedGatewayParameter())
            {
                if (string.Compare("sign", item.Key) != 0 && string.Compare("sign_type", item.Key) != 0)
                {
                    signBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
                }
            }

            return signBuilder.ToString().TrimEnd('&');
        }



        /// <summary>
        /// 验证支付订单的参数设置
        /// </summary>
        private void ValidatePaymentOrderParameter()
        {
            if (string.IsNullOrEmpty(GetGatewayParameterValue("seller_email")))
            {
                throw new ArgumentNullException("seller_email", "订单缺少seller_email参数，seller_email是卖家支付宝账号的邮箱。" +
                                                "你需要使用PaymentSetting.SetGatewayParameterValue(\"seller_email\", \"yourname@email.com\")方法设置卖家支付宝账号的邮箱。");
            }

            if (!IsEmail(GetGatewayParameterValue("seller_email")))
            {
                throw new ArgumentException("Email格式不正确", "seller_email");
            }
        }


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
        /// 读取通知中的订单金额、订单编号
        /// </summary>
        private void ReadNotifyOrder()
        {
            Order.Amount = double.Parse(GetGatewayParameterValue("total_fee"));
            Order.Id = GetGatewayParameterValue("out_trade_no");
        }


        /// <summary>
        /// 是否是已成功支付的支付通知
        /// </summary>
        /// <returns></returns>
        private bool IsSuccessResult()
        {
            if(ValidateNotifyParameter() && ValidateNotifySign())
            {
                if(ValidateNotifyId())
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 检查支付通知，是否支付成功，签名是否正确。
        /// </summary>
        /// <returns></returns>
        private bool ValidateNotifyParameter()
        {
            // 支付状态是否为成功。
            // TRADE_FINISHED（普通即时到账的交易成功状态）
            // TRADE_SUCCESS（开通了高级即时到账或机票分销产品后的交易成功状态）
            if (string.Compare(GetGatewayParameterValue("trade_status"), "TRADE_FINISHED") == 0 ||
                string.Compare(GetGatewayParameterValue("trade_status"), "TRADE_SUCCESS") == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 验证支付宝通知的签名
        /// </summary>
        private bool ValidateNotifySign()
        {
            // 验证通知的签名
            if (string.Compare(GetGatewayParameterValue("sign"), GetOrderSign()) == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 将网关参数的集合排序
        /// </summary>
        /// <param name="coll">原网关参数的集合</param>
        private SortedList<string, string> GatewayParameterDataSort(ICollection<GatewayParameter> coll)
        {
            SortedList<string, string> list = new SortedList<string, string>();
            foreach (GatewayParameter item in coll)
            {
                list.Add(item.Name, item.Value);
            }

            return list;
        }


        /// <summary>
        /// 获得订单的签名。
        /// </summary>
        private string GetOrderSign()
        {
            // 获得MD5值时需要使用GB2312编码，否则主题中有中文时会提示签名异常，并且MD5值必须为小写。
            return Utility.GetMD5(GetSignParameter() + Merchant.Key, pageEncoding).ToLower();
        }


        public override void WriteSucceedFlag()
        {
            if (PaymentNotifyMethod == PaymentNotifyMethod.ServerNotify)
            {
                HttpContext.Current.Response.Write("success");
            }
        }


        /// <summary>
        /// 验证网关的通知Id是否有效
        /// </summary>
        private bool ValidateNotifyId()
        {
            // 浏览器自动返回的通知Id会在验证后1分钟失效，
            // 服务器异步通知的通知Id则会在输出标志成功接收到通知的success字符串后失效。
            if (string.Compare(Utility.ReadPage(GetValidateNotifyUrl()), "true") == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 获得验证支付宝通知的Url
        /// </summary>
        private string GetValidateNotifyUrl()
        {
            return string.Format("{0}?service=notify_verify&partner={1}&notify_id={2}", payGatewayUrl, Merchant.UserName,
                                 GetGatewayParameterValue("notify_id"));
        }


        /// <summary>
        /// 是否是正确格式的Email地址
        /// </summary>
        /// <param name="emailAddress">Email地址</param>
        private bool IsEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }

            return Regex.IsMatch(emailAddress, emailRegexString);
        }

        #endregion

    }
}