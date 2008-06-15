<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Req.aspx.cs" Inherits="Req" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>在线支付测试</title>
</head>
<body>
<body>
    <h4>GET方式</h4>
	<div id="yeepayGet"><%=htmlCodeGet %></div>
	<h4>POST方式</h4>
	<div id="yeepayPost"><%=htmlCodePost %></div>
	<h5>样式1</h5>
	<div><a href="javascript:window.frmYeepay.submit();"><img border="0" src="Images/YeepayOnlineImageButton1.gif"></a></div>
	<div>YeePay易宝银行卡在线支付：支持国内银行卡、信用卡，安全、稳定、高速，是您可信赖的电子支付平台。（工商银行、招商银行、建设银行、农业银行、民生银行、邮政储蓄、中国银行、交通银行、广发银行、深发银行、兴业银行、中信银行、上海浦发银行、北京银行…）</div>
	<h5>样式2</h5>
	<div><a href="javascript:window.frmYeepay.submit();"><img border="0" src="Images/YeepayOnlineImageButton2.gif"></a></div>
	<div>全面支持国内各种银行卡，安全稳定、您可信赖！</div>
	<h5>样式3</h5>
	<div><a href="javascript:window.frmYeepay.submit();"><img border="0" src="Images/YeepayOnlineImageButton3.gif"></a></div>
	<div>YeePay易宝银行卡网上支付：支持国内银行卡、信用卡，安全、稳定、高速，是您可信赖的电子支付平台。（工商银行、招商银行、建设银行、农业银行、民生银行、邮政储蓄、中国银行、交通银行、广发银行、深发银行、兴业银行、中信银行、上海浦发银行、北京银行…）</div>
	
	<h5>YeePay易宝支付业务介绍语</h5>
	<div><b>在线支付</b></div>
	<div>YeePay易宝银行卡在线支付：支持国内银行卡、信用卡，安全、稳定、高速，是您可信赖的电子支付平台。（工商银行、招商银行、建设银行、农业银行、民生银行、邮政储蓄、中国银行、交通银行、广发银行、深发银行、兴业银行、中信银行、上海浦发银行、北京银行…）</div>
	<div><b>电话支付</b></div>
	<div>电话银行支付(推荐)：支持工商银行95588（借记卡、信用卡及北京地区的活期存折）、民生银行95568（借记卡）、招商银行95555（一卡通和信用卡）、建设银行95533（北京龙卡）。</div>
	<div>快 捷：实时到账，支持随时随地的离线支付！</div>
	<div>安 全：无须上网，避免木马病毒和黑客窃密，是公认最安全的支付方式之一！</div>
	<div>免 费：拨一个电话，就能进行支付，不需要花费任何费用(如邮费、汇费)！</div>
	</body>
</body>
</html>
