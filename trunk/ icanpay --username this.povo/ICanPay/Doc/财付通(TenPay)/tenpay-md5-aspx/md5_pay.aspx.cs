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


namespace tenpaymd5
{
	/// <summary>
	/// 本页面完成支付请求,并指定支付成功后的回跳地址来处理支付结果。
	/// </summary>
	public class md5_pay : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox tbBillNo;
		protected System.Web.UI.WebControls.TextBox tbTotalFee;
		protected System.Web.UI.WebControls.TextBox tbDesc;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox tbMemo;
		protected System.Web.UI.WebControls.Button btPay;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label labErrmsg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.btPay.Click += new System.EventHandler(this.btPay_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btPay_Click(object sender, System.EventArgs e)
		{
			Md5Pay md5pay = new Md5Pay();

			//商品名称
			md5pay.Desc = tbDesc.Text.Trim();
			
			//支付日期
			md5pay.Date = DateTime.Now.ToString("yyyyMMdd");			
			
			//商户订单号
			md5pay.Sp_billno = long.Parse(tbBillNo.Text.Trim());
			
			//总金额,以分为单位.
			md5pay.Total_fee = long.Parse(tbTotalFee.Text.Trim());
			
			//交易单备注
			md5pay.Attach = tbMemo.Text.Trim();
			

			string url = "";
			if(!md5pay.GetPayUrl(out url))
			{
				labErrmsg.Text = Server.HtmlEncode(url);
			}
			else
			{
				/*在这里可以把
				 * 交易单号			md5pay.Transaction_id
				 * 商户订单号		md5pay.Sp_billno
				 * 订单金额			md5pay.Total_fee
				 * 等信息记入数据库.
				 * */

				Response.Redirect(url);
			}
		}
	}
}
