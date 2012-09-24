<%
' 支付宝接口公用函数
' 详细：该类是请求、通知返回两个文件所调用的公用函数核心处理文件
' 版本：3.2
' 修改日期：2011-03-31
' 说明：
' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
%>

<!--#include file="alipay_md5.asp"-->

<%

''
' 生成签名结果
' param sPara 要签名的数组
' param key 安全校验码
' param sign_type 签名类型
' return 签名结果字符串
Function BuildMysign(sPara, key, sign_type,input_charset)
	prestr = CreateLinkstring(sPara)		'把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
	
    prestr = prestr & key					'把拼接后的字符串再与安全校验码直接连接起来

    mysign = Sign(prestr,sign_type,input_charset)	'把最终的字符串签名，获得签名结果

    BuildMysign = mysign
End Function

''
' 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
' param sPara 需要拼接的数组
' return 拼接完成以后的字符串
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
' 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并且做urlencode编码
' param sPara 需要拼接的数组
' return 拼接完成以后的字符串
function CreateLinkStringUrlEncode(sPara)
	nCount = ubound(sPara)
	dim prestr
	for i = 0 to nCount
		'把sPara的数组里的元素格式：变量名=值，分割开来
		pos = Instr(sPara(i),"=")			'获得=字符的位置
		nLen = Len(sPara(i))				'获得字符串长度
		itemName = left(sPara(i),pos-1)	'获得变量名
		itemValue = right(sPara(i),nLen-pos)'获得变量的值
		
		if itemName <> "service" and itemName <> "_input_charset" then
			prestr = prestr & itemName &"=" & server.URLEncode(itemValue) & "&"
		else
			prestr = prestr & sPara(i) & "&"
		end if
	next
	
	CreateLinkStringUrlEncode = prestr
end function

''
' 除去数组中的空值和签名参数
' param sPara 签名参数组
' return 去掉空值与签名参数后的新签名参数组
Function FilterPara(sPara)
	Dim sParaFilter(),nCount,j
	nCount = ubound(sPara)
	j = 0
	For i = 0 To nCount
		'把sPara的数组里的元素格式：变量名=值，分割开来
		pos = Instr(sPara(i),"=")			'获得=字符的位置
		nLen = Len(sPara(i))				'获得字符串长度
		itemName = left(sPara(i),pos-1)	'获得变量名
		itemValue = right(sPara(i),nLen-pos)'获得变量的值
		
		If itemName <> "sign" And itemName <> "sign_type" And itemValue <> "" and isnull(itemValue) = false Then
			Redim Preserve sParaFilter(j)
			sParaFilter(j) = sPara(i)
			j = j + 1
		End If
	Next
	
	FilterPara = sParaFilter
End Function

''
' 对数组排序
' param sPara 排序前的数组
' return 排序后的数组
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
' 签名字符串
' param prestr 需要签名的字符串
' param sign_type 签名类型
' return 签名结果
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
' 写日志，方便测试（看网站需求，也可以改成存入数据库）
' param sWord 要写入日志里的文本内容
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
' 获取当前时间
' 格式：年[4位]-月[2位]-日[2位] 小时[2位 24小时制]:分[2位]:秒[2位]，如：2007-10-01 13:13:13
' return 时间格式化结果
Function GetDateTimeFormat()
	sTime=now()
	sResult	= year(sTime)&"-"&right("0" & month(sTime),2)&"-"&right("0" & day(sTime),2)&" "&right("0" & hour(sTime),2)&":"&right("0" & minute(sTime),2)&":"&right("0" & second(sTime),2)
	GetDateTimeFormat = sResult
End Function

''
' 获取当前时间
' 格式：年[4位]月[2位]日[2位]小时[2位 24小时制]分[2位]秒[2位]，如：20071001131313
' return 时间格式化结果
Function GetDateTime()
	sTime=now()
	sResult	= year(sTime)&right("0" & month(sTime),2)&right("0" & day(sTime),2)&right("0" & hour(sTime),2)&right("0" & minute(sTime),2)&right("0" & second(sTime),2)
	GetDateTime = sResult
End Function

''
' 过滤特殊字符
' param Str 要被过滤的字符串
' return 已被过滤掉的新字符串
Function DelStr(Str)
	If IsNull(Str) Or IsEmpty(Str) Then
		Str	= ""
	End If
	DelStr	= Replace(Str,";","")
	DelStr	= Replace(DelStr,"'","")
	DelStr	= Replace(DelStr,"&","")
	DelStr	= Replace(DelStr," ","")
	DelStr	= Replace(DelStr,"　","")
	DelStr	= Replace(DelStr,"%20","")
	DelStr	= Replace(DelStr,"--","")
	DelStr	= Replace(DelStr,"==","")
	DelStr	= Replace(DelStr,"<","")
	DelStr	= Replace(DelStr,">","")
	DelStr	= Replace(DelStr,"%","")
End Function

'*************************************************************************************************
%>