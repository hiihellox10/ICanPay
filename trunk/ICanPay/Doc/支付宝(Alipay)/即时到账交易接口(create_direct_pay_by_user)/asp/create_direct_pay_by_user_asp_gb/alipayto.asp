<%
' ���ܣ���ʱ���ʽӿڽ���ҳ
' �汾��3.2
' ���ڣ�2011-03-31
' ˵����
' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
' �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
	
' /////////////////ע��/////////////////
' ������ڽӿڼ��ɹ������������⣬���԰��������;�������
' 1���̻��������ģ�https://b.alipay.com/support/helperApply.htm?action=consultationApply�����ύ���뼯��Э�������ǻ���רҵ�ļ�������ʦ������ϵ��Э�����
' 2���̻��������ģ�http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9��
' 3��֧������̳��http://club.alipay.com/read-htm-tid-8681712.html��
' /////////////////////////////////////

%>

<!--#include file="class/alipay_service.asp"-->

<%
'/////////////////////�������/////////////////////
'//�������//

'�������վ����ϵͳ�е�Ψһ������ƥ��
out_trade_no = GetDateTime()
'�������ƣ���ʾ��֧��������̨��ġ���Ʒ���ơ����ʾ��֧�����Ľ��׹���ġ���Ʒ���ơ����б��
subject      = request.Form("subject")
'����������������ϸ��������ע����ʾ��֧��������̨��ġ���Ʒ��������
body         = request.Form("alibody")
'�����ܽ���ʾ��֧��������̨��ġ�Ӧ���ܶ��
total_fee    = request.Form("total_fee")

'//��չ���ܲ�������Ĭ��֧����ʽ//

'Ĭ��֧����ʽ�����������ʱ���ʽӿڡ������ĵ�
paymethod = ""
'Ĭ���������ţ������б������ʱ���ʽӿڡ������ĵ�����¼�����������б�
defaultbank = ""

'//��չ���ܲ�������������//

'��ȡ�ͻ��˵�IP��ַ�����飺��д��ȡ�ͻ���IP��ַ�ĳ���
exter_invoke_ip   = ""
'������ʱ���
anti_phishing_key = ""
'ע�⣺
'������ѡ���Ƿ��������㹦��
'exter_invoke_ip��anti_phishing_keyһ�������ù�����ô���Ǿͻ��Ϊ�������
'��Ҫʹ�÷����㹦�ܣ�����ʹ��POST��ʽ��������
'ʾ����
'exter_invoke_ip = "202.1.1.1"
'Set objQuery_timestamp = New AlipayService
'anti_phishing_key = objQuery_timestamp.Query_timestamp()		'��ȡ������ʱ�������

'//��չ���ܲ�����������//

'��Ʒչʾ��ַ��Ҫ��http:// ��ʽ������·�����������?id=123�����Զ������
show_url = "http://www.xxx.com/myorder.asp"
'�Զ���������ɴ���κ����ݣ���=��&�������ַ��⣩��������ʾ��ҳ����
extra_common_param = ""

'//��չ���ܲ�����������(��Ҫʹ�ã��밴��ע��Ҫ��ĸ�ʽ��ֵ)//

'������ͣ���ֵΪ�̶�ֵ��10������Ҫ�޸�
royalty_type		= ""
'�����Ϣ��
royalty_parameters	= ""
'ע�⣺
'����Ҫ����̻���վ���������̬��ȡÿ�ʽ��׵ĸ������տ��˺š��������������˵�������ֻ������10��
'����������ܺ���С�ڵ���total_fee
'�����Ϣ����ʽΪ���տEmail_1^���1^��ע1|�տEmail_2^���2^��ע2
'ʾ����
'royalty_type = "10"
'royalty_parameters	= "111@126.com^0.01^����עһ|222@126.com^0.01^����ע��"

'/////////////////////�������/////////////////////

'���������������
sParaTemp = Array("service=create_direct_pay_by_user","payment_type=1","partner="&partner,"seller_email="&seller_email,"return_url="&return_url,"notify_url="&notify_url,"_input_charset="&input_charset,"show_url="&show_url,"out_trade_no="&out_trade_no,"subject="&subject,"body="&body,"total_fee="&total_fee,"paymethod="&paymethod,"defaultbank="&defaultbank,"anti_phishing_key="&anti_phishing_key,"exter_invoke_ip="&exter_invoke_ip,"extra_common_param="&extra_common_param,"royalty_type="&royalty_type,"royalty_parameters="&royalty_parameters)

'���켴ʱ���ʽӿڱ��ύHTML���ݣ������޸�
Set objService = New AlipayService
sHtml = objService.Create_direct_pay_by_user(sParaTemp)
response.Write sHtml
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>֧������ʱ����</title>
</head>
<body>
</body>
</html>
