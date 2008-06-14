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
using com.yeepay;
public partial class QueryOrderStatus : System.Web.UI.Page
{
    public string merchantId;
    public string keyValue;
    public string orderId;

    protected void Page_Load(object sender, EventArgs e)
    {
        merchantId = "10000432521";                                     // 商家ID
        keyValue = "8UPp0KE8sq73zVP370vko7C39403rtK1YwX40Td6irH216036H27Eb12792t";                                           // 商家密钥

        orderId = Request.Form["orderId"];  // 商家的交易定单号

        try
        {
            QueryOrdResult queryOrdResult = Buy.QueryOrder(merchantId, keyValue, orderId);	// 查询订单

            if (queryOrdResult.ReturnCode == "1")
            {
                Response.Write("查询成功!");
                Response.Write("<br>订单号:" + queryOrdResult.ReturnOrder);
                Response.Write("<br>商品ID:" + queryOrdResult.ReturnPid);
                Response.Write("<br>订单金额:" + queryOrdResult.ReturnAmt);
                Response.Write("<br>商户扩展信息:" + queryOrdResult.ReturnMP);
                Response.Write("<br>订单状态:" + queryOrdResult.ReturnPayStatus);
                Response.Write("<br>已退款次数:" + queryOrdResult.ReturnRefundCount);
                Response.Write("<br>已退款金额:" + queryOrdResult.ReturnRefundAmt);
            }
            else
            {
                Response.Write("<br>查询失败!");
            }
        }

        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}
