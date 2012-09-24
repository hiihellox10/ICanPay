<%
' 类名：AlipayNotify
' 功能：支付宝通知处理类
' 详细：处理支付宝各接口通知返回
' 版本：3.2
' 修改日期：2011-03-31
' 说明：
' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
 
' ////////////////////////注意////////////////////////
' 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常
' ////////////////////////////////////////////////////
%>

<!--#include file="alipay_config.asp"-->
<!--#include file="alipay_core.asp"-->

<%
HTTPS_VERIFY_URL = "https://mapi.alipay.com/gateway.do?service=notify_verify&"

Class AlipayNotify

	''
	' 针对return_url验证消息是否是支付宝发出的合法消息
	' return 验证结果：true/false
	Public Function VerifyReturn()
		Dim mysign, sResponseTxt, url
		'获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
		sParaTemp = GetRequestGet()
		
		'验证是否有数组传来
		If IsArray(sParaTemp) Then
			'生成签名结果
			mysign = GetMysign(sParaTemp)
			
			'获取支付宝远程服务器ATN结果（验证是否是支付宝发来的消息）
			sResponseTxt = "true"
			If Request.QueryString("notify_id") <> "" Then
				sResponseTxt = GetResponse(Request.QueryString("notify_id"))
			End If
			
			'写日志记录（若要调试，请取消下面两行注释）
			'sWord = "responseTxt="& sResponseTxt &"\n return_url_log:sign="&request.QueryString("sign")&"&mysign="&mysign&"&"&CreateLinkstring(sParaTemp)
			'LogResult(sWord)
			
			'验证
			'responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
			'mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
			If mysign = Request.QueryString("sign") And sResponseTxt = "true" Then
				VerifyReturn = true
			Else
				VerifyReturn = false
			End If
		Else
			VerifyReturn = false
		End If
	End Function

	''
	' 针对notify_url验证消息是否是支付宝发出的合法消息
	' return 验证结果：true/false
	Public Function VerifyNotify()
		Dim mysign, sResponseTxt, url
		'获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
		sParaTemp = GetRequestPost()
		
		'验证是否有数组传来
		If IsArray(sParaTemp) Then
			'生成签名结果
			mysign = GetMysign(sParaTemp)
			
			'获取支付宝远程服务器ATN结果（验证是否是支付宝发来的消息）
			sResponseTxt = "true"
			If Request.Form("notify_id") <> "" Then
				sResponseTxt = GetResponse(Request.Form("notify_id"))
			End If
			
			'写日志记录（若要调试，请取消下面两行注释）
			sWord = "responseTxt="& sResponseTxt &"\n notify_url_log:sign="&request.Form("sign")&"&mysign="&mysign&"&"&CreateLinkstring(sParaTemp)
			LogResult(sWord)
			
			'验证
			'responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
			'mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
			If mysign = request.Form("sign") And sResponseTxt = "true" Then
				VerifyNotify = true
			Else
				VerifyNotify = false
			End If
		Else
			VerifyNotify = false
		End If
	End Function

	''
	'根据反馈回来的信息，生成签名结果
	'param sParaTemp 通知返回来的参数数组
	'return 生成的签名结果
	Private Function GetMysign(sParaTemp)
		Dim mysign
		'过滤签名参数数组
		sPara = FilterPara(sParaTemp)
		
		'对请求参数数组排序
		sParaSort = SortPara(sPara)
		
		'获得签名结果
		mysign = BuildMysign(sParaSort, key, sign_type, input_charset)
		
		GetMysign = mysign
	End Function

	''
	' 获取远程服务器ATN结果
	' param notify_id 通知校验ID
	' return 服务器ATN结果字符串
	Private Function GetResponse(notify_id)
		Dim sUrl, objHttp, sResponseTxt
		sUrl = HTTPS_VERIFY_URL & "partner=" & partner & "&notify_id=" & notify_id
		
		Set objHttp = Server.CreateObject("Microsoft.XMLHTTP")
		'如果Microsoft.XMLHTTP不行，那么请替换下面的两行行代码尝试
		'Set objHttp = Server.CreateObject("Msxml2.ServerXMLHTTP.3.0")
		'objHttp.setOption 2, 13056
		objHttp.open "GET", sUrl, False, "", ""
		objHttp.send()
		sResponseTxt = objHttp.ResponseText
		Set objHttp = Nothing

		GetResponse = sResponseTxt
	End Function

	''
	'获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
	'return request回来的信息组成的数组
	Private Function GetRequestGet()
		Dim sPara(), i
		i = 0
		For Each varItem in Request.QueryString
			Redim Preserve sPara(i)
			sPara(i) = varItem&"="&Request(varItem)
			i = i + 1
		Next 
		
		If i = 0 Then	'验证是否有数组传来
			GetRequestGet = ""
		Else
			GetRequestGet = sPara
		End If
		
	End Function

	''
	'获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
	'return request回来的信息组成的数组
	Private Function GetRequestPost()
		Dim sPara(), i
		i = 0
		For Each varItem in Request.Form
			Redim Preserve sPara(i)
			sPara(i) = varItem&"="&Request(varItem) 
			i = i + 1
		Next 
		
		If i = 0 Then	'验证是否有数组传来
			GetRequestPost = ""
		Else
			GetRequestPost = sPara
		End If
	End Function

End Class

%>