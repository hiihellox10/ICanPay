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
	/// 查询请求页面
	/// </summary>
	public class md5_query : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox tbBillNo;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox tbMemo;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.TextBox tbDate;
		protected System.Web.UI.WebControls.TextBox tbTranID;
		protected System.Web.UI.WebControls.Label labErrmsg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				tbDate.Text = DateTime.Now.ToString("yyyyMMdd");
			}
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
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btQuery_Click(object sender, System.EventArgs e)
		{
			Md5Pay md5pay = new Md5Pay();

			md5pay.InitQueryParam(tbDate.Text.Trim(),tbTranID.Text.Trim(),long.Parse(tbBillNo.Text.Trim()),tbMemo.Text.Trim());
			
			
			string url = "";
			if(!md5pay.GetQueryUrl(out url))
			{
				labErrmsg.Text = Server.HtmlEncode(url);
			}
			else
			{
				Response.Redirect(url);
			}
		}
	}
}
