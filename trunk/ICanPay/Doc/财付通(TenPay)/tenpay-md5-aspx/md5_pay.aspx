<%@ Page language="c#" Codebehind="md5_pay.aspx.cs" AutoEventWireup="false" Inherits="tenpaymd5.md5_pay" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>֧��ҳ��</title>
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
						<asp:Label id="Label1" runat="server" ForeColor="Blue">֧��ҳ����Ҫ��д����</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label6" runat="server">��Ʒ����</asp:label></TD>
					<TD><asp:textbox id="tbDesc" runat="server" Width="90%" ForeColor="Blue">������Ʒ</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label3" runat="server">������</asp:label></TD>
					<TD><asp:textbox id="tbBillNo" runat="server" Width="90%" ForeColor="Blue" ToolTip="1-10λ��������,�뱣֤ÿ��Ψһ">1</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label4" runat="server">�ܽ��</asp:label></TD>
					<TD><asp:textbox id="tbTotalFee" runat="server" Width="90%" ForeColor="Blue" ToolTip="�Է�Ϊ��λ,��������֧��ҳ�����ת��">1</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label7" runat="server">ָ���ʶ</asp:label></TD>
					<TD><asp:textbox id="tbMemo" runat="server" Width="90%" ForeColor="Blue" ToolTip="ÿ��ָ�������������ʶ,�Ƹ�ͨ������ɺ��ԭ�����������ʶ">test11</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btPay" runat="server" Text="֧ ��"></asp:button></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2">
						<asp:Label id="labErrmsg" runat="server" ForeColor="Red"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
