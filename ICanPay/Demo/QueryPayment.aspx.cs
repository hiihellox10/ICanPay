using System;
using ICanPay;
using ICanPay.Providers;

namespace Demo
{
    public partial class QueryPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryChinabankOrder();
        }


        /// <summary>
        /// 查询网银在线的订单支付状态
        /// </summary>
        private void QueryChinabankOrder()
        {
            PaymentSetting<ChinabankGateway> setting = new PaymentSetting<ChinabankGateway>();
            setting.Merchant.UserName = "10000000000";
            setting.Merchant.Key = "0000000000000000000000000000000000000000";
            setting.Merchant.NotifyUrl = new Uri("http://yousite.com/Payment/PaymentNotify.aspx");
            setting.Order.OrderId = "1564515";

            // 查询结果将跟支付通知一样的形式返回
            setting.Query();
        }


        /// <summary>
        /// 查询易宝的订单支付状态
        /// </summary>
        private void QueryYeepayOrder()
        {
            PaymentSetting<YeepayGateway> ps = new PaymentSetting<YeepayGateway>();
            ps.Merchant.UserName = "10000000000";
            ps.Merchant.Key = "0000000000000000000000000000000000000000";
            ps.Order.OrderId = "1564515";
            ps.Order.Amount = 0.01;

            if (ps.Gateway.QueryPayment())
            {
                // 订单已支付
            }
        }


    }
}
