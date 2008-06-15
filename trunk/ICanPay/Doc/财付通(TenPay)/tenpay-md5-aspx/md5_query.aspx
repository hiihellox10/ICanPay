<%@ Page language="c#" Codebehind="md5_query.aspx.cs" AutoEventWireup="false" Inherits="tenpaymd5.md5_query" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>订单查询页面</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 5%; POSITION: absolute; TOP: 5%" cellSpacing="1"
				cellPadding="1" width="90%" border="1">
				<TR>
					<TD align="center" colSpan="2">
						<asp:Label id="Label1" runat="server" ForeColor="Blue">查询页面需要填写内容</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label6" runat="server">支付日期</asp:label></TD>
					<TD><asp:textbox id="tbDate" runat="server" Width="90%" ForeColor="Blue" ToolTip="yyyyMMdd格式日期">20070423</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label4" runat="server">交易单号</asp:label></TD>
					<TD><asp:textbox id="tbTranID" runat="server" Width="90%" ForeColor="Blue" ToolTip="28位长交易单号">1201143001200704230000000001</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label3" runat="server">商户订单号</asp:label></TD>
					<TD><asp:textbox id="tbBillNo" runat="server" Width="90%" ForeColor="Blue" ToolTip="1-10位的正整数,请保证每天唯一">1</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label7" runat="server">指令标识</asp:label></TD>
					<TD><asp:textbox id="tbMemo" runat="server" Width="90%" ForeColor="Blue" ToolTip="每个指令都必须带有这个标识,财付通处理完成后会原样返回这个标识">test11</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btQuery" runat="server" Text="查 询"></asp:button></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2">
						<asp:Label id="labErrmsg" runat="server" ForeColor="Red"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
