<%
' 类名：AlipayService
' 功能：支付宝各接口构造类
' 详细：构造支付宝各接口请求参数
' 版本：3.2
' 修改日期：2011-03-31
' 说明：
' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考
%>

<!--#include file="alipay_config.asp"-->
<!--#include file="alipay_submit.asp"-->

<%
'支付宝网关地址（新）
GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?"

Class AlipayService
	''
	' 构造即时到帐接口
	' sParaTemp 请求参数数组
	' return 表单提交HTML信息
	Public Function Create_direct_pay_by_user(sParaTemp)
		Dim sButtonValue, sHtml
		
		'确认按钮显示文字
		sButtonValue = "确认"
		
		'构造表单提交HTML数据
		Set  objSubmit = New AlipaySubmit
		sHtml = objSubmit.BuildFormHtml(sParaTemp, key, sign_type, input_charset, GATEWAY_NEW, "get", sButtonValue)
		
		Create_direct_pay_by_user = sHtml
	End Function

	''
	' 用于防钓鱼，调用支付宝防钓鱼接口(query_timestamp)来获取时间戳的处理函数
	' 注意：远程解析XML出错，与IIS服务器配置有关
	' return 时间戳字符串
	Public Function Query_timestamp()
		Dim sUrl, encrypt_key
		sUrl = GATEWAY_NEW&"service=query_timestamp&partner="&partner
		encrypt_key = ""
		
		Dim objHttp, objXml
		Set objHttp=Server.CreateObject("Microsoft.XMLHTTP")
		'如果Microsoft.XMLHTTP不行，那么请替换下面的两行行代码尝试
		'Set objHttp = Server.CreateObject("Msxml2.ServerXMLHTTP.3.0")
		'objHttp.setOption 2, 13056
		objHttp.open "GET", sUrl, False, "", ""
		objHttp.send()
		Set objXml=Server.CreateObject("Microsoft.XMLDOM")
		objXml.Async=true
		objXml.ValidateOnParse=False
		objXml.Load(objHttp.ResponseXML)
		Set objHttp = Nothing
		
		Set objXmlData = objXml.getElementsByTagName("encrypt_key")  '节点的名称
		If Isnull(objXml.getElementsByTagName("encrypt_key")) Then
			encrypt_key = ""
		Else
			encrypt_key = objXmlData.item(0).childnodes(0).text
		End If

		Query_timestamp = encrypt_key
	End Function
	
	'******************若要增加其他支付宝接口，可以按照下面的格式定义******************

	''
	' 构造(支付宝接口名称)接口
	' param sParaTemp 请求参数数组
	' return 表单提交HTML文本或者支付宝返回XML处理结果
	Public Function Alipay_interface(sParaTemp)
		
		'构造给支付宝处理的请求
		Set  objSubmit = New AlipaySubmit
		'请求方式有以下三种：
		'1.构造表单提交HTML数据：（sMethod可赋值为get或post）：
		'sHtml = objSubmit.BuildFormHtml(sParaTemp, key, sign_type, input_charset, gateway, sMethod, sButtonValue)
		'2.构造模拟远程HTTP的GET请求，获取支付宝的返回XML处理结果：
		'sHtml = objSubmit.SendGetInfo(sParaTemp, key, sign_type, input_charset, gateway, sParaNode)
		'请根据不同的接口特性二选一
	End Function

End Class

%>