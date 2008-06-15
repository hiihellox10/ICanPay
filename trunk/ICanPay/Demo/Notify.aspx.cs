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
            // �̻����ʺŸ���Կ
            Merchant yeepayMerchant = new Merchant();
            yeepayMerchant.UserName = "10000432521";
            yeepayMerchant.Key = "8UPp0KE8sq73zVP370vko7C39403rtK1YwX40Td6irH216036H27Eb12792t";

            Merchant chinabankMerchat = new Merchant();
            chinabankMerchat.UserName = "00000000000";
            chinabankMerchat.Key = "0000000000000000000000000000000000000000000";

            PaymentNotify paymentNotify = PayGatewayFactory.GetGatewayNotify();

            // û���յ���ʶ�������֪ͨ
            if (paymentNotify.PayGateway == null)
            {
                return;
            }

            // ʹ����Ӧ���ص��̻���������֤֪ͨ�����Ƿ���ȷ
            switch (paymentNotify.PayGateway.GatewayName)
            {
                case GatewayType.ChinaBank:
                    paymentNotify.PayGateway.Merchant = chinabankMerchat;

                    // �����֪��������������IP���������ǿ��ȫ�ԡ�
                    // ����������֤����֪ͨ��������IP��ַ�����������ķ�����IP��ַ��
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
                double amount = paymentNotify.PayGateway.Order.Amount;
                string orderId = paymentNotify.PayGateway.Order.OrderId;

                // �ж�amount��orderId�����ݿⶩ����¼�Ƿ���ϣ��ٴ����
            }
        }
    }
}
