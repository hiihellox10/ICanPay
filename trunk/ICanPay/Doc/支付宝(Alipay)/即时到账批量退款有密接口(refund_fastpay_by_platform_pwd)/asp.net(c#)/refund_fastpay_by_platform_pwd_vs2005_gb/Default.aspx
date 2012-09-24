<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>支付宝即时到账批量退款有密接口</title>
</head>

<body>
    <form id="Form1" runat="server">
        <div style="text-align: center; font-size: 9pt; font-family: 宋体">
            即时到账批量退款有密接口<br />
         
            卖家用户ID：<asp:TextBox ID="Seller_user_id" runat="server" Text="默认值"></asp:TextBox><br />
            退款批次号：<asp:TextBox ID="Batch_no" runat="server" Text="默认值"></asp:TextBox><br />
            退款请求时间：<asp:TextBox ID="Refund_date" runat="server" Text="默认值"></asp:TextBox><br />
            退款总笔数：<asp:TextBox ID="Batch_num" runat="server" Text="默认值"></asp:TextBox><br />
            单笔数据集：<asp:TextBox ID="Detail_data" runat="server" Text="默认值"></asp:TextBox><br />
            <asp:Button ID="BtnAlipay" runat="server" Text="确 认" OnClick="BtnAlipay_Click" /></div>
    </form>
</body>
</html>
