<%
/* *
 ���ܣ�֧�����������첽֪ͨҳ��
 �汾��3.2
 ���ڣ�2011-03-17
 ˵����
 ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���

 //***********ҳ�湦��˵��***********
 ������ҳ���ļ�ʱ�������ĸ�ҳ���ļ������κ�HTML���뼰�ո�
 ��ҳ�治���ڱ������Բ��ԣ��뵽�������������ԡ���ȷ���ⲿ���Է��ʸ�ҳ�档
 ��ҳ����Թ�����ʹ��д�ı�����logResult���ú�����com.alipay.util�ļ��е�AlipayNotify.java���ļ���
 ���û���յ���ҳ�淵�ص� success ��Ϣ��֧��������24Сʱ�ڰ�һ����ʱ������ط�֪ͨ
 TRADE_FINISHED(��ʾ�����Ѿ��ɹ��������������ٶԸý�������������);
 TRADE_SUCCESS(��ʾ�����Ѿ��ɹ����������ԶԸý����������������磺�����˿��);
 //********************************
 * */
%>
<%@ page language="java" contentType="text/html; charset=gbk" pageEncoding="gbk"%>
<%@ page import="java.util.*"%>
<%@ page import="com.alipay.util.*"%>
<%@ page import="com.alipay.services.*"%>
<%@ page import="com.alipay.config.*"%>
<%
	//��ȡ֧����POST����������Ϣ
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
		//valueStr = new String(valueStr.getBytes("ISO-8859-1"), "gbk");
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

	if(AlipayNotify.verify(params)){//��֤�ɹ�
		//////////////////////////////////////////////////////////////////////////////////////////
		//������������̻���ҵ���߼��������

		//�������������ҵ���߼�����д�������´�������ο�������
		
		if(trade_status.equals("TRADE_FINISHED")){
			//�жϸñʶ����Ƿ����̻���վ���Ѿ���������
				//���û�������������ݶ����ţ�out_trade_no�����̻���վ�Ķ���ϵͳ�в鵽�ñʶ�������ϸ����ִ���̻���ҵ�����
				//���������������ִ���̻���ҵ�����
				
			//ע�⣺
			//���ֽ���״ֻ̬����������³���
			//1����ͨ����ͨ��ʱ���ˣ���Ҹ���ɹ���
			//2����ͨ�˸߼���ʱ���ˣ��Ӹñʽ��׳ɹ�ʱ�����𣬹���ǩԼʱ�Ŀ��˿�ʱ�ޣ��磺���������ڿ��˿һ�����ڿ��˿�ȣ���
		} else if (trade_status.equals("TRADE_SUCCESS")){
			//�жϸñʶ����Ƿ����̻���վ���Ѿ���������
				//���û�������������ݶ����ţ�out_trade_no�����̻���վ�Ķ���ϵͳ�в鵽�ñʶ�������ϸ����ִ���̻���ҵ�����
				//���������������ִ���̻���ҵ�����
				
			//ע�⣺
			//���ֽ���״ֻ̬��һ������³��֡�����ͨ�˸߼���ʱ���ˣ���Ҹ���ɹ���
		}

		//�������������ҵ���߼�����д�������ϴ�������ο�������
			
		out.println("success");	//�벻Ҫ�޸Ļ�ɾ��

		//////////////////////////////////////////////////////////////////////////////////////////
	}else{//��֤ʧ��
		out.println("fail");
	}
%>
