<%@ Page language="c#" Codebehind="md5_query.aspx.cs" AutoEventWireup="false" Inherits="tenpaymd5.md5_query" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������ѯҳ��</title>
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
						<asp:Label id="Label1" runat="server" ForeColor="Blue">��ѯҳ����Ҫ��д����</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label6" runat="server">֧������</asp:label></TD>
					<TD><asp:textbox id="tbDate" runat="server" Width="90%" ForeColor="Blue" ToolTip="yyyyMMdd��ʽ����">20070423</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label4" runat="server">���׵���</asp:label></TD>
					<TD><asp:textbox id="tbTranID" runat="server" Width="90%" ForeColor="Blue" ToolTip="28λ�����׵���">1201143001200704230000000001</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label3" runat="server">�̻�������</asp:label></TD>
					<TD><asp:textbox id="tbBillNo" runat="server" Width="90%" ForeColor="Blue" ToolTip="1-10λ��������,�뱣֤ÿ��Ψһ">1</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" width="30%"><asp:label id="Label7" runat="server">ָ���ʶ</asp:label></TD>
					<TD><asp:textbox id="tbMemo" runat="server" Width="90%" ForeColor="Blue" ToolTip="ÿ��ָ�������������ʶ,�Ƹ�ͨ������ɺ��ԭ�����������ʶ">test11</asp:textbox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btQuery" runat="server" Text="�� ѯ"></asp:button></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2">
						<asp:Label id="labErrmsg" runat="server" ForeColor="Red"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
