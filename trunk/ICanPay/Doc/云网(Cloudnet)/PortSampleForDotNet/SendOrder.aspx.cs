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

		protected string c_mid;			//�̻���ţ��������̻��ɹ��󼴿ɻ�ã������������̻��ɹ����ʼ��л�ȡ�ñ��
		protected string c_order;		//�̻���վ���ɵĶ����ţ������ظ�
		protected string c_name;		//�̻������е��ջ�������
		protected string c_address;		//�̻������е��ջ��˵�ַ
		protected string c_tel;			//�̻������е��ջ��˵绰
		protected string c_post;		//�̻������е��ջ����ʱ�
		protected string c_email;		//�̻������е��ջ���Email
		protected string c_orderamount;	//�̻������ܽ��
		protected string c_ymd;			//�̻������Ĳ������ڣ���ʽΪ"yyyymmdd"����20050102
		protected string c_moneytype;	//֧�����֣�0Ϊ�����
		protected string c_retflag;		//�̻�����֧���ɹ����Ƿ���Ҫ�����̻�ָ�����ļ���0�����÷��� 1����Ҫ����
		protected string c_paygate;		//������̻���վѡ�����������ø�ֵ������ֵ�ɲμ�������֧��@�������ӿ��ֲᡷ��¼һ�����������֧��@��ѡ�����д���Ϊ��ֵ��
		protected string c_returl;		//���c_retflagΪ1ʱ����ֵ����֧���ɹ��󷵻ص��ļ���·��
		protected string c_memo1;		//�̻���Ҫ��֧�����֪ͨ��ת�����̻�����һ
		protected string c_memo2;		//�̻���Ҫ��֧�����֪ͨ��ת�����̻�������
		protected string c_signstr;		//�̻��Զ�����Ϣ����MD5ǩ������ַ���
		protected string c_pass;		//֧����Կ�����¼�̻������̨�����ʻ���Ϣ->������Ϣ->��ȫ��Ϣ�е�֧����Կ��
		protected string notifytype;	//0��֪ͨͨ��ʽ/1������֪ͨ��ʽ����ֵΪ��֪ͨͨ��ʽ
		protected string c_language;	//�������˹��ʿ�֧��ʱ����ʹ�ø�ֵ����������������֧��ʱ��ҳ�����֣�ֵΪ��0����ҳ����ʾΪ����/1����ҳ����ʾΪӢ��

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DateTime dt = DateTime.Now;
			string timeStr = dt.ToString("HHmmss", DateTimeFormatInfo.InvariantInfo); 
			
			c_mid = "";
			c_pass = "";

			c_name		= "����";
			c_address	= "�����г�����XX";
			c_tel		= "010-12345678";
			c_post		= "100001";
			c_email		= "zhangsan@test.com";

			c_ymd = dt.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo); 
			c_order = c_ymd + timeStr;
			c_orderamount = "0.01";
			c_moneytype = "0";
			c_retflag = "1";
			c_paygate = "";
			c_returl = "http://www.yoursitename.com/urlpath/GetPayNotify.asp";	//�õ�ַΪ�̻���������֧�����֪ͨ��ҳ�棬���ύ�����ļ���(��Ӧ�����ļ���GetPayNotify.aspx)
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
