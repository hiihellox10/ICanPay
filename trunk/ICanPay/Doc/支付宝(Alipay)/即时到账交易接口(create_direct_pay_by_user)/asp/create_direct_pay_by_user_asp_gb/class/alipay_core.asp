<%
' ֧�����ӿڹ��ú���
' ��ϸ������������֪ͨ���������ļ������õĹ��ú������Ĵ����ļ�
' �汾��3.2
' �޸����ڣ�2011-03-31
' ˵����
' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
' �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
%>

<!--#include file="alipay_md5.asp"-->

<%

''
' ����ǩ�����
' param sPara Ҫǩ��������
' param key ��ȫУ����
' param sign_type ǩ������
' return ǩ������ַ���
Function BuildMysign(sPara, key, sign_type,input_charset)
	prestr = CreateLinkstring(sPara)		'����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
	
    prestr = prestr & key					'��ƴ�Ӻ���ַ������밲ȫУ����ֱ����������

    mysign = Sign(prestr,sign_type,input_charset)	'�����յ��ַ���ǩ�������ǩ�����

    BuildMysign = mysign
End Function

''
' ����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
' param sPara ��Ҫƴ�ӵ�����
' return ƴ������Ժ���ַ���
Function CreateLinkstring(sPara)
	nCount = ubound(sPara)
	Dim prestr
	For i = 0 To nCount
		If i = nCount Then
			prestr = prestr & sPara(i)
		Else
			prestr = prestr & sPara(i) & "&"
		End if
	Next
	
	CreateLinkstring = prestr
End Function

''
' ����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ�����������urlencode����
' param sPara ��Ҫƴ�ӵ�����
' return ƴ������Ժ���ַ���
function CreateLinkStringUrlEncode(sPara)
	nCount = ubound(sPara)
	dim prestr
	for i = 0 to nCount
		'��sPara���������Ԫ�ظ�ʽ��������=ֵ���ָ��
		pos = Instr(sPara(i),"=")			'���=�ַ���λ��
		nLen = Len(sPara(i))				'����ַ�������
		itemName = left(sPara(i),pos-1)	'��ñ�����
		itemValue = right(sPara(i),nLen-pos)'��ñ�����ֵ
		
		if itemName <> "service" and itemName <> "_input_charset" then
			prestr = prestr & itemName &"=" & server.URLEncode(itemValue) & "&"
		else
			prestr = prestr & sPara(i) & "&"
		end if
	next
	
	CreateLinkStringUrlEncode = prestr
end function

''
' ��ȥ�����еĿ�ֵ��ǩ������
' param sPara ǩ��������
' return ȥ����ֵ��ǩ�����������ǩ��������
Function FilterPara(sPara)
	Dim sParaFilter(),nCount,j
	nCount = ubound(sPara)
	j = 0
	For i = 0 To nCount
		'��sPara���������Ԫ�ظ�ʽ��������=ֵ���ָ��
		pos = Instr(sPara(i),"=")			'���=�ַ���λ��
		nLen = Len(sPara(i))				'����ַ�������
		itemName = left(sPara(i),pos-1)	'��ñ�����
		itemValue = right(sPara(i),nLen-pos)'��ñ�����ֵ
		
		If itemName <> "sign" And itemName <> "sign_type" And itemValue <> "" and isnull(itemValue) = false Then
			Redim Preserve sParaFilter(j)
			sParaFilter(j) = sPara(i)
			j = j + 1
		End If
	Next
	
	FilterPara = sParaFilter
End Function

''
' ����������
' param sPara ����ǰ������
' return ����������
Function SortPara(sPara)
	Dim nCount
	nCount = ubound(sPara)
	For i = nCount To 0 Step -1
		minmax = sPara( 0 )
    	minmaxSlot = 0
    	For j = 1 To i
            mark = (sPara( j ) > minmax)
        	If mark Then 
            	minmax = sPara( j )
            	minmaxSlot = j
        	End If
    	Next
		If minmaxSlot <> i Then 
			temp = sPara( minmaxSlot )
			sPara( minmaxSlot ) = sPara( i )
			sPara( i ) = temp
		End If
	Next
	SortPara = sPara
end function

''
' ǩ���ַ���
' param prestr ��Ҫǩ�����ַ���
' param sign_type ǩ������
' return ǩ�����
Function Sign(prestr,sign_type,input_charset)
	Dim sResult
	If sign_type = "MD5" Then
		sResult = md5(prestr,input_charset)
	Else 
		sResult = ""
	End If
	Sign = sResult
End Function

''
' д��־��������ԣ�����վ����Ҳ���Ըĳɴ������ݿ⣩
' param sWord Ҫд����־����ı�����
Function LogResult(sWord)
	Randomize
	Set fs= createobject("scripting.filesystemobject")
	Set ts=fs.createtextfile(server.MapPath("log/"&GetDateTime()&INT((1000+1)*RND)&".txt"),true)
	ts.writeline(sWord)
	ts.close
	Set ts=Nothing
	Set fs=Nothing
End Function

''
' ��ȡ��ǰʱ��
' ��ʽ����[4λ]-��[2λ]-��[2λ] Сʱ[2λ 24Сʱ��]:��[2λ]:��[2λ]���磺2007-10-01 13:13:13
' return ʱ���ʽ�����
Function GetDateTimeFormat()
	sTime=now()
	sResult	= year(sTime)&"-"&right("0" & month(sTime),2)&"-"&right("0" & day(sTime),2)&" "&right("0" & hour(sTime),2)&":"&right("0" & minute(sTime),2)&":"&right("0" & second(sTime),2)
	GetDateTimeFormat = sResult
End Function

''
' ��ȡ��ǰʱ��
' ��ʽ����[4λ]��[2λ]��[2λ]Сʱ[2λ 24Сʱ��]��[2λ]��[2λ]���磺20071001131313
' return ʱ���ʽ�����
Function GetDateTime()
	sTime=now()
	sResult	= year(sTime)&right("0" & month(sTime),2)&right("0" & day(sTime),2)&right("0" & hour(sTime),2)&right("0" & minute(sTime),2)&right("0" & second(sTime),2)
	GetDateTime = sResult
End Function

''
' ���������ַ�
' param Str Ҫ�����˵��ַ���
' return �ѱ����˵������ַ���
Function DelStr(Str)
	If IsNull(Str) Or IsEmpty(Str) Then
		Str	= ""
	End If
	DelStr	= Replace(Str,";","")
	DelStr	= Replace(DelStr,"'","")
	DelStr	= Replace(DelStr,"&","")
	DelStr	= Replace(DelStr," ","")
	DelStr	= Replace(DelStr,"��","")
	DelStr	= Replace(DelStr,"%20","")
	DelStr	= Replace(DelStr,"--","")
	DelStr	= Replace(DelStr,"==","")
	DelStr	= Replace(DelStr,"<","")
	DelStr	= Replace(DelStr,">","")
	DelStr	= Replace(DelStr,"%","")
End Function

'*************************************************************************************************
%>