<?php
/* *
 * ������AlipayNotify
 * ���ܣ�֧����֪ͨ������
 * ��ϸ������֧�������ӿ�֪ͨ����
 * �汾��3.2
 * ���ڣ�2011-03-25
 * ˵����
 * ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 * �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο�

 *************************ע��*************************
 * ����֪ͨ����ʱ���ɲ鿴���дlog��־��д��TXT������ݣ������֪ͨ�����Ƿ�����
 */

require_once("alipay_core.function.php");

class AlipayNotify {
    /**
     * HTTPS��ʽ��Ϣ��֤��ַ
     */
	var $https_verify_url = 'https://mapi.alipay.com/gateway.do?service=notify_verify&';
	/**
     * HTTP��ʽ��Ϣ��֤��ַ
     */
	var $http_verify_url = 'http://notify.alipay.com/trade/notify_query.do?';
	var $aliapy_config;

	function __construct($aliapy_config){
		$this->aliapy_config = $aliapy_config;
	}
    function AlipayNotify($aliapy_config) {
    	$this->__construct($aliapy_config);
    }
    /**
     * ���notify_url��֤��Ϣ�Ƿ���֧���������ĺϷ���Ϣ
     * @return ��֤���
     */
	function verifyNotify(){
		if(empty($_POST)) {//�ж�POST���������Ƿ�Ϊ��
			return false;
		}
		else {
			//����ǩ�����
			$mysign = $this->getMysign($_POST);
			//��ȡ֧����Զ�̷�����ATN�������֤�Ƿ���֧������������Ϣ��
			$responseTxt = 'true';
			if (! empty($_POST["notify_id"])) {$responseTxt = $this->getResponse($_POST["notify_id"]);}
			
			//д��־��¼
			//$log_text = "responseTxt=".$responseTxt."\n notify_url_log:sign=".$_POST["sign"]."&mysign=".$mysign.",";
			//$log_text = $log_text.createLinkString($_POST);
			//logResult($log_text);
			
			//��֤
			//$responseTxt�Ľ������true����������������⡢���������ID��notify_idһ����ʧЧ�й�
			//mysign��sign���ȣ��밲ȫУ���롢����ʱ�Ĳ�����ʽ���磺���Զ�������ȣ��������ʽ�й�
			if (preg_match("/true$/i",$responseTxt) && $mysign == $_POST["sign"]) {
				return true;
			} else {
				return false;
			}
		}
	}
	
    /**
     * ���return_url��֤��Ϣ�Ƿ���֧���������ĺϷ���Ϣ
     * @return ��֤���
     */
	function verifyReturn(){
		if(empty($_GET)) {//�ж�POST���������Ƿ�Ϊ��
			return false;
		}
		else {
			//����ǩ�����
			$mysign = $this->getMysign($_GET);
			//��ȡ֧����Զ�̷�����ATN�������֤�Ƿ���֧������������Ϣ��
			$responseTxt = 'true';
			if (! empty($_GET["notify_id"])) {$responseTxt = $this->getResponse($_GET["notify_id"]);}
			
			//д��־��¼
			//$log_text = "responseTxt=".$responseTxt."\n notify_url_log:sign=".$_GET["sign"]."&mysign=".$mysign.",";
			//$log_text = $log_text.createLinkString($_GET);
			//logResult($log_text);
			
			//��֤
			//$responseTxt�Ľ������true����������������⡢���������ID��notify_idһ����ʧЧ�й�
			//mysign��sign���ȣ��밲ȫУ���롢����ʱ�Ĳ�����ʽ���磺���Զ�������ȣ��������ʽ�й�
			if (preg_match("/true$/i",$responseTxt) && $mysign == $_GET["sign"]) {
				return true;
			} else {
				return false;
			}
		}
	}
	
    /**
     * ���ݷ�����������Ϣ������ǩ�����
     * @param $para_temp ֪ͨ�������Ĳ�������
     * @return ���ɵ�ǩ�����
     */
	function getMysign($para_temp) {
		//��ȥ��ǩ�����������еĿ�ֵ��ǩ������
		$para_filter = paraFilter($para_temp);
		
		//�Դ�ǩ��������������
		$para_sort = argSort($para_filter);
		
		//����ǩ�����
		$mysign = buildMysign($para_sort, trim($this->aliapy_config['key']), strtoupper(trim($this->aliapy_config['sign_type'])));
		
		return $mysign;
	}

    /**
     * ��ȡԶ�̷�����ATN���,��֤����URL
     * @param $notify_id ֪ͨУ��ID
     * @return ������ATN���
     * ��֤�������
     * invalid����������� ��������������ⷵ�ش�����partner��key�Ƿ�Ϊ�� 
     * true ������ȷ��Ϣ
     * false �������ǽ�����Ƿ�������ֹ�˿������Լ���֤ʱ���Ƿ񳬹�һ����
     */
	function getResponse($notify_id) {
		$transport = strtolower(trim($this->aliapy_config['transport']));
		$partner = trim($this->aliapy_config['partner']);
		$veryfy_url = '';
		if($transport == 'https') {
			$veryfy_url = $this->https_verify_url;
		}
		else {
			$veryfy_url = $this->http_verify_url;
		}
		$veryfy_url = $veryfy_url."partner=" . $partner . "&notify_id=" . $notify_id;
		$responseTxt = getHttpResponse($veryfy_url);
		
		return $responseTxt;
	}
}
?>
