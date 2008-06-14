using System;

namespace com.yeepay
{
	/// <summary>
	/// Buy ��ժҪ˵����
	/// </summary>
	public class Buy:FormatQueryString
	{
		private static string nodeAuthorizationURL = @"https://www.yeepay.com/app-merchant-proxy/node";
		
		public Buy(){}

		// �����ύ��
		public static string  CreateForm(	string merchantId,
											string keyValue,
											string orderId,
											string amount,
											string cur,

											string productId,
											string merchantCallbackURL,
											string addressFlag,

											string sMctProperties,
											string frpId,
											string formName)
		{
			string messageType	= "Buy";
			string needResponse = "1";
			Digest digest		= new Digest();

			string sbOld="";
			sbOld = sbOld + messageType;
			sbOld = sbOld + merchantId;
			sbOld = sbOld + orderId;
			sbOld = sbOld + amount;
	
			sbOld = sbOld + cur;		
			sbOld = sbOld + productId;
			sbOld = sbOld + merchantCallbackURL;
			
			sbOld = sbOld + addressFlag;
			sbOld = sbOld + sMctProperties;
			sbOld = sbOld + frpId;
			sbOld = sbOld + needResponse;

			string sNewString=digest.HmacSign(sbOld,keyValue);

			string html="";
			html = html + "<form name='" + formName + "' action='" + nodeAuthorizationURL + "' method='post'>";
			html = html + "<input type='hidden' name='p0_Cmd' value='" + messageType + "'>";
			html = html + "<input type='hidden' name='p1_MerId' value='" + merchantId + "'>";
			html = html + "<input type='hidden' name='p2_Order' value='" + orderId + "'>";

			html = html + "<input type='hidden' name='p3_Amt' value='" + amount + "'>";
			html = html + "<input type='hidden' name='p4_Cur' value='" + cur + "'>";
			html = html + "<input type='hidden' name='p5_Pid' value='" + productId + "'>";

			html = html + "<input type='hidden' name='p8_Url' value='" + merchantCallbackURL + "'>";
			html = html + "<input type='hidden' name='p9_SAF' value='" + addressFlag + "'>";
			html = html + "<input type='hidden' name='pa_MP' value='" + sMctProperties + "'>";
			html = html + "<input type='hidden' name='pd_FrpId' value='" + frpId + "'>";
			html = html + "<input type='hidden' name='pr_NeedResponse' value='" + needResponse + "'>";

			html = html + "<input type='hidden' name='hmac' value='" + sNewString + "'>";

			html=html+"</form>";
			//html=html+"<script type='text/javascript'>window.document.formYeepay.submit();</script>";
			
			return html;
		}

		// ��������֧��URL
		public static string  CreateUrl(	
											string merchantId,
											string keyValue,
											string orderId,
											string amount,
											string cur,

											string productId,
											string merchantCallbackURL,
											string addressFlag,

											string sMctProperties,
											string frpId)
		{
			string messageType	= "Buy";
			string needResponse = "1";
			Digest digest		= new Digest();

			string sbOld="";
			sbOld = sbOld + messageType;
			sbOld = sbOld + merchantId;
			sbOld = sbOld + orderId;
			sbOld = sbOld + amount;
	
			sbOld = sbOld + cur;		
			sbOld = sbOld + productId;
			sbOld = sbOld + merchantCallbackURL;
			
			sbOld = sbOld + addressFlag;
			sbOld = sbOld + sMctProperties;
			sbOld = sbOld + frpId;
			sbOld = sbOld + needResponse;

			string sNewString=digest.HmacSign(sbOld,keyValue);

			string html = "";

			html += nodeAuthorizationURL;
			html += "?p0_Cmd=" + messageType;
			html += "&p1_MerId=" + merchantId;
			html += "&p2_Order=" + orderId;
			html += "&p3_Amt=" + amount;

			html += "&p4_Cur=" + cur;
			html += "&p5_Pid=" + productId;
			html += "&p8_Url=" + System.Web.HttpUtility.UrlEncode(merchantCallbackURL,System.Text.Encoding.GetEncoding("gb2312"));

			html += "&p9_SAF=" + addressFlag;
			html += "&pa_MP=" + sMctProperties;
			html += "&pd_FrpId=" + frpId;
			html += "&pr_NeedResponse=" + needResponse;

			html += "&hmac=" + sNewString;
			
			return html;
		}

