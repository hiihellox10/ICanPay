<%@ Page language="c#" Codebehind="SendOrder.aspx.cs" AutoEventWireup="false" Inherits="PortSampleForDotNet.SendOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SendOrder</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<style>
			body,input,td{font-size:9pt}
		</style>
	</HEAD>
	<body>
		<form name="cnPayForm" action="https://www.cncard.net/purchase/getorder.asp" method="POST">
		<table width="50%" border="0" align="center" cellpadding="3" cellspacing="1" bgcolor="#4777bc">
			<tr>
				<td height="30" colspan="2">&nbsp;&nbsp;<font color="#ffffff">云网支付@网 支付测试填写订单测试页</font></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">订单号</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;<%=c_order%></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">订单金额</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;￥<%=c_orderamount%></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">收货人</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;<input type="text" name="c_name" value="<%=c_name%>"></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">地 址</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;<input type="text" name="c_address" value="<%=c_address%>"></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">邮 编</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;<input type="text" name="c_post" value="<%=c_post%>"></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">联系电话</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;<input type="text" name="c_tel" value="<%=c_tel%>"></td>
			</tr>
			<tr>
				<td width="25%" height="25" align="center" bgcolor="#f5f5f5">Email</td>
				<td width="75%" bgcolor="#ffffff">&nbsp;<input type="text" name="c_email" value="<%=c_email%>"></td>
			</tr>
			<tr align="center" bgcolor="#ffffff">
				<td colspan="2">
					<table width="98%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td width="50%" align="center"><a href="https://www.cncard.net" target="_blank" title="云网支付@网"><img src="https://www.cncard.net/images/main_left_01.gif" width="159" height="55" border="0"></a></td>
							<td width="50%" align="center"><input type="submit" name="submit" value="点击 -> 云网支付@网"></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<input type="hidden" name="c_mid" value="<%=c_mid%>">
		<input type="hidden" name="c_order" value="<%=c_order%>">
		<input type="hidden" name="c_orderamount" value="<%=c_orderamount%>">
		<input type="hidden" name="c_ymd" value="<%=c_ymd%>">
		<input type="hidden" name="c_moneytype" value="<%=c_moneytype%>">
		<input type="hidden" name="c_retflag" value="<%=c_retflag%>">
		<input type="hidden" name="c_paygate" value="<%=c_paygate%>">
		<input type="hidden" name="c_returl" value="<%=c_returl%>">
		<input type="hidden" name="c_memo1" value="<%=c_memo1%>">
		<input type="hidden" name="c_memo2" value="<%=c_memo2%>">
		<input type="hidden" name="c_language" value="<%=c_language%>">
		<input type="hidden" name="notifytype" value="<%=notifytype%>">
		<input type="hidden" name="c_signstr" value="<%=c_signstr%>">
		</form>
	</body>
</HTML>
