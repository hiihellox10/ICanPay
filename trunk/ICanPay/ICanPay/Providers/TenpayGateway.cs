using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ICanPay.Providers
{
    /// <summary>
    /// 财付通网关
    /// </summary>
    /// <remarks>
    /// 当前财付通的实现只提供了即时到帐功能
    /// </remarks>
    public sealed class TenpayGateway : PayGateway, IPaymentUrl
    {

        #region 私有字段

        const string payGatewayUrl = @"http://service.tenpay.com/cgi-bin/v3.0/payservice.cgi";

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
        public TenpayGateway(ICollection<GatewayParameter> gatewayParameterData)
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
                // 通过RequestType、UserAgent来判断是否为服务器通知
                if (string.Compare(HttpContext.Current.Request.RequestType, "GET") == 0 &&
                    string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
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
            if (Order.OrderId.Length > 10)
            {
                throw new ArgumentException("订单编号必须少于10位数", "OrderId");
            }

            if (!Utility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("订单编号只能是数字", "OrderId");
            }

            StringBuilder url = new StringBuilder();
            url.Append(payGatewayUrl + "?");
            url.Append("cmdno=1");
            url.Append("&date=" + DateTime.Now.ToString("yyyyMMdd"));
            url.Append("&bank_type=0");
            url.Append("&desc=" + Order.OrderId);
            url.Append("&purchaser_id=");
            url.Append("&bargainor_id=" + Merchant.UserName);
            url.Append("&transaction_id=" + Merchant.UserName + DateTime.Now.ToString("yyyyMMdd") + Order.OrderId.PadLeft(10, '0'));
            url.Append("&sp_billno=" + Order.OrderId);
            url.Append("&total_fee=" + Order.Amount * 100);
            url.Append("&fee_type=1");
            url.Append("&return_url=" + Merchant.NotifyUrl);
            url.Append("&attach=");
            url.Append("&spbill_create_ip=" + HttpContext.Current.Request.UserHostAddress);
            url.Append("&sign=" + PaySign());

            return url.ToString();
        }


        /// <summary>
        /// 支付订单的签名
        /// </summary>
        private string PaySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append("cmdno=1");
            sign.Append("&date=" + DateTime.Now.ToString("yyyyMMdd"));
            sign.Append("&bargainor_id=" + Merchant.UserName);
            sign.Append("&transaction_id=" + Merchant.UserName + DateTime.Now.ToString("yyyyMMdd") + Order.OrderId.PadLeft(10, '0'));
            sign.Append("&sp_billno=" + Order.OrderId);
            sign.Append("&total_fee=" + Order.Amount * 100);
            sign.Append("&fee_type=1");
            sign.Append("&return_url=" + Merchant.NotifyUrl);
            sign.Append("&attach=");
            sign.Append("&spbill_create_ip=" + HttpContext.Current.Request.UserHostAddress);
            sign.Append("&key=" + Merchant.Key);

            return Utility.MD5(sign.ToString()).ToUpper();
        }


        /// <summary>
        /// 验证订单是否支付成功
        /// </summary>
        /// <remarks>这里处理查询订单的网关通知跟支付订单的网关通知</remarks>
        protected override bool CheckNotifyData()
        {
            // 检查通知签名是否正确、是否支付成功，货币类型是否为RMB
            if (string.Compare(GetGatewayParameterValue("sign"), NotifySign()) == 0 &&
                string.Compare(GetGatewayParameterValue("fee_type"), "1") == 0 &&
                string.Compare(GetGatewayParameterValue("pay_result"), "0") == 0 &&
                string.Compare(GetGatewayParameterValue("pay_info"), "OK") == 0)
            {
                Order.Amount = Convert.ToDouble(GetGatewayParameterValue("total_fee")) * 0.01;
                Order.OrderId = GetGatewayParameterValue("sp_billno");

                return true;
            }

            return false;
        }


        /// <summary>
        /// 服务器支付通知签名
        /// </summary>
        private string NotifySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append("cmdno=" + GetGatewayParameterValue("cmdno"));
            sign.Append("&pay_result=" + GetGatewayParameterValue("pay_result"));
            sign.Append("&date=" + GetGatewayParameterValue("date"));
            sign.Append("&transaction_id=" + GetGatewayParameterValue("transaction_id"));
            sign.Append("&sp_billno=" + GetGatewayParameterValue("sp_billno"));
            sign.Append("&total_fee=" + GetGatewayParameterValue("total_fee"));
            sign.Append("&fee_type=" + GetGatewayParameterValue("fee_type"));
            sign.Append("&attach=" + GetGatewayParameterValue("attach"));
            sign.Append("&key=" + Merchant.Key);

            return Utility.MD5(sign.ToString());
        }


        protected override void WriteSucceedFlag()
        {
            if (PaymentNotifyMethod == PaymentNotifyMethod.ServerNotify)
            {
                StringBuilder flag = new StringBuilder();
                flag.AppendLine("<html>");
                flag.AppendLine("<head>");
                flag.AppendLine("<meta name=\"TENCENT_ONELINE_PAYMENT\" content=\"China TENCENT\">");
                flag.AppendLine("<script language=javascript>");
                flag.AppendLine(string.Format("window.location.href='http://{0}';", System.Web.HttpContext.Current.Request.Url));
                flag.AppendLine("</script>");
                flag.AppendLine("</head>");
                flag.AppendLine("<body></body>");
                flag.AppendLine("</html>");

                System.Web.HttpContext.Current.Response.Write(flag.ToString());
            }
        }

        #endregion

    }
}