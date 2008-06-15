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
public partial class Callback : System.Web.UI.Page
{
    public static string merchantId;
    public static string keyValue;
    public string sCmd;
    public string sErrorCode;
    public string sTrxId;

    public string amount;
    public string cur;
    public string productId;
    public string orderId;
    public string userId;

    public string mp;
    public string bType;
    public string svrHmac;
    public bool isOk;

    protected void Page_Load(object sender, EventArgs e)
    {
        merchantId = "10000432521";                                     // 商家ID
        keyValue = "8UPp0KE8sq73zVP370vko7C39403rtK1YwX40Td6irH216036H27Eb12792t";                                           // 商家密钥

        sCmd = Buy.GetQueryString("r0_Cmd", Request.Url.Query);
        sErrorCode = Buy.GetQueryString("r1_Code", Request.Url.Query);
        sTrxId = Buy.GetQueryString("r2_TrxId", Request.Url.Query);
        amount = Buy.GetQueryString("r3_Amt", Request.Url.Query);       // 订单金额
        cur = Buy.GetQueryString("r4_Cur", Request.Url.Query);

        productId = Buy.GetQueryString("r5_Pid", Request.Url.Query);    // 商品ID
        orderId = Buy.GetQueryString("r6_Order", Request.Url.Query);    // 订单号
        userId = Buy.GetQueryString("r7_Uid", Request.Url.Query);
        mp = Buy.GetQueryString("r8_MP", Request.Url.Query);            // 商家扩展信息
        bType = Buy.GetQueryString("r9_BType", Request.Url.Query);

        svrHmac = Buy.GetQueryString("hmac", Request.Url.Query);

        // 校验返回数据包
        isOk = Buy.VerifyCallback(merchantId, keyValue, sCmd, sErrorCode, sTrxId, amount, cur, productId, orderId, userId, mp, bType, svrHmac);

        if (isOk == true)
        {
            if (sErrorCode == "1")
            {
                if (bType == "1")
                {
                    //  callback方式:在线支付浏览器重定向
                    Response.Write("交易成功!<br>商品ID:" + productId + "<br>商家订单号:" + orderId + "<br>交易金额:" + amount + "<br>YeePay易宝交易流水号:" + sTrxId + "<BR>");
                }
                else if (bType == "2")
                {
                    // * 如果是服务器返回或者电话支付返回(bType==2 or bType==3)则需要回应一个特定字符串'SUCCES',且在'SUCCESS'之前不可以有任何其他字符输出,保证首先输出的是'SUCCESS'字符串
                    Response.Write("SUCCESS");
                    // callback方式:在线支付服务器点对点通讯
                    // 写入 log.txt 这里用来调试接口
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(@"./log.txt")))
                    {
                        try
                        {
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(@"./log.txt"), true, System.Text.Encoding.GetEncoding("gb2312"));
                            sw.WriteLine("\n");
                            sw.WriteLine(DateTime.Now.ToString() + " 服务器返回 交易成功! 商家订单号:" + orderId + " 交易金额:" + amount + " YeePay易宝交易流水号:" + sTrxId);
                            sw.Flush();
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<br>" + ex.ToString());
                        }
                    }
                }
                else if (bType == "3")
                {
                    // * 如果是服务器返回或者电话支付返回(bType==2 or bType==3)则需要回应一个特定字符串'SUCCES',且在'SUCCESS'之前不可以有任何其他字符输出,保证首先输出的是'SUCCESS'字符串
                    Response.Write("SUCCESS");
                    // callback方式:电话支付返回
                    // 写入 log.txt 这里用来调试接口
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(@"./log.txt")))
                    {
                        try
                        {
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(@"./log.txt"), true, System.Text.Encoding.GetEncoding("gb2312"));
                            sw.WriteLine("\n");
                            sw.WriteLine(DateTime.Now.ToString() + " 电话支付返回 交易成功! 商家订单号:" + orderId + " 交易金额:" + amount + " YeePay易宝交易流水号:" + sTrxId);
                            sw.Flush();
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<br>" + ex.ToString());
                        }
                    }
                }
            }
            else
            {
                Response.Write("交易失败!");
            }
        }
        else
        {
            Response.Write("交易签名被篡改!");
        }
    }
}
