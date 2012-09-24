<?php
/* *
 * ֧�����ӿڹ��ú���
 * ��ϸ������������֪ͨ���������ļ������õĹ��ú������Ĵ����ļ�
 * �汾��3.2
 * ���ڣ�2011-03-25
 * ˵����
 * ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 * �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
 */

/**
 * ����ǩ�����
 * @param $sort_para Ҫǩ��������
 * @param $key ֧�������װ�ȫУ����
 * @param $sign_type ǩ������ Ĭ��ֵ��MD5
 * return ǩ������ַ���
 */
function buildMysign($sort_para,$key,$sign_type = "MD5") {
	//����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
	$prestr = createLinkstring($sort_para);
	//��ƴ�Ӻ���ַ������밲ȫУ����ֱ����������
	$prestr = $prestr.$key;
	//�����յ��ַ���ǩ�������ǩ�����
	$mysgin = sign($prestr,$sign_type);
	return $mysgin;
}
/**
 * ����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
 * @param $para ��Ҫƴ�ӵ�����
 * return ƴ������Ժ���ַ���
 */
function createLinkstring($para) {
	$arg  = "";
	while (list ($key, $val) = each ($para)) {
		$arg.=$key."=".$val."&";
	}
	//ȥ�����һ��&�ַ�
	$arg = substr($arg,0,count($arg)-2);
	
	//�������ת���ַ�����ôȥ��ת��
	if(get_magic_quotes_gpc()){$arg = stripslashes($arg);}
	
	return $arg;
}
/**
 * ����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ����������ַ�����urlencode����
 * @param $para ��Ҫƴ�ӵ�����
 * return ƴ������Ժ���ַ���
 */
function createLinkstringUrlencode($para) {
	$arg  = "";
	while (list ($key, $val) = each ($para)) {
		$arg.=$key."=".urlencode($val)."&";
	}
	//ȥ�����һ��&�ַ�
	$arg = substr($arg,0,count($arg)-2);
	
	//�������ת���ַ�����ôȥ��ת��
	if(get_magic_quotes_gpc()){$arg = stripslashes($arg);}
	
	return $arg;
}
/**
 * ��ȥ�����еĿ�ֵ��ǩ������
 * @param $para ǩ��������
 * return ȥ����ֵ��ǩ�����������ǩ��������
 */
function paraFilter($para) {
	$para_filter = array();
	while (list ($key, $val) = each ($para)) {
		if($key == "sign" || $key == "sign_type" || $val == "")continue;
		else	$para_filter[$key] = $para[$key];
	}
	return $para_filter;
}
/**
 * ����������
 * @param $para ����ǰ������
 * return ����������
 */
function argSort($para) {
	ksort($para);
	reset($para);
	return $para;
}
/**
 * ǩ���ַ���
 * @param $prestr ��Ҫǩ�����ַ���
 * @param $sign_type ǩ������ Ĭ��ֵ��MD5
 * return ǩ�����
 */
function sign($prestr,$sign_type='MD5') {
	$sign='';
	if($sign_type == 'MD5') {
		$sign = md5($prestr);
	}elseif($sign_type =='DSA') {
		//DSA ǩ����������������
		die("DSA ǩ����������������������ʹ��MD5ǩ����ʽ");
	}else {
		die("֧�����ݲ�֧��".$sign_type."���͵�ǩ����ʽ");
	}
	return $sign;
}
/**
 * д��־��������ԣ�����վ����Ҳ���ԸĳɰѼ�¼�������ݿ⣩
 * ע�⣺��������Ҫ��ͨfopen����
 * @param $word Ҫд����־����ı����� Ĭ��ֵ����ֵ
 */
function logResult($word='') {
	$fp = fopen("log.txt","a");
	flock($fp, LOCK_EX) ;
	fwrite($fp,"ִ�����ڣ�".strftime("%Y%m%d%H%M%S",time())."\n".$word."\n");
	flock($fp, LOCK_UN);
	fclose($fp);
}

/**
 * Զ�̻�ȡ����
 * ע�⣺�ú����Ĺ��ܿ�����curl��ʵ�ֺʹ��档curl�����б�д��
 * $url ָ��URL����·����ַ
 * @param $input_charset �����ʽ��Ĭ��ֵ����ֵ
 * @param $time_out ��ʱʱ�䡣Ĭ��ֵ��60
 * return Զ�����������
 */
function getHttpResponse($url, $input_charset = '', $time_out = "60") {
	$urlarr     = parse_url($url);
	$errno      = "";
	$errstr     = "";
	$transports = "";
	$responseText = "";
	if($urlarr["scheme"] == "https") {
		$transports = "ssl://";
		$urlarr["port"] = "443";
	} else {
		$transports = "tcp://";
		$urlarr["port"] = "80";
	}
	$fp=@fsockopen($transports . $urlarr['host'],$urlarr['port'],$errno,$errstr,$time_out);
	if(!$fp) {
		die("ERROR: $errno - $errstr<br />\n");
	} else {
		if (trim($input_charset) == '') {
			fputs($fp, "POST ".$urlarr["path"]." HTTP/1.1\r\n");
		}
		else {
			fputs($fp, "POST ".$urlarr["path"].'?_input_charset='.$input_charset." HTTP/1.1\r\n");
		}
		fputs($fp, "Host: ".$urlarr["host"]."\r\n");
		fputs($fp, "Content-type: application/x-www-form-urlencoded\r\n");
		fputs($fp, "Content-length: ".strlen($urlarr["query"])."\r\n");
		fputs($fp, "Connection: close\r\n\r\n");
		fputs($fp, $urlarr["query"] . "\r\n\r\n");
		while(!feof($fp)) {
			$responseText .= @fgets($fp, 1024);
		}
		fclose($fp);
		$responseText = trim(stristr($responseText,"\r\n\r\n"),"\r\n");
		
		return $responseText;
	}
}
/**
 * ʵ�ֶ����ַ����뷽ʽ
 * @param $input ��Ҫ������ַ���
 * @param $_output_charset ����ı����ʽ
 * @param $_input_charset ����ı����ʽ
 * return �������ַ���
 */
function charsetEncode($input,$_output_charset ,$_input_charset) {
	$output = "";
	if(!isset($_output_charset) )$_output_charset  = $_input_charset;
	if($_input_charset == $_output_charset || $input ==null ) {
		$output = $input;
	} elseif (function_exists("mb_convert_encoding")) {
		$output = mb_convert_encoding($input,$_output_charset,$_input_charset);
	} elseif(function_exists("iconv")) {
		$output = iconv($_input_charset,$_output_charset,$input);
	} else die("sorry, you have no libs support for charset change.");
	return $output;
}
/**
 * ʵ�ֶ����ַ����뷽ʽ
 * @param $input ��Ҫ������ַ���
 * @param $_output_charset ����Ľ����ʽ
 * @param $_input_charset ����Ľ����ʽ
 * return �������ַ���
 */
function charsetDecode($input,$_input_charset ,$_output_charset) {
	$output = "";
	if(!isset($_input_charset) )$_input_charset  = $_input_charset ;
	if($_input_charset == $_output_charset || $input ==null ) {
		$output = $input;
	} elseif (function_exists("mb_convert_encoding")) {
		$output = mb_convert_encoding($input,$_output_charset,$_input_charset);
	} elseif(function_exists("iconv")) {
		$output = iconv($_input_charset,$_output_charset,$input);
	} else die("sorry, you have no libs support for charset changes.");
	return $output;
}
?>