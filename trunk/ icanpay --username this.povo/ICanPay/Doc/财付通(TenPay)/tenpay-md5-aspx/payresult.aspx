<%@ Page language="c#" Codebehind="payresult.aspx.cs" AutoEventWireup="false" Inherits="tenpaymd5.payresult" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>֧���������ҳ��</title>
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
					<TD align="center" colSpan="2"><asp:label id="Label1" runat="server" ForeColor="Blue">֧�����������ʾ</asp:label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label2" runat="server">���׵���</asp:label></TD>
					<TD><FONT face="����"><asp:label id="labTransid" runat="server"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label3" runat="server">������</asp:label></TD>
					<TD><asp:label id="labBillno" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label4" runat="server">�ܽ��</asp:label></TD>
					<TD><asp:label id="labTotalFee" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label7" runat="server">���ױ�ע</asp:label></TD>
					<TD><asp:label id="labAttach" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label5" runat="server">֧�����</asp:label></TD>
					<TD><asp:label id="labResult" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2"><asp:label id="labErrmsg" runat="server" ForeColor="Red"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
