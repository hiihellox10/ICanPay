<%
/* *
 *���ܣ���ʱ���������˿����ܽӿڽ���ҳ
 *�汾��3.2
 *���ڣ�2011-03-17
 *˵����
 *���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 *�ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���

 *************************ע��*****************
 *������ڽӿڼ��ɹ������������⣬���԰��������;�������
 *1���̻��������ģ�https://b.alipay.com/support/helperApply.htm?action=consultationApply�����ύ���뼯��Э�������ǻ���רҵ�ļ�������ʦ������ϵ��Э�����
 *2���̻��������ģ�http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9��
 *3��֧������̳��http://club.alipay.com/read-htm-tid-8681712.html��
 *�������ʹ����չ���������չ���ܲ�������ֵ��
 **********************************************
 */
%>
<%@ page language="java" contentType="text/html; charset=gbk"
	pageEncoding="gbk"%>
<%@ page import="com.alipay.services.*"%>
<%@ page import="com.alipay.util.*"%>
<%@ page import="java.util.HashMap"%>
<%@ page import="java.util.Map"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=gbk">
		<title>��ʱ���������˿����ܽӿ�</title>
	</head>
	<%
    
		////////////////////////////////////�������//////////////////////////////////////
		
		//�������//
		
		//�˿����κš���ʽΪ���˿����ڣ�8λ�������ڣ�+��ˮ�ţ�3��24λ�����ܽ��ܡ�000�������ǿ��Խ���Ӣ���ַ���
		String batch_no = request.getParameter("batch_no");
		
		//�˿�����ʱ��
		String refund_date = request.getParameter("refund_date");
		
		//�˿��ܱ���
		String batch_num = request.getParameter("batch_num");
		
		//�������ݼ�
		String detail_data =  new String(request.getParameter("detail_data").getBytes("ISO-8859-1"),"gbk");
		//��ʽ����һ�ʽ���#�ڶ��ʽ���#�����ʽ���
	        //��N�ʽ��׸�ʽ�������˿���Ϣ
	        //�����˿���Ϣ��ʽ��ԭ����֧�������׺�^�˿��ܽ��^�˿�����
	        //ע�⣺
	        //1.detail_data�е��˿�����ܺ�Ҫ���ڲ���batch_num��ֵ
	        //2.detail_data��ֵ�в����С�^������|������#������$����Ӱ��detail_data�ĸ�ʽ�������ַ�
	        //3.detail_data���˿��ܽ��ܴ��ڽ����ܽ��
	        //4.һ�ʽ��׿��Զ���˿ֻ��Ҫ���ض���˿���ܽ������ñʽ��׸���ʱ��
	        //5.��֧���˷�����
		//ѡ�����������������������ͬʱΪ�գ�
		//����֧�����˺�
		String seller_email = request.getParameter("seller_email");
		
		//�����û�ID
		String seller_user_id = request.getParameter("seller_user_id");
		
		//////////////////////////////////////////////////////////////////////////////////
		
		//������������������
		Map<String, String> sParaTemp = new HashMap<String, String>();
		sParaTemp.put("seller_email", seller_email);
		sParaTemp.put("seller_user_id", seller_user_id);
        sParaTemp.put("batch_no", batch_no);
        sParaTemp.put("refund_date", refund_date);
        sParaTemp.put("batch_num", batch_num);
        sParaTemp.put("detail_data", detail_data);
		//���캯������������URL  
		String sHtmlText = AlipayService.refund_fastpay_by_platform_pwd(sParaTemp);
		out.println(sHtmlText);
	%>
	<body>
	</body>
</html>
