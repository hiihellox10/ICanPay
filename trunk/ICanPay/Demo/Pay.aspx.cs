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
using ICanPay.Providers;

namespace Demo
{
    public partial class Pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 设置付款信息
            PaymentPostData<YeepayGateway> po = new PaymentPostData<YeepayGateway>();
            po.PayGateway.Merchant.UserName = "10000432521";
            po.PayGateway.Merchant.Key = "8UPp0KE8sq73zVP370vko7C39403rtK1YwX40Td6irH216036H27Eb12792t";
            po.PayGateway.Merchant.NotifyUrl = "http://yousite.com/Notify.aspx";
            po.PayGateway.Order.OrderId = "1564515";
            po.PayGateway.Order.Amount = 0.01;
            po.PayGateway.Customer.Name = "name";
            po.PayGateway.Customer.Email = "name@address.com";
            po.PayGateway.Customer.Address = "my address"; ;
            po.PayGateway.Customer.Post = "4564565";
            po.PayGateway.Customer.Telephone = "1568847888";

            // 创建付款的Url
            Response.Write(po.PayGateway.BuildPaymentUrl());
        }
    }
}
