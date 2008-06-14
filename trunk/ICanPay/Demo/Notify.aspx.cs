using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ICanPay;

namespace Demo
{
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetNotify();
        }

        private void GetNotify()
        {
            // 商户的帐号跟密钥
            Merchant yeepayMerchant = new Merchant();
            yeepayMerchant.UserName = "10000432521";
            yeepayMerchant.Key = "8UPp0KE8sq73zVP370vko7C39403rtK1YwX40Td6irH216036H27Eb12792t";

            Merchant chinabankMerchat = new Merchant();
            chinabankMerchat.UserName = "00000000000";
            chinabankMerchat.Key = "0000000000000000000000000000000000000000000";

            PaymentNotify paymentNotify = PayGatewayFactory.GetGatewayNotify();

            // 没有收到可识别的网关通知
            if (paymentNotify.PayGateway == null)
            {
                return;
            }

            // 使用相应网关的商户数据以验证通知数据是否正确
            switch (paymentNotify.PayGateway.GatewayName)
            {
                case GatewayType.ChinaBank:
                    paymentNotify.PayGateway.Merchant = chinabankMerchat;

                    // 如果你知道服务器的网关IP，并且想加强安全性。
                    // 可以设置验证发送通知服务器的IP地址，并添加允许的服务器IP地址。
                    paymentNotify.PayGateway.ValidateNotifyHostServerAddress = true;
                    paymentNotify.PayGateway.SafeAddress.Add("129.1.12.52");
                    paymentNotify.PayGateway.SafeAddress.Add("129.1.12.51");
                    paymentNotify.PayGateway.SafeAddress.Add("129.1.12.50");
                    break;
                case GatewayType.YeePay:
                    paymentNotify.PayGateway.Merchant = yeepayMerchant;
                    break;
            }

            if (paymentNotify.HasNotify)
            {
                Response.Write("yes");
                double amount = paymentNotify.PayGateway.Order.Amount;
                string orderId = paymentNotify.PayGateway.Order.OrderId;

                // 判断amount、orderId跟数据库订单记录是否符合，再处理付款。
            }
        }
    }
}
