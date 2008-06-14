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
public partial class Req : System.Web.UI.Page
{
    public static string merchantId;
    public static string keyValue;
    public string orderId;
    public string amount;
    public string cur;

    public string productId;
    public string merchantCallbackURL;
    public string addressFlag;

    public string sMctProperties;
    public string frpId;
    public string htmlCodeGet;
    public string htmlCodePost;

    protected void Page_Load(object sender, EventArgs e)
    {
        // 设置 Response编码格式为GB2312
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

        merchantId = "10000432521";                                     // 商家ID
        keyValue = "8UPp0KE8sq73zVP370vko7C39403rtK1YwX40Td6irH216036H27Eb12792t";  // 商家密钥

        // 商家设置用户购买商品的支付信息

        orderId = Request.Form["orderId"];                          // 商家的交易定单号
        amount = Request.Form["amount"];                            // 订阅金额
        cur = Request.Form["cur"];                                  // 货币单位
        productId = Request.Form["productId"];                      // 商品ID

        merchantCallbackURL = Request.Form["merchantCallbackURL"];  // 交易结果通知地址
        addressFlag = Request.Form["addressFlag"];                  // 需要填写送货信息 0：不需要  1:需要
        sMctProperties = Request.Form["sMctProperties"];            // 商家扩展信息
        frpId = Request.Form["frpId"];                              // 银行ID

        //创建一个Form
        htmlCodePost = Buy.CreateForm(merchantId, keyValue, orderId, amount, cur, productId, merchantCallbackURL, addressFlag, sMctProperties, frpId, "frmYeepay");
        //创建GET方式的url
        htmlCodeGet = Buy.CreateUrl(merchantId, keyValue, orderId, amount, cur, productId, merchantCallbackURL, addressFlag, sMctProperties, frpId);
    }
}
