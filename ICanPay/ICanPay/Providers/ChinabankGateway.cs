using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ICanPay.Providers
{
    /// <summary>
    /// 网银在线网关
    /// </summary>
    /// <remarks>
    /// 网银在线的服务器通知需要联系他们设置接收支付通知的地址才能使用。
    /// </remarks>
    public sealed class ChinabankGateway : PayGateway, IPaymentForm, IQueryForm
    {

        #region 私有字段

        const string payGatewayUrl = @"https://pay3.chinabank.com.cn/PayGate";
        const string queryGatewayUrl = @"https://pay3.chinabank.com.cn/receiveorder.jsp";
        const string orderIdRegexString = @"^[a-zA-Z_\-0-9#$():;,.]+$";

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化网银在线网关
        /// </summary>
        public ChinabankGateway()
        {
        }

        /// <summary>
        /// 初始化网银在线网关
        /// </summary>
        /// <param name="gatewayParameterData">网关通知的数据集合</param>
        public ChinabankGateway(ICollection<GatewayParameter> gatewayParameterData)
            : base(gatewayParameterData)
        {
        }

        #endregion


        #region 属性

        /// <summary>
        /// 网关名
        /// </summary>
        public override GatewayType GatewayType
        {
            get
            {
                return GatewayType.Chinabank;
            }
        }


        public override PaymentNotifyMethod PaymentNotifyMethod
        {
            get
            {
                // 网银在线的服务器通知需要联系他们设置接收支付通知的地址才能使用。
                if (string.Compare(HttpContext.Current.Request.RequestType, "POST") == 0 &&
                    string.Compare(HttpContext.Current.Request.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)") == 0)
                {
                    return PaymentNotifyMethod.ServerNotify;
                }

                return PaymentNotifyMethod.AutoReturn;
            }
        }


        #endregion


        #region 方法

        /// <summary>
        /// 检查网关支付通知，是否支付成功
        /// </summary>
        protected override bool CheckNotifyData()
        {
            // 检查订单是否支付成功，订单签名是否正确，货币类型是否为RMB
            if (string.Compare(GetGatewayParameterValue("v_md5str"), NotifySign()) == 0 &&
                string.Compare(GetGatewayParameterValue("v_moneytype"), "CNY") == 0 &&
                string.Compare(GetGatewayParameterValue("v_pstatus"), "20") == 0 ||
                string.Compare(GetGatewayParameterValue("v_pstring"), "支付完成") == 0)
            {
                Order.Amount = Convert.ToDouble(GetGatewayParameterValue("v_amount"));
                Order.OrderId = GetGatewayParameterValue("v_oid");

                return true;
            }

            return false;
        }


        /// <summary>
        /// 通知签名
        /// </summary>
        private string NotifySign()
        {
            string[] notifyParma = { "v_oid", "v_pstatus", "v_amount", "v_moneytype" };
            string sign = GetGatewayParameterValue(notifyParma) + Merchant.Key;

            return Utility.MD5(sign).ToUpper();
        }

        /// <summary>
        /// 创建支付HTML代码
        /// </summary>
        public string BuildPaymentForm()
        {
            if (!IsRightOrderId(Order.OrderId))
            {
                throw new ArgumentException("订单只能由英文数字跟英文符号组成。", "OrderId");
            }

            Dictionary<string, string> parma = new Dictionary<string, string>();
            parma.Add("v_mid", Merchant.UserName);
            parma.Add("v_oid", Order.OrderId);
            parma.Add("v_amount", Order.Amount.ToString());
            parma.Add("v_moneytype", "CNY");
            parma.Add("v_url", Merchant.NotifyUrl.ToString());
            parma.Add("v_md5info", PaySign());

            return GetForm(parma, payGatewayUrl);
        }



        /// <summary>
        /// 支付签名
        /// </summary>
        private string PaySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append(Order.Amount);
            sign.Append("CNY");
            sign.Append(Order.OrderId);
            sign.Append(Merchant.UserName);
            sign.Append(Merchant.NotifyUrl);
            sign.Append(Merchant.Key);

            return Utility.MD5(sign.ToString()).ToUpper();
        }


        /// <summary>
        /// 是否是正确的订单编号格式。订单只能由英文数字跟_-#$():;,.符号组成。
        /// </summary>
        /// <param name="orderId">订单编号</param>
        private static bool IsRightOrderId(string orderId)
        {
            return Regex.IsMatch(orderId, orderIdRegexString);
        }


        /// <summary>
        /// 创建查询HTML代码
        /// </summary>
        public string BuildQueryForm()
        {
            if (!IsRightOrderId(Order.OrderId))
            {
                throw new ArgumentException("订单只能由英文数字跟_-#$():;,.符号组成。", "OrderId");
            }

            Dictionary<string, string> parma = new Dictionary<string, string>();
            parma.Add("v_mid", Merchant.UserName);
            parma.Add("v_oid", Order.OrderId);
            parma.Add("v_url", Merchant.NotifyUrl.ToString());
            parma.Add("billNo_md5", QuerySign());

            return GetForm(parma, queryGatewayUrl);
        }


        /// <summary>
        /// 查询签名
        /// </summary>
        private string QuerySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append(Order.OrderId);
            sign.Append(Merchant.Key);

            return Utility.MD5(sign.ToString()).ToUpper();
        }


        protected override void WriteSucceedFlag()
        {
            if (PaymentNotifyMethod == PaymentNotifyMethod.ServerNotify)
            {
                System.Web.HttpContext.Current.Response.Write("ok");
            }
        }

        #endregion

    }
}