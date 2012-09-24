<%
	/* *
	 *功能：即时到帐批量退款有密接口调试入口页面
	 *版本：3.2
	 *日期：2011-03-17
	 *说明：
	 *以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
	 */
%>
<%@ page language="java" contentType="text/html; charset=utf-8"
	pageEncoding="utf-8"%>
<%@ page import="com.alipay.services.*"%>
<%@ page import="com.alipay.util.*"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<title>即时到帐批量退款有密接口</title>
	</head>
	<BODY>
		<FORM name=alisubmit action=refund_fastpay_by_platform_pwd.jsp method=post target="_blank">
			<div style="text-align: center; font-size: 9pt; font-family: 宋体">
			卖家支付宝账号：<INPUT type="text" size="30" name="seller_email" value=""><br />
			卖家用户ID：<INPUT type="text" size="30" name="seller_user_id" value=""><br />
			  退款批次号：<INPUT type="text" size="30" name="batch_no" value=""><br />
			退款请求时间：<INPUT type="text" size="30" name="refund_date" value=""><br />
			退款总笔数：<INPUT type="text" size="30" name="batch_num" value=""><br />
			单笔数据集：<INPUT type="text" size="30" name="detail_data" value=""><br />
                <INPUT type="submit" value="退款"  name="btnAlipay">
			</div>
		</FORM>
	</BODY>
</html>