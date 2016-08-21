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
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "0000000000000000000000000000000000000000";
            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
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
            paymentSetting.SetGatewayParameterValue("appid", "wx000000000000000");
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "0000000000000000000000000000000000000000";
            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            paymentSetting.Order.Id = "20";
            paymentSetting.Order.Amount = 0.01;

            if (paymentSetting.CanQueryNow && paymentSetting.QueryNow())
            {
                // 订单已支付
            }
        }


        /// <summary>
        /// 查询财付通的订单支付状态
        /// </summary>
        private void QueryTenpayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Tenpay);
            paymentSetting.Merchant.UserName = "000000000000000";
            paymentSetting.Merchant.Key = "0000000000000000000000000000000000000000";
            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            paymentSetting.Order.Id = "885";
            paymentSetting.Order.Amount = 0.01;

            if (paymentSetting.CanQueryNow && paymentSetting.QueryNow())
            {
                // 订单已支付
            }
        }

    }
}
