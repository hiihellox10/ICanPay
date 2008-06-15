<%@ Page language="c#" Codebehind="md5_pay.aspx.cs" AutoEventWireup="false" Inherits="tenpaymd5.md5_pay" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>支付页面</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 5%; POSITION: absolute; TOP: 5%" cellSpacing="1"
				cellPadding="1" width="90%" border="1">
				<TR>
					<TD align="center" colSpan="2">
						<asp:Label id="Label1" runat="server" ForeColor="Blue">支付页面需要填写内容</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label6" runat="server">商品名称</asp:label></TD>
					<TD><asp:textbox id="tbDesc" runat="server" Width="90%" ForeColor="Blue">测试商品</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label3" runat="server">订单号</asp:label></TD>
					<TD><asp:textbox id="tbBillNo" runat="server" Width="90%" ForeColor="Blue" ToolTip="1-10位的正整数,请保证每天唯一">1</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label4" runat="server">总金额</asp:label></TD>
					<TD><asp:textbox id="tbTotalFee" runat="server" Width="90%" ForeColor="Blue" ToolTip="以分为单位,请在真正支付页面进行转化">1</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label7" runat="server">指令标识</asp:label></TD>
					<TD><asp:textbox id="tbMemo" runat="server" Width="90%" ForeColor="Blue" ToolTip="每个指令都必须带有这个标识,财付通处理完成后会原样返回这个标识">test11</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btPay" runat="server" Text="支 付"></asp:button></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2">
						<asp:Label id="labErrmsg" runat="server" ForeColor="Red"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
