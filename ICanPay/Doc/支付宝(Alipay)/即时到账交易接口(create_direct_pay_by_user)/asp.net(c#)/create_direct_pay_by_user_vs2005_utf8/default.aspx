<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>支付宝即时到帐接口</title>

    <script language="JavaScript">
function CheckForm()
{
	if (document.alipayment.TxtSubject.value.length == 0) {
		alert("请输入商品名称.");
		document.alipayment.TxtSubject.focus();
		return false;
	}
	if (document.alipayment.TxtTotal_fee.value.length == 0) {
		alert("请输入付款金额.");
		document.alipayment.TxtTotal_fee.focus();
		return false;
	}
	var reg	= new RegExp(/^\d*\.?\d{0,2}$/);
	if (! reg.test(document.alipayment.TxtTotal_fee.value))
	{
        alert("请正确输入付款金额");
		document.alipayment.TxtTotal_fee.focus();
		return false;
	}
	if (Number(document.alipayment.TxtTotal_fee.value) < 0.01) {
		alert("付款金额金额最小是0.01.");
		document.alipayment.TxtTotal_fee.focus();
		return false;
	}
	function getStrLength(value){
        return value.replace(/[^\x00-\xFF]/g,'**').length;
    }
    if(getStrLength(document.alipayment.TxtBody.value) > 200){
        alert("备注过长！请在100个汉字以内");
        document.alipayment.TxtBody.focus();
        return false;
    }
    if(getStrLength(document.alipayment.TxtSubject.value) > 256){
        alert("标题过长！请在128个汉字以内");
        document.alipayment.TxtSubject.focus();
        return false;
    }

	document.aplipayment.TxtBody.value = document.aplipayment.TxtBody.value.replace(/\n/g,'');
}  

    </script>

    <style>
*{
	margin:0;
	padding:0;
}
ul,ol{
	list-style:none;
}
.title{
    color: #ADADAD;
    font-size: 14px;
    font-weight: bold;
    padding: 8px 16px 5px 10px;
}
.hidden{
	display:none;
}

.new-btn-login-sp{
	border:1px solid #D74C00;
	padding:1px;
	display:inline-block;
}

.new-btn-login{
    background-color: transparent;
    background-image: url("images/new-btn-fixed.png");
    border: medium none;
}
.new-btn-login{
    background-position: 0 -198px;
    width: 82px;
	color: #FFFFFF;
    font-weight: bold;
    height: 28px;
    line-height: 28px;
    padding: 0 10px 3px;
}
.new-btn-login:hover{
	background-position: 0 -167px;
	width: 82px;
	color: #FFFFFF;
    font-weight: bold;
    height: 28px;
    line-height: 28px;
    padding: 0 10px 3px;
}
.bank-list{
	overflow:hidden;
	margin-top:5px;
}
.bank-list li{
	float:left;
	width:153px;
	margin-bottom:5px;
}

#main{
	width:750px;
	margin:0 auto;
	font-size:14px;
	font-family:'宋体';
}
#logo{
	background-color: transparent;
    background-image: url("images/new-btn-fixed.png");
    border: medium none;
	background-position:0 0;
	width:166px;
	height:35px;
    float:left;
}
.red-star{
	color:#f00;
	width:10px;
	display:inline-block;
}
.null-star{
	color:#fff;
}
.content{
	margin-top:5px;
}

.content dt{
	width:100px;
	display:inline-block;
	text-align:right;
	float:left;
	
}
.content dd{
	margin-left:100px;
	margin-bottom:5px;
}
#foot{
	margin-top:10px;
}
.foot-ul li {
	text-align:center;
}
.note-help {
    color: #999999;
    font-size: 12px;
    line-height: 130%;
    padding-left: 3px;
}

.cashier-nav {
    font-size: 14px;
    margin: 15px 0 10px;
    text-align: left;
    height:30px;
    border-bottom:solid 2px #CFD2D7;
}
.cashier-nav ol li {
    float: left;
}
.cashier-nav li.current {
    color: #AB4400;
    font-weight: bold;
}
.cashier-nav li.last {
    clear:right;
}
.alipay_link {
    text-align:right;
}
.alipay_link a:link{
    text-decoration:none;
    color:#8D8D8D;
}
.alipay_link a:visited{
    text-decoration:none;
    color:#8D8D8D;
}
</style>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="main">
            <div id="head">
                <div id="logo">
                </div>
                <dl class="alipay_link">
                    <a target="_blank" href="http://www.alipay.com/"><span>支付宝首页</span></a>| <a target="_blank"
                        href="https://b.alipay.com/home.htm"><span>商家服务</span></a>| <a target="_blank" href="http://help.alipay.com/support/index_sh.htm">
                            <span>帮助中心</span></a>
                </dl>
                <span class="title">支付宝即时到帐付款快速通道</span>
                <!--<div id="title" class="title">支付宝即时到帐付款快速通道</div>-->
            </div>
            <div class="cashier-nav">
                <ol>
                    <li class="current">1、确认付款信息 →</li>
                    <li>2、付款 →</li>
                    <li class="last">3、付款完成</li>
                </ol>
            </div>
            <div id="body" style="clear: left">
                <dl class="content">
                    <dt>标题：</dt>
                    <dd>
                        <span class="red-star">*</span>
                        <asp:TextBox ID="TxtSubject" name="TxtSubject" runat="server"></asp:TextBox><span>如：7月5日定货款。</span>
                    </dd>
                    <dt>付款金额：</dt>
                    <dd>
                        <span class="red-star">*</span>
                        <asp:TextBox ID="TxtTotal_fee" name="TxtTotal_fee" runat="server" Text="00.00" onfocus="if(Number(this.value)==0){this.value='';}"
                            MaxLength="10"></asp:TextBox>
                        <span>如：112.21</span>
                    </dd>
                    <dt>备注：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="TxtBody" name="TxtBody" runat="server" MaxLength="100" TextMode="MultiLine"></asp:TextBox><br />
                        <span>（如联系方法，商品要求、数量等。100汉字内）</span>
                    </dd>
                    <dt></dt>
                    <dd>
                        <span class="new-btn-login-sp">
                            <asp:Button ID="BtnAlipay" name="BtnAlipay" class="new-btn-login" Text="确认付款" Style="text-align: center;"
                                runat="server" OnClick="BtnAlipay_Click" /></span></dd></dl>
            </div>
            <div id="foot">
                <ul class="foot-ul">
                    <li><font class="note-help">如果您点击“确认付款”按钮，即表示您同意向卖家购买此物品。
                        <br />
                        您有责任查阅完整的物品登录资料，包括卖家的说明和接受的付款方式。卖家必须承担物品信息正确登录的责任！ </font></li>
                    <li>支付宝版权所有 2011-2015 ALIPAY.COM </li>
                </ul>
                <ul>
            </div>
        </div>
    </form>
</body>
</html>
