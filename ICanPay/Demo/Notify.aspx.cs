using ICanPay;
using System;
using System.Collections.Generic;

namespace Demo
{
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 设置商户数据
            Merchant alipayMerchant = new Merchant();
            alipayMerchant.GatewayType = GatewayType.Alipay;
            alipayMerchant.UserName = "000000000000000";
            alipayMerchant.Key = "000000000000000000000000000000000000000000";

            Merchant yeepayMerchant = new Merchant();
            yeepayMerchant.GatewayType = GatewayType.Yeepay;
            yeepayMerchant.UserName = "000000000000000";
            yeepayMerchant.Key = "000000000000000000000000000000000000000000";

            Merchant tenpayMerchant = new Merchant();
            tenpayMerchant.GatewayType = GatewayType.Tenpay;
            tenpayMerchant.UserName = "000000000000000";
            tenpayMerchant.Key = "000000000000000000000000000000000000000000";

            Merchant weChatPaymentMerchant = new Merchant();
            weChatPaymentMerchant.GatewayType = GatewayType.WeChatPayment;
            weChatPaymentMerchant.UserName = "000000000000000";
            weChatPaymentMerchant.Key = "000000000000000000000000000000000000000000";

            // 添加到商户数据集合
            List<Merchant> merchantList = new List<Merchant>();
            merchantList.Add(alipayMerchant);
            merchantList.Add(yeepayMerchant);
            merchantList.Add(tenpayMerchant);
            merchantList.Add(weChatPaymentMerchant);

            // 订阅支付通知事件
            PaymentNotify notify = new PaymentNotify(merchantList);
            notify.PaymentSucceed += new PaymentSucceedEventHandler(notify_PaymentSucceed);
            notify.PaymentFailed += new PaymentFailedEventHandler(notify_PaymentFailed);
            notify.UnknownGateway += new UnknownGatewayEventHandler(notify_UnknownGateway);

            // 接收并处理支付通知
            notify.Received();
        }

        private void notify_PaymentSucceed(object sender, PaymentSucceedEventArgs e)
        {
            // 支付成功时时的处理代码
            if (e.PaymentNotifyMethod == PaymentNotifyMethod.AutoReturn)
            {
                // 当前是用户的浏览器自动返回时显示充值成功页面
            }
        }

        private void notify_PaymentFailed(object sender, PaymentFailedEventArgs e)
        {
            // 支付失败时的处理代码
        }

        private void notify_UnknownGateway(object sender, UnknownGatewayEventArgs e)
        {
            // 无法识别支付网关时的处理代码
        }
    }
}
