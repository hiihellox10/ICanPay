<%
/* *
 *���ܣ���ʱ���ʽӿڽ���ҳ
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
		<title>֧������ʱ���ʽӿ�</title>
	</head>
	<%
    
		////////////////////////////////////�������//////////////////////////////////////
		
		//�������//

		//�������վ����ϵͳ�е�Ψһ������ƥ��
		String out_trade_no = UtilDate.getOrderNum();
		//�������ƣ���ʾ��֧��������̨��ġ���Ʒ���ơ����ʾ��֧�����Ľ��׹���ġ���Ʒ���ơ����б��
		String subject = new String(request.getParameter("subject").getBytes("ISO-8859-1"),"gbk");
		//����������������ϸ��������ע����ʾ��֧��������̨��ġ���Ʒ��������
		String body = new String(request.getParameter("alibody").getBytes("ISO-8859-1"),"gbk");
		//�����ܽ���ʾ��֧��������̨��ġ�Ӧ���ܶ��
		String total_fee = request.getParameter("total_fee");

		
		//��չ���ܲ�������Ĭ��֧����ʽ//
		
		//Ĭ��֧����ʽ��ȡֵ������ʱ���ʽӿڡ������ĵ��е���������б�
		String paymethod = "";
		//Ĭ���������ţ������б������ʱ���ʽӿڡ������ĵ�����¼�����������б�
		String defaultbank = "";
		
		//��չ���ܲ�������������//

		//������ʱ���
		String anti_phishing_key  = "";
		//��ȡ�ͻ��˵�IP��ַ�����飺��д��ȡ�ͻ���IP��ַ�ĳ���
		String exter_invoke_ip= "";
		//ע�⣺
		//1.������ѡ���Ƿ��������㹦��
		//2.exter_invoke_ip��anti_phishing_keyһ�������ù�����ô���Ǿͻ��Ϊ�������
		//3.���������㹦�ܺ󣬷��������������Ա���֧��Զ��XML�����������úøû�����
		//4.����ʹ��POST��ʽ��������
		//ʾ����
		//anti_phishing_key = AlipayService.query_timestamp();	//��ȡ������ʱ�������
		//exter_invoke_ip = "202.1.1.1";
		
		//��չ���ܲ�����������///
		
		//�Զ���������ɴ���κ����ݣ���=��&�������ַ��⣩��������ʾ��ҳ����
		String extra_common_param = "";
		//Ĭ�����֧�����˺�
		String buyer_email = "";
		//��Ʒչʾ��ַ��Ҫ��http:// ��ʽ������·�����������?id=123�����Զ������
		String show_url = "http://www.xxx.com/order/myorder.jsp";
		
		//��չ���ܲ�����������(��Ҫʹ�ã��밴��ע��Ҫ��ĸ�ʽ��ֵ)//
		
		//������ͣ���ֵΪ�̶�ֵ��10������Ҫ�޸�
		String royalty_type = "";
		//�����Ϣ��
		String royalty_parameters ="";
		//ע�⣺
		//����Ҫ����̻���վ���������̬��ȡÿ�ʽ��׵ĸ������տ��˺š��������������˵�������ֻ������10��
		//����������ܺ���С�ڵ���total_fee
		//�����Ϣ����ʽΪ���տEmail_1^���1^��ע1|�տEmail_2^���2^��ע2
		//ʾ����
		//royalty_type = "10"
		//royalty_parameters	= "111@126.com^0.01^����עһ|222@126.com^0.01^����ע��"
		
		//////////////////////////////////////////////////////////////////////////////////
		
		//������������������
		Map<String, String> sParaTemp = new HashMap<String, String>();
        sParaTemp.put("payment_type", "1");
        sParaTemp.put("out_trade_no", out_trade_no);
        sParaTemp.put("subject", subject);
        sParaTemp.put("body", body);
        sParaTemp.put("total_fee", total_fee);
        sParaTemp.put("show_url", show_url);
        sParaTemp.put("paymethod", paymethod);
        sParaTemp.put("defaultbank", defaultbank);
        sParaTemp.put("anti_phishing_key", anti_phishing_key);
        sParaTemp.put("exter_invoke_ip", exter_invoke_ip);
        sParaTemp.put("extra_common_param", extra_common_param);
        sParaTemp.put("buyer_email", buyer_email);
        sParaTemp.put("royalty_type", royalty_type);
        sParaTemp.put("royalty_parameters", royalty_parameters);
		
		//���캯������������URL
		String sHtmlText = AlipayService.create_direct_pay_by_user(sParaTemp);
		out.println(sHtmlText);
	%>
	<body>
	</body>
</html>
