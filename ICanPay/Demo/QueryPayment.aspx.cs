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
            PaymentSetting querySetting = new PaymentSetting(GatewayType.Yeepay);
            querySetting.Merchant.UserName = "000000000000000";
            querySetting.Merchant.Key = "0000000000000000000000000000000000000000";

            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            querySetting.Order.Id = "1564515";
            querySetting.Order.Amount = 0.01;

            if (querySetting.CanQueryNow && querySetting.QueryNow())
            {
                // 订单已支付
            }
        }


        /// <summary>
        /// 查询微信的订单支付状态
        /// </summary>
        private void QueryWeChatPayOrder()
        {
            PaymentSetting querySetting = new PaymentSetting(GatewayType.WeChatPay);
            querySetting.SetGatewayParameterValue("appid", "wx000000000000000");
            querySetting.Merchant.UserName = "000000000000000";
            querySetting.Merchant.Key = "0000000000000000000000000000000000000000";

            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            querySetting.Order.Id = "20";
            querySetting.Order.Amount = 0.01;

            if (querySetting.CanQueryNow && querySetting.QueryNow())
            {
                // 订单已支付
            }
        }


        /// <summary>
        /// 查询财付通的订单支付状态
        /// </summary>
        private void QueryTenpayOrder()
        {
            PaymentSetting querySetting = new PaymentSetting(GatewayType.Tenpay);
            querySetting.Merchant.UserName = "000000000000000";
            querySetting.Merchant.Key = "0000000000000000000000000000000000000000";

            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            querySetting.Order.Id = "885";
            querySetting.Order.Amount = 0.01;

            if (querySetting.CanQueryNow && querySetting.QueryNow())
            {
                // 订单已支付
            }
        }

    }
}
