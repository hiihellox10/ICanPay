<%
	/* *
	 *���ܣ���ʱ���������˿����ܽӿڵ������ҳ��
	 *�汾��3.2
	 *���ڣ�2011-03-17
	 *˵����
	 *���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
	 */
%>
<%@ page language="java" contentType="text/html; charset=gbk"
	pageEncoding="gbk"%>
<%@ page import="com.alipay.services.*"%>
<%@ page import="com.alipay.util.*"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=gbk">
		<title>��ʱ���������˿����ܽӿ�</title>
	</head>
	<BODY>
		<FORM name=alisubmit action=refund_fastpay_by_platform_pwd.jsp method=post target="_blank">
			<div style="text-align: center; font-size: 9pt; font-family: ����">
			����֧�����˺ţ�<INPUT type="text" size="30" name="seller_email" value=""><br />
			�����û�ID��<INPUT type="text" size="30" name="seller_user_id" value=""><br />
			  �˿����κţ�<INPUT type="text" size="30" name="batch_no" value=""><br />
			�˿�����ʱ�䣺<INPUT type="text" size="30" name="refund_date" value=""><br />
			�˿��ܱ�����<INPUT type="text" size="30" name="batch_num" value=""><br />
			�������ݼ���<INPUT type="text" size="30" name="detail_data" value=""><br />
                <INPUT type="submit" value="�˿�"  name="btnAlipay">
			</div>
		</FORM>
	</BODY>
</html>