		// ����url���md5
		public static bool VerifyCallback(	string merchantId,
											string keyValue,
											string sCmd,
											string sErrorCode,
											string sTrxId,

											string amount,
											string cur,
											string productId,
											string orderId,
											string userId,

											string mp,
											string bType,
											string hmac
											)
		{
			Digest digest = new Digest();

			string sbOld="";

           

			sbOld = sbOld + merchantId;
			sbOld = sbOld + sCmd;
			sbOld = sbOld + sErrorCode;
			sbOld = sbOld + sTrxId;
			sbOld = sbOld + amount;
	
			sbOld = sbOld + cur;		
			sbOld = sbOld + productId;
			sbOld = sbOld + orderId;
			sbOld = sbOld + userId;
			sbOld = sbOld + mp;

			sbOld = sbOld + bType;

			string sNewString=digest.HmacSign(sbOld,keyValue);

			if(hmac==sNewString)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		// �����绰����

		public static MotoOrdResult CreateMotoOrder(string merchantId,
													string keyValue,
													string merchantCallbackURL,
													string orderId,
													string amount,

													string cur,
													string productId,
													string sMctProperties,
													string frpId,

													string buyerTel,
													string buyerName,
													string buyerAddr,
													string period,
													string checkType,

													string periodType)
		{
			string messageType	= "MotoOrd";
			string needResponse = "1";
			string sbOld		= "";
			string sNewString	="";
			string sFormString	= "";

			Digest digest		= new Digest();
			HttpUtils httpUtils	= new HttpUtils();

			sbOld = sbOld + messageType;
			sbOld = sbOld + merchantId;	        //�����̼�ID
			sbOld = sbOld + orderId;            //���붨����ID
			sbOld = sbOld + amount;				//������
			sbOld = sbOld + cur;				//������ҵ�λ
				
			sbOld = sbOld + productId;			//�����ƷID
			sbOld = sbOld + merchantCallbackURL;//���뷵���̼�Url
			sbOld = sbOld + sMctProperties;		//�����̼ҷ��ظ�����Ϣ
			sbOld = sbOld + frpId;				//��������ID
				
			sbOld = sbOld + buyerTel;			//���빺�����ֻ���
			sbOld = sbOld + buyerName;			//���빺��������
			sbOld = sbOld + buyerAddr;			//���빺�����ͻ���ַ
			sbOld = sbOld + period;				//���붩����Ч��1-5��
			sbOld = sbOld + checkType;		    //���붩�����״̬
				
			sbOld = sbOld + periodType;		    //���붩����Ч�����
			sbOld = sbOld + needResponse;		// ����needResponse

			sNewString = digest.HmacSign(sbOld,keyValue);

			sFormString = "p0_Cmd=" + messageType;
			sFormString = sFormString + "&p1_MerId=" + merchantId;	    	//�����̼�ID
			sFormString = sFormString + "&p2_Order=" + orderId;				//���빺�򶩵�����
			sFormString = sFormString + "&p3_Amt=" + amount;				//������
			sFormString = sFormString + "&p4_Cur=" + cur;					//������ҵ�λ
				
			sFormString = sFormString + "&p5_Pid=" + System.Web.HttpUtility.UrlEncode(productId,System.Text.Encoding.GetEncoding("gb2312"));				//�����ƷID
			sFormString = sFormString + "&p8_Url=" + System.Web.HttpUtility.UrlEncode(merchantCallbackURL,System.Text.Encoding.GetEncoding("gb2312"));		//�������̼�ͨѶURL
			sFormString = sFormString + "&pa_MP=" + System.Web.HttpUtility.UrlEncode(sMctProperties,System.Text.Encoding.GetEncoding("gb2312"));			//�����̼ҷ��ظ�����Ϣ
			sFormString = sFormString + "&pd_FrpId=" + frpId;				//��������ID
				
			sFormString = sFormString + "&pe_BuyerTel=" + buyerTel;			//���빺�����ֻ���
			sFormString = sFormString + "&pf_BuyerName=" + System.Web.HttpUtility.UrlEncode(buyerName,System.Text.Encoding.GetEncoding("gb2312"));			//���빺��������
			sFormString = sFormString + "&pg_BuyerAddr=" + System.Web.HttpUtility.UrlEncode(buyerAddr,System.Text.Encoding.GetEncoding("gb2312"));			//���빺��󶨺���
			sFormString = sFormString + "&pm_Period=" + period;				//���붩����Ч��1-5��
			sFormString = sFormString + "&pn_CheckType=" + checkType;		//���붩�����״̬
				
			sFormString = sFormString + "&pn_Unit=" + periodType;       	//���붩����Ч�����
			sFormString = sFormString + "&pr_NeedResponse=" + needResponse;       	//����needResponse 
			sFormString = sFormString + "&hmac=" + sNewString;      	    //����У����

			string url=httpUtils.SendRequest(nodeAuthorizationURL,sFormString);

			string returnCode=FormatQueryString.GetQueryString("r1_Code",url);
			string returnAmt=FormatQueryString.GetQueryString("r3_Amt",url);
			string returnPid=FormatQueryString.GetQueryString("r5_Pid",url);
			string returnOrder=FormatQueryString.GetQueryString("r6_Order",url);
			string returnMotoId=FormatQueryString.GetQueryString("rd_MotoId",url);

			string returnAllPara=url;

			MotoOrdResult result = new MotoOrdResult(returnCode,returnAmt,returnPid,returnOrder,returnMotoId,returnAllPara);
			
			return result;
		}

		// ������ѯ

		public static QueryOrdResult QueryOrder(string merchantId,string keyValue,string orderId)
		{
			string messageType	= "QueryOrdDetail";
			string sbOld		= "";
			string sNewString	= "";
			string sFormString	= "";

			Digest digest		= new Digest();
			HttpUtils httpUtils	= new HttpUtils();

			sbOld = messageType + merchantId + orderId;
			sNewString=digest.HmacSign(sbOld,keyValue);
			
			sFormString = "p0_Cmd=" + messageType + "&p1_MerId=" + merchantId + "&p2_Order=" + orderId + "&hmac=" + sNewString;

			string url=httpUtils.SendRequest(nodeAuthorizationURL , sFormString);

            string returnCode = FormatQueryString.GetQueryString("r1_Code", url);
            string returnTrxId = FormatQueryString.GetQueryString("r2_TrxId", url);
            string returnAmt = FormatQueryString.GetQueryString("r3_Amt", url);
            string returnPid = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("r5_Pid", url), System.Text.Encoding.GetEncoding("gb2312"));
            string returnOrder = FormatQueryString.GetQueryString("r6_Order", url);
            string returnMP = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("r8_MP", url), System.Text.Encoding.GetEncoding("gb2312"));

