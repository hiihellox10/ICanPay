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
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000";
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "24";
            paymentSetting.Order.Subject = "测试易宝";

            paymentSetting.Payment();
        }


        private void CreateTenpayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Tenpay);
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000";
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
            paymentSetting.SetGatewayParameterValue("seller_email", "youremail@email.com");
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000";
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "35";
            paymentSetting.Order.Subject = "测测看支付宝";

            paymentSetting.Payment();
        }


        /// <summary>
        /// 创建微信的支付订单
        /// </summary>
        private void CreateWeChatPaymentOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.WeChatPayment);
            paymentSetting.SetGatewayParameterValue("appid", "wx000000000000000");
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "000000000000000000000000000000000000000000";
            paymentSetting.Merchant.NotifyUrl = new Uri("http://yourwebsite.com/Notify.aspx");

            paymentSetting.Order.Amount = 0.01;
            paymentSetting.Order.Id = "31";
            paymentSetting.Order.Subject = "测测看微信";

            paymentSetting.Payment();
        }

    }
}
