<%@ Page language="c#" Codebehind="GetPayHandle.aspx.cs" AutoEventWireup="false" Inherits="PortSampleForDotNet.GetPayHandle" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GetPayHandle</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style>
			BODY { FONT-SIZE: 9pt }
			INPUT { FONT-SIZE: 9pt }
			TD { FONT-SIZE: 9pt }
		</style>
	</HEAD>
	<body>
		<div align="center"><asp:label id="payMsg" ForeColor="#E10900" Runat="server">֧�����Խ��</asp:label></div>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="1" cellPadding="3" width="385" align="center" bgColor="#000000" border="0">
				<tr>
					<td width="102" bgColor="#f0f0f0" height="20">�������ڣ�</td>
					<td width="280" bgColor="#ffffff">&nbsp;<font color="#000080"><%= c_ymd %></font></td>
				</tr>
				<tr>
					<td width="102" bgColor="#f0f0f0" height="20">�� �� �ţ�</td>
					<td width="280" bgColor="#ffffff">&nbsp;<font color="#000080"><%= c_order %></font></td>
				</tr>
				<tr>
					<td width="102" bgColor="#f0f0f0" height="20">������</td>
					<td width="280" bgColor="#ffffff">&nbsp;<font color="#000080"><%= c_orderamount %>Ԫ</font></td>
				</tr>
				<tr>
					<td width="102" bgColor="#f0f0f0" height="20">֧����ʽ��</td>
					<td width="280" bgColor="#ffffff">&nbsp;<font color="#000080">����֧��@������֧��</font></td>
				</tr>
				<tr>
					<td width="102" bgColor="#f0f0f0" height="20">֧�������</td>
					<td width="280" bgColor="#ffffff">&nbsp;<font color="#e10900"><%
					if(c_succmark=="Y")
					{
						Response.Write("��֧���ɹ�");
					}
					else
					{
						Response.Write("֧��ʧ��, ����ԭ��" + c_cause);
					}
					%></font>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>