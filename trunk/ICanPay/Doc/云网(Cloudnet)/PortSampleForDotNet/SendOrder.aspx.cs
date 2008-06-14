using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using net.pay.cncard.Security;

namespace PortSampleForDotNet
{
	/// <summary>
	/// Summary description for SendOrder.
	/// </summary>
	public class SendOrder : System.Web.UI.Page
	{

		protected string c_mid;			//商户编号，在申请商户成功后即可获得，可以在申请商户成功的邮件中获取该编号
		protected string c_order;		//商户网站生成的订单号，不能重复
		protected string c_name;		//商户订单中的收货人姓名
		protected string c_address;		//商户订单中的收货人地址
		protected string c_tel;			//商户订单中的收货人电话
		protected string c_post;		//商户订单中的收货人邮编
		protected string c_email;		//商户订单中的收货人Email
		protected string c_orderamount;	//商户订单总金额
		protected string c_ymd;			//商户订单的产生日期，格式为"yyyymmdd"，如20050102
		protected string c_moneytype;	//支付币种，0为人民币
		protected string c_retflag;		//商户订单支付成功后是否需要返回商户指定的文件，0：不用返回 1：需要返回
		protected string c_paygate;		//如果在商户网站选择银行则设置该值，具体值可参见《云网支付@网技术接口手册》附录一；如果来云网支付@网选择银行此项为空值。
		protected string c_returl;		//如果c_retflag为1时，该值代表支付成功后返回的文件的路径
		protected string c_memo1;		//商户需要在支付结果通知中转发的商户参数一
		protected string c_memo2;		//商户需要在支付结果通知中转发的商户参数二
		protected string c_signstr;		//商户对订单信息进行MD5签名后的字符串
		protected string c_pass;		//支付密钥，请登录商户管理后台，在帐户信息->基本信息->安全信息中的支付密钥项
		protected string notifytype;	//0普通通知方式/1服务器通知方式，空值为普通通知方式
		protected string c_language;	//对启用了国际卡支付时，可使用该值定义消费者在银行支付时的页面语种，值为：0银行页面显示为中文/1银行页面显示为英文

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DateTime dt = DateTime.Now;
			string timeStr = dt.ToString("HHmmss", DateTimeFormatInfo.InvariantInfo); 
			
			c_mid = "";
			c_pass = "";

			c_name		= "张三";
			c_address	= "北京市朝阳区XX";
			c_tel		= "010-12345678";
			c_post		= "100001";
			c_email		= "zhangsan@test.com";

			c_ymd = dt.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo); 
			c_order = c_ymd + timeStr;
			c_orderamount = "0.01";
			c_moneytype = "0";
			c_retflag = "1";
			c_paygate = "";
			c_returl = "http://www.yoursitename.com/urlpath/GetPayNotify.asp";	//该地址为商户接收云网支付结果通知的页面，请提交完整文件名(对应范例文件：GetPayNotify.aspx)
			c_memo1 = "ABCDE";
			c_memo2 = "12345";
			c_language = "0";
			notifytype = "1";

			string srcStr = c_mid + c_order + c_orderamount + c_ymd + c_moneytype + c_retflag + c_returl + c_paygate + c_memo1 + c_memo2 + notifytype + c_language + c_pass;
			c_signstr = cnSecurity.EncryptMD5(srcStr);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
