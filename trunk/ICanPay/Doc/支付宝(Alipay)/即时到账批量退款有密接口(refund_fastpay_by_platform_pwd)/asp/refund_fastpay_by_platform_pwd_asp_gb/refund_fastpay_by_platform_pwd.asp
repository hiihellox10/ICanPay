<%
' ���ܣ���ʱ���������˿����ܽӿڽ���ҳ
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

'�˿�����ڣ���ȡ�������ڣ���ʽ����[4λ]-��[2λ]-��[2λ] Сʱ[2λ 24Сʱ��]:��[2λ]:��[2λ]���磺2007-10-01 13:13:13
refund_date	= GetDateTimeFormat()

'�̼���վ������κţ���֤��Ψһ�ԣ���ʽ����������[8λ]+���к�[3��24λ]���磺201008010000001
batch_no	= GetDateTime()

'�˿������������detail_data��ֵ�У���#���ַ����ֵ�������1�����֧��1000�ʣ�����#���ַ����ֵ�����999����
batch_num	= request.Form("batch_num")

'�˿���ϸ����
detail_data	= request.Form("detail_data")
'��ʽ����һ�ʽ���#�ڶ��ʽ���#�����ʽ���
'��N�ʽ��׸�ʽ�������˿���Ϣ
'�����˿���Ϣ��ʽ��ԭ����֧�������׺�^�˿��ܽ��^�˿�����
'ע�⣺
'1.detail_data�е��˿�����ܺ�Ҫ���ڲ���batch_num��ֵ
'2.detail_data��ֵ�в����С�^������|������$������#����Ӱ��detail_data�ĸ�ʽ�������ַ�
'3.detail_data���˿��ܽ��ܴ��ڽ����ܽ��
'4.һ�ʽ��׿��Զ���˿ֻ��Ҫ���ض���˿���ܽ������ñʽ��׸���ʱ��
'5.��֧���˷�����

'/////////////////////�������/////////////////////

'���������������
sParaTemp = Array("service=refund_fastpay_by_platform_pwd","partner="&partner,"seller_email="&seller_email,"notify_url="&notify_url,"refund_date="&refund_date,"batch_no="&batch_no,"batch_num="&batch_num,"detail_data="&detail_data,"_input_charset="&input_charset)

'���켴ʱ���������˿����ܽӿڱ��ύHTML���ݣ������޸�
Set objService = New AlipayService
sHtml = objService.Refund_fastpay_by_platform_pwd(sParaTemp)
response.Write sHtml
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>֧������ʱ���������˿�����</title>
</head>
<body>
</body>
</html>
