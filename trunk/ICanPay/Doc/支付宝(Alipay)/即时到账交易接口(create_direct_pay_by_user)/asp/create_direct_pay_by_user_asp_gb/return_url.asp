<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<%
' ���ܣ�֧����ҳ����תͬ��֪ͨҳ��
' �汾��3.2
' ���ڣ�2011-03-31
' ˵����
' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
' �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
	
' //////////////ҳ�湦��˵��//////////////
' ��ҳ����ڱ������Բ���
' �ɷ���HTML������ҳ��Ĵ��롢�̻�ҵ���߼��������
' ��ҳ�����ʹ��ASP�������ߵ��ԣ�Ҳ����ʹ��д�ı�����LogResult���е��ԣ��ú����ѱ�Ĭ�Ϲرգ���alipay_notify.asp�еĺ���VerifyReturn
' TRADE_FINISHED(��ʾ�����Ѿ��ɹ��������������ٶԸý�������������);
' TRADE_SUCCESS(��ʾ�����Ѿ��ɹ����������ԶԸý����������������磺�����˿��);
'////////////////////////////////////////
%>

<!--#include file="class/alipay_notify.asp"-->

<%
'����ó�֪ͨ��֤���
Set objNotify = New AlipayNotify
sVerifyResult = objNotify.VerifyReturn()

If sVerifyResult Then	'��֤�ɹ�
	'*********************************************************************
	'������������̻���ҵ���߼��������

	'�������������ҵ���߼�����д�������´�������ο�������
    '��ȡ֧������֪ͨ���ز������ɲο������ĵ���ҳ����תͬ��֪ͨ�����б�
    out_trade_no	= Request.QueryString("out_trade_no")	'��ȡ������
    trade_no		= Request.QueryString("trade_no")		'��ȡ֧�������׺�
    total_fee		= Request.QueryString("total_fee")		'��ȡ�ܽ��
	
	If Request.QueryString("trade_status") = "TRADE_FINISHED" or Request.QueryString("trade_status") = "TRADE_SUCCESS" Then
	'�ж��Ƿ����̻���վ���Ѿ����������֪ͨ���صĴ���
		'���û������������ôִ���̻���ҵ�����
		'���������������ô��ִ���̻���ҵ�����
	Else
		Response.Write "trade_status="&Request.QueryString("trade_status")
	End If

	Response.Write "��֤�ɹ�<br>"
	Response.Write "trade_no="&trade_no

	'�������������ҵ���߼�����д�������ϴ�������ο�������
	
	'*********************************************************************
Else '��֤ʧ��
    '��Ҫ���ԣ��뿴alipay_notify.aspҳ���VerifyReturn�������ȶ�sign��mysign��ֵ�Ƿ���ȣ����߼��responseTxt��û�з���true
    Response.Write "��֤ʧ��"
End If
%>
<title>֧������ʱ����</title>
</head>
<body>
</body>
</html>
