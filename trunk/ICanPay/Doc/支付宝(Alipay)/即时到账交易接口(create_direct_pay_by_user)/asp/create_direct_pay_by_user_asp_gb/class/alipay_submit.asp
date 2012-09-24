<%
' ������AlipaySubmit
' ���ܣ�֧�������ӿ������ύ��
' ��ϸ������֧�������ӿڱ�HTML�ı�����ȡԶ��HTTP����
' �汾��3.2
' �޸����ڣ�2011-03-31
' ˵����
' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
' �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο�
%>

<!--#include file="alipay_core.asp"-->

<%

Class AlipaySubmit

	''
	' ����Ҫ�����֧�����Ĳ�������
	' param sParaTemp ����ǰ�Ĳ�������
	' param key ���װ�ȫУ����
	' param sign_type ǩ������
	' param input_charset �����ʽ
	' return Ҫ����Ĳ�������
	Private Function BuildRequestPara(sParaTemp, key, sign_type, input_charset)
		Dim mysign
		'����ǩ����������
		sPara = FilterPara(sParaTemp)
		
		'�����������������
		sParaSort = SortPara(sPara)
		
		'���ǩ�����
		mysign = BuildMysign(sParaSort, key, sign_type, input_charset)
		
		'ǩ�������ǩ����ʽ���������ύ��������
		nCount = ubound(sParaSort)
		Redim Preserve sParaSort(nCount+1)
		sParaSort(nCount+1) = "sign="&mysign
		Redim Preserve sParaSort(nCount+2)
		sParaSort(nCount+2) = "sign_type="&sign_type

		BuildRequestPara = sParaSort
	End Function
	
	''
	' ����Ҫ�����֧�����Ĳ��������ַ���
	' param sParaTemp ����ǰ�Ĳ�������
	' param key ���װ�ȫУ����
	' param sign_type ǩ������
	' param input_charset �����ʽ
	' return Ҫ����Ĳ��������ַ���
	Private Function BuildRequestParaToString(sParaTemp, key, sign_type, input_charset)
		Dim sRequestData
		'��ǩ�������������
		sPara = BuildRequestPara(sParaTemp, key, sign_type, input_charset)
		'�Ѳ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ��������Ҷ�����urlencode���봦��
		sRequestData = CreateLinkStringUrlEncode(sPara)
		
		BuildRequestParaToString = sRequestData
	End Function

	''
	' �����ύ��HTML����
	' param sParaTemp ����ǰ�Ĳ�������
	' param key ���װ�ȫУ����
	' param sign_type ǩ������
	' param input_charset �����ʽ
	' param gateway ���ص�ַ
	' param sMethod �ύ��ʽ������ֵ��ѡ��post��get
	' param sButtonValue ȷ�ϰ�ť��ʾ����
	' return �ύ��HTML�ı�
	Public Function BuildFormHtml(sParaTemp, key, sign_type, input_charset, gateway, sMethod, sButtonValue)
		Dim sHtml, nCount
		'�������������
		sPara = BuildRequestPara(sParaTemp, key, sign_type, input_charset)
		
		sHtml = "<form id='alipaysubmit' name='alipaysubmit' action='"& gateway &"_input_charset="&input_charset&"' method='"&sMethod&"'>"
		
		nCount = ubound(sPara)
		For i = 0 To nCount
			'��sPara���������Ԫ�ظ�ʽ��������=ֵ���ָ��
			iPos = Instr(sPara(i),"=")			'���=�ַ���λ��
			nLen = Len(sPara(i))				'����ַ�������
			sItemName = left(sPara(i),iPos-1)	'��ñ�����
			sItemValue = right(sPara(i),nLen-iPos)'��ñ�����ֵ
		
			sHtml = sHtml & "<input type='hidden' name='"& sItemName &"' value='"& sItemValue &"'/>"
		next

		'submit��ť�ؼ��벻Ҫ����name����
		'submit��ťĬ������Ϊ����ʾ
		sHtml = sHtml & "<input type='submit' value='"&sButtonValue&"' style='display:none;'></form>"
		
		sHtml = sHtml & "<script>document.forms['alipaysubmit'].submit();</script>"
		
		BuildFormHtml = sHtml
	End Function
	
	''
	' ����ģ��Զ��HTTP��GET���󣬻�ȡ֧�����ķ���XML������
	' param sParaTemp ����ǰ�Ĳ�������
	' param key ���װ�ȫУ����
	' param sign_type ǩ������
	' param input_charset �����ʽ
	' param gateway ���ص�ַ
	' param sParaNode Ҫ�����XML�ڵ���
	' return ֧��������XMLָ���ڵ�����
	Public Function SendGetInfo(sParaTemp, key, sign_type, input_charset, gateway, sParaNode)
		Dim sUrl, objHttp, objXml, nCount, sParaXml()
		nCount = ubound(sParaNode)
		
		'��������������ַ���
		sRequestData = BuildRequestParaToString(sParaTemp, key, sign_type, input_charset)
		'���������ַ
		sUrl = gateway & sRequestData

		'��ȡԶ������
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
		
		set objXmlData = objXml.getElementsByTagName("alipay").item(0)
		If Isnull(objXmlData.selectSingleNode("is_success")) Then
			Redim Preserve sParaXml(1)
			sParaXml(0) = "���󣺷Ƿ�XML��ʽ����"
		Else
			If objXmlData.selectSingleNode("is_success").text = "T" Then
				For i = 0 To nCount
					Redim Preserve sParaXml(i+1)
					sParaXml(i) = objXmlData.selectSingleNode(sParaNode(i)).text
				Next
			Else
				Redim Preserve sParaXml(1)
				sParaXml(0) = "����"&objXmlData.selectSingleNode("error").text
			End If
		End If
		
		SendGetInfo = sParaXml
	End Function

End Class

%>