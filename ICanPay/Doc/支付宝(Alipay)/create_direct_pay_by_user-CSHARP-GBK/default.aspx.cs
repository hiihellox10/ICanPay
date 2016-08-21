using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Com.Alipay;

/// <summary>
/// 功能：即时到账交易接口接入页
/// 版本：3.4
/// 修改日期：2016-03-08
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// /////////////////注意///////////////////////////////////////////////////////////////
/// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
/// 1、开发文档中心（https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.KvddfJ&treeId=62&articleId=103740&docType=1）
/// 2、商户帮助中心（https://cshall.alipay.com/enterprise/help_detail.htm?help_id=473888）
/// 3、支持中心（https://support.open.alipay.com/alipay/support/index.htm）
/// 如果不想使用扩展功能请把扩展功能参数赋空值。
/// </summary>
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void BtnAlipay_Click(object sender, EventArgs e)
    {
        ////////////////////////////////////////////请求参数////////////////////////////////////////////

        //商户订单号，商户网站订单系统中唯一订单号，必填
        string out_trade_no = WIDout_trade_no.Text.Trim();

        //订单名称，必填
        string subject = WIDsubject.Text.Trim();

        //付款金额，必填
        string total_fee = WIDtotal_fee.Text.Trim();

        //商品描述，可空
        string body = WIDbody.Text.Trim();




        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("service", Config.service);
        sParaTemp.Add("partner", Config.partner);
        sParaTemp.Add("seller_id", Config.seller_id);
        sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
        sParaTemp.Add("payment_type", Config.payment_type);
        sParaTemp.Add("notify_url", Config.notify_url);
        sParaTemp.Add("return_url", Config.return_url);
        sParaTemp.Add("anti_phishing_key", Config.anti_phishing_key);
        sParaTemp.Add("exter_invoke_ip", Config.exter_invoke_ip);
        sParaTemp.Add("out_trade_no", out_trade_no);
        sParaTemp.Add("subject", subject);
        sParaTemp.Add("total_fee", total_fee);
        sParaTemp.Add("body", body);
        //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.O9yorI&treeId=62&articleId=103740&docType=1
        //如sParaTemp.Add("参数名","参数值");

        //建立请求
        string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
        Response.Write(sHtmlText);
        
    }
}
