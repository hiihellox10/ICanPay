using System;
using ICanPay;
using ICanPay.Providers;

namespace Demo
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateYeepayOrder();
        }


        /// <summary>
        /// 创建易宝的支付订单
        /// </summary>
        private void CreateYeepayOrder()
        {
            PaymentSetting<TenpayGateway> setting = new PaymentSetting<TenpayGateway>();
            setting.Merchant.UserName = "0000000000";
            setting.Merchant.Key = "00000000000000000000000000";
            setting.Merchant.NotifyUrl = new Uri("http://yousite.com/Payment/PaymentNotify.aspx");

            setting.Order.Amount = 0.01;
            setting.Order.OrderId = "9";

            setting.Payment();
        }

        /// <summary>
        /// 创建支付宝的支付订单
        /// </summary>
        private void CreateAlipayOrder()
        {
            PaymentSetting<AlipayGateway> setting = new PaymentSetting<AlipayGateway>();
            setting.SetGatewayParameterValue("seller_email", "name@address.com");
            setting.Merchant.UserName = "0000000000";
            setting.Merchant.Key = "00000000000000000000000000";
            setting.Merchant.NotifyUrl = new Uri("http://yousite.com/Payment/PaymentNotify.aspx");

            setting.Order.Amount = 0.01;
            setting.Order.OrderId = "10";

            setting.Payment();
        }

    }
}
