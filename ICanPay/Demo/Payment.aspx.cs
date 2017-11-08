using ICanPay;
using System;

namespace Demo
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateAlipayOrder();
        }


        /// <summary>
        /// 创建易宝的支付订单
        /// </summary>
        private void CreateYeepayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Yeepay);
            paymentSetting.Merchant.UserName = "000000000000000";   // 商户编号
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000"; // 商户密钥
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "24";
            paymentSetting.Order.Subject = "测试易宝";

            paymentSetting.Payment();
        }


        private void CreateTenpayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Tenpay);
            paymentSetting.Merchant.UserName = "000000000000000";   // 商户号
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000"; // 密钥
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "93";
            paymentSetting.Order.Subject = "测测看";

            paymentSetting.Payment();
        }


        /// <summary>
        /// 创建支付宝的支付订单
        /// </summary>
        private void CreateAlipayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Alipay);
            paymentSetting.SetGatewayParameterValue("seller_email", "yourname@address.com");    // 支付宝的注册邮箱
            paymentSetting.Merchant.UserName = "000000000000000";   // 合作伙伴身份（PID）
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000"; // MD5密钥
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "35";
            paymentSetting.Order.Subject = "测测看支付宝";

            paymentSetting.Payment();
        }


        /// <summary>
        /// 创建微信的支付订单
        /// </summary>
        private void CreateWeChatPayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.WeChatPay);
            paymentSetting.SetGatewayParameterValue("appid", "wx000000000000000");  // 公众号APPID
            paymentSetting.Merchant.UserName = "000000000000000";   // 微信支付商户号
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000"; // API密钥
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "31";
            paymentSetting.Order.Subject = "测测看微信";

            paymentSetting.Payment();
        }

    }
}
