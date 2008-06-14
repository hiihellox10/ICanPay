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
	/// ֧���������ҳ��,���ݻ���ʱ��URL�����Ĳ���ȡ��֧�������
	/// </summary>
	public class payresult : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label labTransid;
		protected System.Web.UI.WebControls.Label labBillno;
		protected System.Web.UI.WebControls.Label labTotalFee;
		protected System.Web.UI.WebControls.Label labAttach;
		protected System.Web.UI.WebControls.Label labResult;
		protected System.Web.UI.WebControls.Label labErrmsg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				//���ڰ�ȫ����,����У��һ������URL�Ļ����Ƿ�Ϊ�Ƹ�ͨ������.

				string errmsg = "";

				Md5Pay md5pay = new Md5Pay();
				if(!md5pay.GetPayValueFromUrl(Request.QueryString,out errmsg))
				{
					labErrmsg.Text = Server.HtmlEncode(errmsg);
				}
				else
				{
					labTransid.Text = md5pay.Transaction_id;
					labAttach.Text = Server.HtmlEncode(md5pay.Attach);
					labBillno.Text = md5pay.Sp_billno.ToString();
					labTotalFee.Text = md5pay.Total_fee.ToString();
					labResult.Text = md5pay.PayResultStr;

					if(md5pay.PayResult == Md5Pay.PAYERROR)
					{
						 labResult.Text += md5pay.PayErrMsg;
					}

					//��������Ը���֧�����������Ӧ�Ĵ���,������¶���Ϊ�ɹ���ʧ��.
				}
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
