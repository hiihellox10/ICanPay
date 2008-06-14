<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Send.aspx.cs" Inherits="Send" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>订单提交</title>
</head>
<body >
<body onLoad="javascript:document.E_FORM.submit()" >
     <form    action="https://pay3.chinabank.com.cn/PayGate"  method="post" name="E_FORM">
    
      <input type="hidden" name="v_md5info"    value="<%=v_md5info%>" size="100" />
      <input type="hidden" name="v_mid"        value="<%=v_mid%>" />
      <input type="hidden" name="v_oid"        value="<%=v_oid%>" />
      <input type="hidden" name="v_amount"     value="<%=v_amount%>" />
      <input type="hidden" name="v_moneytype"  value="<%=v_moneytype%>" />
      <input type="hidden" name="v_url"        value="<%=v_url%>" />


<!--以下几项项为网上支付完成后，随支付反馈信息一同传给信息接收页-->
    
  <input type="hidden"  name="remark1" value="<%=remark1%>" />
  <input type="hidden"  name="remark2" value="<%=remark2%>" />
    
  <!--以下几项只是用来记录客户信息，可以不用，不影响支付 -->

	<input type="hidden"  name="v_rcvname"      value="<%=v_rcvname%>" />
	<input type="hidden"  name="v_rcvaddr"      value="<%=v_rcvaddr%>" />
	<input type="hidden"  name="v_rcvtel"       value="<%=v_rcvtel%>" />
	<input type="hidden"  name="v_rcvpost"      value="<%=v_rcvpost%>" />
	<input type="hidden"  name="v_rcvemail"     value="<%=v_rcvemail%>" />
	<input type="hidden"  name="v_rcvmobile"    value="<%=v_rcvmobile%>" />

	<input type="hidden"  name="v_ordername"    value="<%=v_ordername%>" />
	<input type="hidden"  name="v_orderaddr"    value="<%=v_orderaddr%>" />
	<input type="hidden"  name="v_ordertel"     value="<%=v_ordertel%>" />
	<input type="hidden"  name="v_orderpost"    value="<%=v_orderpost%>" />
	<input type="hidden"  name="v_orderemail"   value="<%=v_orderemail%>" />
	<input type="hidden"  name="v_ordermobile"  value="<%=v_ordermobile%>" />
    </form>
</body>
</html>
