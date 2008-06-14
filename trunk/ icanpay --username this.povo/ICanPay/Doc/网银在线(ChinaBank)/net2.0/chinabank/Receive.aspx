<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receive.aspx.cs" Inherits="Receive" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>支付结果</title>
</head>
<body>
    <form id="form1" runat="server">
    <TABLE width=500 border=0 align="center" cellPadding=0 cellSpacing=0>

			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>订单号:<%=v_oid%></B></div></TD>
			</TR>
			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>支付银行:<%=v_pmode%></B></div></TD>
			</TR>
			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>支付结果:<%=status_msg%></B></div></TD>
			</TR>
			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>支付金额:<%=v_amount%></B></div></TD>
			</TR>
			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>支付币种:<%=v_moneytype%></B></div></TD>
			</TR>
			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>备注1:<%=remark1%></B></div></TD>
			</TR>			
			<TR> 
			  <TD vAlign=top align=middle> <div align="left"><B>备注2:<%=remark2%></B></div></TD>
			</TR>				

		</TABLE>
    </form>
</body>
</html>
