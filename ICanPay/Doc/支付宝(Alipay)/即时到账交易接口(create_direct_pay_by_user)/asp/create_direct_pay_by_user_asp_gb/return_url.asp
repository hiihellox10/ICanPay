<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<%
' 功能：支付宝页面跳转同步通知页面
' 版本：3.2
' 日期：2011-03-31
' 说明：
' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
	
' //////////////页面功能说明//////////////
' 该页面可在本机电脑测试
' 可放入HTML等美化页面的代码、商户业务逻辑程序代码
' 该页面可以使用ASP开发工具调试，也可以使用写文本函数LogResult进行调试，该函数已被默认关闭，见alipay_notify.asp中的函数VerifyReturn
' TRADE_FINISHED(表示交易已经成功结束，并不能再对该交易做后续操作);
' TRADE_SUCCESS(表示交易已经成功结束，可以对该交易做后续操作，如：分润、退款等);
'////////////////////////////////////////
%>

<!--#include file="class/alipay_notify.asp"-->

<%
'计算得出通知验证结果
Set objNotify = New AlipayNotify
sVerifyResult = objNotify.VerifyReturn()

If sVerifyResult Then	'验证成功
	'*********************************************************************
	'请在这里加上商户的业务逻辑程序代码

	'——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
    '获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
    out_trade_no	= Request.QueryString("out_trade_no")	'获取订单号
    trade_no		= Request.QueryString("trade_no")		'获取支付宝交易号
    total_fee		= Request.QueryString("total_fee")		'获取总金额
	
	If Request.QueryString("trade_status") = "TRADE_FINISHED" or Request.QueryString("trade_status") = "TRADE_SUCCESS" Then
	'判断是否在商户网站中已经做过了这次通知返回的处理
		'如果没有做过处理，那么执行商户的业务程序
		'如果有做过处理，那么不执行商户的业务程序
	Else
		Response.Write "trade_status="&Request.QueryString("trade_status")
	End If

	Response.Write "验证成功<br>"
	Response.Write "trade_no="&trade_no

	'——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
	
	'*********************************************************************
Else '验证失败
    '如要调试，请看alipay_notify.asp页面的VerifyReturn函数，比对sign和mysign的值是否相等，或者检查responseTxt有没有返回true
    Response.Write "验证失败"
End If
%>
<title>支付宝即时到帐</title>
</head>
<body>
</body>
</html>
