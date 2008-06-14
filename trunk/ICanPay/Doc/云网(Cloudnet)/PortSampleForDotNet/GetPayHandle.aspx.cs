using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using net.pay.cncard.Security;

namespace PortSampleForDotNet
{
	/// <summary>
	/// Summary description for GetPayHandle.
	/// </summary>
	public class GetPayHandle : System.Web.UI.Page
	{
		protected string c_mid;			//�̻���ţ��������̻��ɹ��󼴿ɻ�ã������������̻��ɹ����ʼ��л�ȡ�ñ��
		protected string c_order;		//�̻��ṩ�Ķ�����
		protected string c_orderamount; //�̻��ṩ�Ķ����ܽ���ԪΪ��λ��С���������λ���磺13.05
		protected string c_ymd;			//�̻���������Ķ����������ڣ���ʽΪ"yyyymmdd"����20050102
		protected string c_transnum;	//����֧�������ṩ�ĸñʶ����Ľ�����ˮ�ţ����պ��ѯ���˶�ʹ�ã�
		protected string c_succmark;	//���׳ɹ���־��Y-�ɹ� N-ʧ��
		protected string c_moneytype;	//֧�����֣�0Ϊ�����
		protected string c_cause;		//�������֧��ʧ�ܣ����ֵ����ʧ��ԭ��
		protected string c_memo1;		//�̻��ṩ����Ҫ��֧�����֪ͨ��ת�����̻�����һ
		protected string c_memo2;		//�̻��ṩ����Ҫ��֧�����֪ͨ��ת�����̻�������
		protected string c_signstr;		//����֧�����ض�������Ϣ����MD5���ܺ���ַ���
		protected System.Web.UI.WebControls.Label payMsg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			c_mid			= Request["c_mid"];			
			c_order			= Request["c_order"];		
			c_orderamount	= Request["c_orderamount"];	
			c_ymd			= Request["c_ymd"];			
			c_transnum		= Request["c_transnum"];		
			c_succmark		= Request["c_succmark"];				
			c_moneytype		= Request["c_moneytype"];	
			c_cause			= Request["c_cause"];				
			c_memo1			= Request["c_memo1"];		
			c_memo2			= Request["c_memo2"];		
			c_signstr		= Request["c_signstr"];
			
			//---У����Ϣ������---
			if(c_mid==null || c_order==null || c_orderamount==null || c_ymd==null || c_moneytype==null || c_transnum==null || c_succmark==null || c_signstr==null)
			{
				Response.Write("֧����Ϣ���� !");
				Response.End();
				return;
			}

			//�붨���̻���
			string MerchantID;
			MerchantID = "";

			//�붨��֧����Կ
			string c_pass;
			c_pass = "";

			//У���̻����
			if(MerchantID!=c_mid)
			{
				Response.Write("�ύ���̻�������� !");
				Response.End();
				return;
			}

			//��֧��֪ͨ��Ϣ����MD5����
			string srcStr,r_signstr;
			srcStr = c_mid + c_order + c_orderamount + c_ymd + c_transnum + c_succmark + c_moneytype + c_memo1 + c_memo2 + c_pass;
			r_signstr = cnSecurity.EncryptMD5(srcStr);

			//У���̻���վ��֪ͨ��Ϣ��MD5���ܵĽ��������֧�������ṩ��MD5���ܽ���Ƿ�һ��
			if(r_signstr!=c_signstr)
			{
				Response.Write("ǩ����֤ʧ�� !");
				Response.End();
				return;
			}

			//У���̻�����ϵͳ���Ƿ���֪ͨ��Ϣ���صĶ�����Ϣ
			string connStr;
			string chkSQL;
			connStr = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
			chkSQL = @"select top 1 * from �̻��Ķ����� where �̻�������=@�̻�������";
			SqlConnection conn = new SqlConnection(connStr);
			SqlCommand myComm = new SqlCommand(chkSQL);
			myComm.Parameters.Add("@�̻�������",c_order);
			SqlDataReader myReader;

			try
			{
				myComm.Connection = conn;
				myComm.Connection.Open();
				myReader = myComm.ExecuteReader();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				Response.End();
				return;
			}

			if(!myReader.Read())
			{
				Response.Write("δ�ҵ��ö�����Ϣ !");
				Response.End();
				return;
			}

			//У���̻�����ϵͳ�м�¼�Ķ�����������֧������֪ͨ��Ϣ�еĽ���Ƿ�һ��
			double r_orderamount = Convert.ToDouble(myReader["�������"]);
			if(!double.Parse(c_orderamount).Equals(r_orderamount))
			{
				Response.Write("֧��������� !");
				Response.End();
				return;
			}

			//У���̻�����ϵͳ�м�¼�Ķ����������ں�����֧������֪ͨ��Ϣ�еĶ������������Ƿ�һ��
			string r_ymd;
			r_ymd = myReader["������������"].ToString().Trim();
			if(r_ymd!=c_ymd)
			{
				Response.Write("����ʱ������ !");
				Response.End();
				return;
			}

			//У���̻�ϵͳ�м�¼����Ҫ��֧�����֪ͨ��ת���Ĳ���������֧������֪ͨ��Ϣ���ṩ�Ĳ����Ƿ�һ��
			string r_memo1,r_memo2;
			r_memo1 = myReader["ת������һ"].ToString().Trim();
			r_memo2 = myReader["ת��������"].ToString().Trim();
			if(r_memo1!=c_memo1 || r_memo2!=c_memo2)
			{
				Response.Write("�Զ�������ύ���� !");
				Response.End();
				return;
			}

			//У�鷵�ص�֧������ĸ�ʽ�Ƿ���ȷ
			if(c_succmark!="Y" && c_succmark!="N")
			{
				Response.Write("�����ύ���� !");
				Response.End();
				return;
			}

			//���ݷ��ص�֧��������̻���վ�����и��û���ʾ˵��
			if(c_succmark=="Y")
			{
				������GetPayNotify.cs���ѽ�����ʵ�ʷ��������������ڴ˴�ֻҪϵͳ�в�ѯ���������Ľ����Ȼ����û���ʾ����ɹ�����ʾ��Ϣ���ɡ�
			}
			else if(c_succmark=="N")
			{
				
			}
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
