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
	/// ��ҳ�����֧������,��ָ��֧���ɹ���Ļ�����ַ������֧�������
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
			this.btPay.Click += new System.EventHandler(this.btPay_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btPay_Click(object sender, System.EventArgs e)
		{
			Md5Pay md5pay = new Md5Pay();

			//��Ʒ����
			md5pay.Desc = tbDesc.Text.Trim();
			
			//֧������
			md5pay.Date = DateTime.Now.ToString("yyyyMMdd");			
			
			//�̻�������
			md5pay.Sp_billno = long.Parse(tbBillNo.Text.Trim());
			
			//�ܽ��,�Է�Ϊ��λ.
			md5pay.Total_fee = long.Parse(tbTotalFee.Text.Trim());
			
			//���׵���ע
			md5pay.Attach = tbMemo.Text.Trim();
			

			string url = "";
			if(!md5pay.GetPayUrl(out url))
			{
				labErrmsg.Text = Server.HtmlEncode(url);
			}
			else
			{
				/*��������԰�
				 * ���׵���			md5pay.Transaction_id
				 * �̻�������		md5pay.Sp_billno
				 * �������			md5pay.Total_fee
				 * ����Ϣ�������ݿ�.
				 * */

				Response.Redirect(url);
			}
		}
	}
}
