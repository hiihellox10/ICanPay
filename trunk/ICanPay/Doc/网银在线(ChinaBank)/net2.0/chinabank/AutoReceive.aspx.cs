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

public partial class AutoReceive : System.Web.UI.Page
{
    protected string v_oid; //订单号
    protected string v_pstatus; //支付状态码
    //20（支付成功，对使用实时银行卡进行扣款的订单）；
    //30（支付失败，对使用实时银行卡进行扣款的订单）；

    protected string v_pstring; //支付状态描述
    protected string v_pmode; //支付银行
    protected string v_md5str; //MD5校验码
    protected string v_amount; //支付金额
    protected string v_moneytype; //币种		
    protected string remark1;//备注1
    protected string remark2;//备注1


    protected void Page_Load(object sender, EventArgs e)
    {
        			// MD5密钥要跟订单提交页相同，如Send.asp里的 key = "test" ,修改""号内 test 为您的密钥
			string key = "test";	// 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
						// 登陆后在上面的导航栏里可能找到“资料管理”，在资料管理的二级导航栏里有“MD5密钥设置”
						// 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了			
			
			v_oid     = Request["v_oid"];
			v_pstatus = Request["v_pstatus"];
			v_pstring = Request["v_pstring"];
			v_pmode   = Request["v_pmode"];
			v_md5str  = Request["v_md5str"];
			v_amount  = Request["v_amount"];
			v_moneytype = Request["v_moneytype"];
			remark1 = Request["remark1"];
			remark2 = Request["remark2"];

			string str = v_oid+v_pstatus+v_amount+v_moneytype+key;
            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();
			
            if(str==v_md5str)
			{
				Response.Write("ok");

				if(v_pstatus.Equals("20")) 
				{
					//支付成功
					//商户系统的逻辑处理（例如判断金额，判断支付状态，更新订单状态等等）.......
				}
			}
			else
			{
				Response.Write("error");
			}
    }
}
