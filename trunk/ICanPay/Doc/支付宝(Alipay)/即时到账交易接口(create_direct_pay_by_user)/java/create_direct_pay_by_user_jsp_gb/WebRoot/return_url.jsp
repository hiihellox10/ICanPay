<%
/* *
 ���ܣ�֧����ҳ����תͬ��֪ͨҳ��
 �汾��3.2
 ���ڣ�2011-03-17
 ˵����
 ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���

 //***********ҳ�湦��˵��***********
 ��ҳ����ڱ������Բ���
 �ɷ���HTML������ҳ��Ĵ��롢�̻�ҵ���߼��������
 TRADE_FINISHED(��ʾ�����Ѿ��ɹ��������������ٶԸý�������������);
 TRADE_SUCCESS(��ʾ�����Ѿ��ɹ����������ԶԸý����������������磺�����˿��);
 //********************************
 * */
%>
<%@ page language="java" contentType="text/html; charset=gbk" pageEncoding="gbk"%>
<%@ page import="java.util.*"%>
<%@ page import="java.util.Map"%>
<%@ page import="com.alipay.util.*"%>
<%@ page import="com.alipay.services.*"%>
<%@ page import="com.alipay.config.*"%>
<html>
  <head>
		<meta http-equiv="Content-Type" content="text/html; charset=gbk">
		<title>֧����ҳ����תͬ��֪ͨҳ��</title>
  </head>
  <body>
<%
	//��ȡ֧����GET����������Ϣ
	Map<String,String> params = new HashMap<String,String>();
	Map requestParams = request.getParameterMap();
	for (Iterator iter = requestParams.keySet().iterator(); iter.hasNext();) {
		String name = (String) iter.next();
		String[] values = (String[]) requestParams.get(name);
		String valueStr = "";
		for (int i = 0; i < values.length; i++) {
			valueStr = (i == values.length - 1) ? valueStr + values[i]
					: valueStr + values[i] + ",";
		}
		//����������δ����ڳ�������ʱʹ�á����mysign��sign�����Ҳ����ʹ����δ���ת��
		valueStr = new String(valueStr.getBytes("ISO-8859-1"), "gbk");
		params.put(name, valueStr);
	}

	
	//��ȡ֧������֪ͨ���ز������ɲο������ĵ���ҳ����תͬ��֪ͨ�����б�(���½����ο�)//
	String trade_no = request.getParameter("trade_no");				//֧�������׺�
	String order_no = request.getParameter("out_trade_no");	        //��ȡ������
	String total_fee = request.getParameter("total_fee");	        //��ȡ�ܽ��
	String subject = new String(request.getParameter("subject").getBytes("ISO-8859-1"),"gbk");//��Ʒ���ơ���������
	String body = "";
	if(request.getParameter("body") != null){
		body = new String(request.getParameter("body").getBytes("ISO-8859-1"), "gbk");//��Ʒ������������ע������
	}
	String buyer_email = request.getParameter("buyer_email");		//���֧�����˺�
	String trade_status = request.getParameter("trade_status");		//����״̬

	//��ȡ֧������֪ͨ���ز������ɲο������ĵ���ҳ����תͬ��֪ͨ�����б�(���Ͻ����ο�)//
	
	//����ó�֪ͨ��֤���
	boolean verify_result = AlipayNotify.verify(params);
	
	if(verify_result){//��֤�ɹ�
		//////////////////////////////////////////////////////////////////////////////////////////
		//������������̻���ҵ���߼��������

		if(trade_status.equals("TRADE_FINISHED") || trade_status.equals("TRADE_SUCCESS")){
			//�жϸñʶ����Ƿ����̻���վ���Ѿ���������
				//���û�������������ݶ����ţ�out_trade_no�����̻���վ�Ķ���ϵͳ�в鵽�ñʶ�������ϸ����ִ���̻���ҵ�����
				//���������������ִ���̻���ҵ�����
		}
		
		//��ҳ�����ҳ�������༭
		out.println("��֤�ɹ�<br />");
		out.println("trade_no=" + trade_no);
		//�������������ҵ���߼�����д�������ϴ�������ο�������

		//////////////////////////////////////////////////////////////////////////////////////////
	}else{
		//��ҳ�����ҳ�������༭
		out.println("��֤ʧ��");
	}
%>
  </body>
</html>
