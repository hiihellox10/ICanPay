<%
' ���ܣ�֧������ʱ���ʽӿڵ������ҳ��
' �汾��3.2
' ���ڣ�2011-03-31
' ˵����
' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
	<title>֧������ʱ���ʽӿ�</title>
	<META http-equiv=Content-Type content="text/html; charset=gb2312">
	<script language=JavaScript>
function CheckForm()
{
	if (document.alipayment.subject.value.length == 0) {
		alert("��������Ʒ����.");
		document.alipayment.subject.focus();
		return false;
	}
	if (document.alipayment.total_fee.value.length == 0) {
		alert("�����븶����.");
		document.alipayment.total_fee.focus();
		return false;
	}
	var reg	= new RegExp(/^\d*\.?\d{0,2}$/);
	if (! reg.test(document.alipayment.total_fee.value))
	{
        alert("����ȷ���븶����");
		document.alipayment.total_fee.focus();
		return false;
	}
	if (Number(document.alipayment.total_fee.value) < 0.01) {
		alert("����������С��0.01.");
		document.alipayment.total_fee.focus();
		return false;
	}
	function getStrLength(value){
        return value.replace(/[^\x00-\xFF]/g,'**').length;
    }
    if(getStrLength(document.alipayment.alibody.value) > 200){
        alert("��ע����������100����������");
        document.alipayment.alibody.focus();
        return false;
    }
    if(getStrLength(document.alipayment.subject.value) > 256){
        alert("�������������128����������");
        document.alipayment.subject.focus();
        return false;
    }

	document.aplipayment.alibody.value = document.aplipayment.alibody.value.replace(/\n/g,'');
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
	font-family:'����';
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
<body text=#000000 bgColor=#ffffff leftMargin=0 topMargin=4>
	<div id="main">
		<div id="head">
			<div id="logo"></div>
            <dl class="alipay_link">
                <a target="_blank" href="http://www.alipay.com/"><span>֧������ҳ</span></a>|
                <a target="_blank" href="https://b.alipay.com/home.htm"><span>�̼ҷ���</span></a>|
                <a target="_blank" href="http://help.alipay.com/support/index_sh.htm"><span>��������</span></a>
            </dl>
            <span class="title">֧������ʱ���ʸ������ͨ��</span>
			<!--<div id="title" class="title">֧������ʱ���ʸ������ͨ��</div>-->
		</div>
        <div class="cashier-nav">
            <ol>
                <li class="current">1��ȷ�ϸ�����Ϣ ��</li>
                <li>2������ ��</li>
                <li class="last">3���������</li>
            </ol>
        </div>
        <form name=alipayment onSubmit="return CheckForm();" action=alipayto.asp method=post target="_blank">
            <div id="body" style="clear:left">
                <dl class="content">
                    <dt>���⣺</dt>
                    <dd>
                        <span class="red-star">*</span>
                        <input size=30 name=subject />
                        <span>�磺7��5�ն����</span>
                    </dd>
                    <dt>�����</dt>
                    <dd>
                        <span class="red-star">*</span>
                        <input maxLength=10 size=30 name=total_fee onfocus="if(Number(this.value)==0){this.value='';}" value="00.00"/>
                        <span>�磺112.21</span>
                    </dd>
                    <dt>��ע��</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <textarea style="margin-left:3px;" name=alibody rows=2 cols=40 wrap="physical"></textarea><br/>
                        <span>������ϵ��������ƷҪ�������ȡ�100�����ڣ�</span>
                    </dd>
                    <dt></dt>
                    <dd>
                        <span class="new-btn-login-sp">
                            <button class="new-btn-login" type="submit" style="text-align:center;">ȷ�ϸ���</button>
                        </span>
                    </dd>
                </dl>
            </div>
		</form>
        <div id="foot">
			<ul class="foot-ul">
				<li>
					<font class=note-help>����������ȷ�ϸ����ť������ʾ��ͬ�������ҹ������Ʒ�� 
					  <br/>
					  �������β�����������Ʒ��¼���ϣ��������ҵ�˵���ͽ��ܵĸ��ʽ�����ұ���е���Ʒ��Ϣ��ȷ��¼�����Σ� 
					</font>
				</li>
				<li>
					֧������Ȩ���� 2011-2015 ALIPAY.COM 
				</li>
			</ul>
			<ul>
		</div>
	</div>
</body>
</html>