<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<%
' 功能：即时到账批量退款有密接口接入页
' 版本：3.2
' 日期：2011-03-31
' 说明：
' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
	
' /////////////////注意/////////////////
' 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
' 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
' 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
' 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
' 如果不想使用扩展功能请把扩展功能参数赋空值。
' /////////////////////////////////////

%>

<!--#include file="class/alipay_service.asp"-->

<%
'/////////////////////请求参数/////////////////////
'//必填参数//

'退款当天日期，获取当天日期，格式：年[4位]-月[2位]-日[2位] 小时[2位 24小时制]:分[2位]:秒[2位]，如：2007-10-01 13:13:13
refund_date	= GetDateTimeFormat()

'商家网站里的批次号，保证其唯一性，格式：当天日期[8位]+序列号[3至24位]，如：201008010000001
batch_no	= GetDateTime()

'退款笔数，即参数detail_data的值中，“#”字符出现的数量加1，最大支持1000笔（即“#”字符出现的数量999个）
batch_num	= request.Form("batch_num")

'退款详细数据
detail_data	= request.Form("detail_data")
'格式：第一笔交易#第二笔交易#第三笔交易
'第N笔交易格式：交易退款信息
'交易退款信息格式：原付款支付宝交易号^退款总金额^退款理由
'注意：
'1.detail_data中的退款笔数总和要等于参数batch_num的值
'2.detail_data的值中不能有“^”、“|”、“$”、“#”等影响detail_data的格式的特殊字符
'3.detail_data中退款总金额不能大于交易总金额
'4.一笔交易可以多次退款，只需要遵守多次退款的总金额不超过该笔交易付款时金额。
'5.不支持退分润功能

'/////////////////////请求参数/////////////////////

'构造请求参数数组
sParaTemp = Array("service=refund_fastpay_by_platform_pwd","partner="&partner,"seller_email="&seller_email,"notify_url="&notify_url,"refund_date="&refund_date,"batch_no="&batch_no,"batch_num="&batch_num,"detail_data="&detail_data,"_input_charset="&input_charset)

'构造即时到账批量退款有密接口表单提交HTML数据，无需修改
Set objService = New AlipayService
sHtml = objService.Refund_fastpay_by_platform_pwd(sParaTemp)
response.Write sHtml
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>支付宝即时到账批量退款有密</title>
</head>
<body>
</body>
</html>