            string returnPayStatus = FormatQueryString.GetQueryString("rb_PayStatus", url);
            string returnRefundCount = FormatQueryString.GetQueryString("rc_RefundCount", url);
            string returnRefundAmt = FormatQueryString.GetQueryString("rd_RefundAmt", url);
            string returnAllPara = url;

            QueryOrdResult result = new QueryOrdResult(returnCode, returnTrxId, returnAmt, returnPid, returnOrder, returnMP, returnPayStatus, returnRefundCount, returnRefundAmt, returnAllPara);

			return result;
		}

		// �˿�
		public static RefundResult RefundOrder(string merchantId,string keyValue,string trxId,string amount,string cur,string desc)
		{
			string messageType	= "RefundOrd";
			string sbOld		= "";
			string sNewString	= "";
			string sFormString	= "";

			Digest digest		= new Digest();
			HttpUtils httpUtils	= new HttpUtils();

			sbOld = messageType + merchantId + trxId + amount + cur;
			sNewString=digest.HmacSign(sbOld,keyValue);

			sFormString = "p0_Cmd=" + messageType + "&p1_MerId=" + merchantId + "&pb_TrxId=" + trxId + "&p3_Amt=" + amount + "&p4_Cur=" + cur + "&p5_Desc=" + desc + "&hmac=" + sNewString;

			string url=httpUtils.SendRequest(nodeAuthorizationURL , sFormString);
			
			string returnCode=FormatQueryString.GetQueryString("r1_Code",url);
			string returnAllPara=url;

			RefundResult result = new RefundResult(returnCode,returnAllPara);

			return result;
		}

	}
}
