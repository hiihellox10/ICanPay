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
using Com.Alipay;
using System.Xml;
using System.Text;

/// <summary>
/// 功能：即时到账批量退款有密接口接入页
/// 版本：3.2
/// 日期：2011-03-11
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// /////////////////注意///////////////////////////////////////////////////////////////
/// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
/// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
/// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
/// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
/// 
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

        //必填参数//


        //卖家用户ID
        string seller_user_id = Seller_user_id.Text.Trim();
        //卖家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。

        //退款批次号
        string batch_no = Batch_no.Text.Trim();
     
        //格式为：退款日期（8位当天日期）+流水号（3～24位，不能接受“000”，但是可以接受英文字符）。

        //退款请求时间
        string refund_date = Refund_date.Text.Trim();
     
        //退款请求的当前时间。格式为：yyyy-MM-dd hh:mm:ss

        //退款总笔数
        string batch_num = Batch_num.Text.Trim();
        //即参数detail_data的值中，“#”字符出现的数量加1，最大支持1000笔（即“#”字符出现的最大数量999个）。


        //单笔数据集
        string detail_data = Detail_data.Text.Trim();
        //退款请求的明细数据。格式详见“4.3 单笔数据集参数说明”。
       
        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
       
        sParaTemp.Add("seller_user_id", seller_user_id);
        sParaTemp.Add("batch_no", batch_no);
        sParaTemp.Add("refund_date", refund_date);
        sParaTemp.Add("batch_num", batch_num);
        sParaTemp.Add("detail_data", detail_data);


       


        //构造即时到账批量退款有密接口表单提交HTML数据，无需修改
        Service ali = new Service();
        string sHtmlText = ali.Refund_fastpay_by_platform_pwd(sParaTemp);
        Response.Write(sHtmlText);
    }
}
