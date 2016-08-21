using ICanPay;
using System;

namespace Demo
{
    public partial class QueryPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryYeepayOrder();
        }


        /// <summary>
        /// 查询易宝的订单支付状态
        /// </summary>
        private void QueryYeepayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Yeepay);
            paymentSetting.Merchant.UserName = "10000000000";
            paymentSetting.Merchant.Key = "0000000000000000000000000000000000000000";
            paymentSetting.Order.Id = "1564515";
            paymentSetting.Order.Amount = 0.01;

            if (paymentSetting.CanQueryNow && paymentSetting.QueryNow())
            {
                // 订单已支付
            }
        }

        /// <summary>
        /// 查询微信的订单支付状态
        /// </summary>
        private void QueryWeChatPaymentOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.WeChatPayment);
            paymentSetting.SetGatewayParameterValue("appid", "wx8340ff249a2941bd");
            paymentSetting.Merchant.UserName = "1358853202";
            paymentSetting.Merchant.Key = "31b43e9966f05e3216bbdbd154fc34d1";
            paymentSetting.Order.Id = "20";
            paymentSetting.Order.Amount = 0.01;

            if (paymentSetting.CanQueryNow && paymentSetting.QueryNow())
            {
                // 订单已支付
                Response.Write("成功");
            }
        }
    }
}
