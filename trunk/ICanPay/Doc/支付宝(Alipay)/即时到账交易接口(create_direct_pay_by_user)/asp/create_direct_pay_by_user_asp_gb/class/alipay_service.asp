<%
' ������AlipayService
' ���ܣ�֧�������ӿڹ�����
' ��ϸ������֧�������ӿ��������
' �汾��3.2
' �޸����ڣ�2011-03-31
' ˵����
' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
' �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο�
%>

<!--#include file="alipay_config.asp"-->
<!--#include file="alipay_submit.asp"-->

<%
'֧�������ص�ַ���£�
GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?"

Class AlipayService
	''
	' ���켴ʱ���ʽӿ�
	' sParaTemp �����������
	' return ���ύHTML��Ϣ
	Public Function Create_direct_pay_by_user(sParaTemp)
		Dim sButtonValue, sHtml
		
		'ȷ�ϰ�ť��ʾ����
		sButtonValue = "ȷ��"
		
		'������ύHTML����
		Set  objSubmit = New AlipaySubmit
		sHtml = objSubmit.BuildFormHtml(sParaTemp, key, sign_type, input_charset, GATEWAY_NEW, "get", sButtonValue)
		
		Create_direct_pay_by_user = sHtml
	End Function

	''
	' ���ڷ����㣬����֧����������ӿ�(query_timestamp)����ȡʱ����Ĵ�����
	' ע�⣺Զ�̽���XML������IIS�����������й�
	' return ʱ����ַ���
	Public Function Query_timestamp()
		Dim sUrl, encrypt_key
		sUrl = GATEWAY_NEW&"service=query_timestamp&partner="&partner
		encrypt_key = ""
		
		Dim objHttp, objXml
		Set objHttp=Server.CreateObject("Microsoft.XMLHTTP")
		'���Microsoft.XMLHTTP���У���ô���滻����������д��볢��
		'Set objHttp = Server.CreateObject("Msxml2.ServerXMLHTTP.3.0")
		'objHttp.setOption 2, 13056
		objHttp.open "GET", sUrl, False, "", ""
		objHttp.send()
		Set objXml=Server.CreateObject("Microsoft.XMLDOM")
		objXml.Async=true
		objXml.ValidateOnParse=False
		objXml.Load(objHttp.ResponseXML)
		Set objHttp = Nothing
		
		Set objXmlData = objXml.getElementsByTagName("encrypt_key")  '�ڵ������
		If Isnull(objXml.getElementsByTagName("encrypt_key")) Then
			encrypt_key = ""
		Else
			encrypt_key = objXmlData.item(0).childnodes(0).text
		End If

		Query_timestamp = encrypt_key
	End Function
	
	'******************��Ҫ��������֧�����ӿڣ����԰�������ĸ�ʽ����******************

	''
	' ����(֧�����ӿ�����)�ӿ�
	' param sParaTemp �����������
	' return ���ύHTML�ı�����֧��������XML������
	Public Function Alipay_interface(sParaTemp)
		
		'�����֧�������������
		Set  objSubmit = New AlipaySubmit
		'����ʽ���������֣�
		'1.������ύHTML���ݣ���sMethod�ɸ�ֵΪget��post����
		'sHtml = objSubmit.BuildFormHtml(sParaTemp, key, sign_type, input_charset, gateway, sMethod, sButtonValue)
		'2.����ģ��Զ��HTTP��GET���󣬻�ȡ֧�����ķ���XML��������
		'sHtml = objSubmit.SendGetInfo(sParaTemp, key, sign_type, input_charset, gateway, sParaNode)
		'����ݲ�ͬ�Ľӿ����Զ�ѡһ
	End Function

End Class

%>