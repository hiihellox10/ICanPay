<?php
/* *
 * ������AlipayService
 * ���ܣ�֧�������ӿڹ�����
 * ��ϸ������֧�������ӿ��������
 * �汾��3.2
 * ���ڣ�2011-03-25
 * ˵����
 * ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 * �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
 */

require_once("alipay_submit.class.php");

class AlipayService {
	
	var $aliapy_config;
	/**
	 *֧�������ص�ַ���£�
	 */
	var $alipay_gateway_new = 'https://mapi.alipay.com/gateway.do?';

	function __construct($aliapy_config){
		$this->aliapy_config = $aliapy_config;
	}
    function AlipayService($aliapy_config) {
    	$this->__construct($aliapy_config);
    }
	/**
     * ���켴ʱ���ʽӿ�
     * @param $para_temp �����������
     * @return ���ύHTML��Ϣ
     */
	function create_direct_pay_by_user($para_temp) {
		//���ð�ť����
		$button_name = "ȷ��";
		//���ɱ��ύHTML�ı���Ϣ
		$alipaySubmit = new AlipaySubmit();
		$html_text = $alipaySubmit->buildForm($para_temp, $this->alipay_gateway_new, "get", $button_name, $this->aliapy_config);

		return $html_text;
	}

	/**
     * ���ڷ����㣬���ýӿ�query_timestamp����ȡʱ����Ĵ�����
	 * ע�⣺�ù���PHP5����������֧�֣���˱�������������ص�����װ��֧��DOMDocument��SSL��PHP���û��������鱾�ص���ʱʹ��PHP�������
     * return ʱ����ַ���
	 */
	function query_timestamp() {
		$url = $this->alipay_gateway_new."service=query_timestamp&partner=".trim($this->aliapy_config['partner']);
		$encrypt_key = "";		

		$doc = new DOMDocument();
		$doc->load($url);
		$itemEncrypt_key = $doc->getElementsByTagName( "encrypt_key" );
		$encrypt_key = $itemEncrypt_key->item(0)->nodeValue;
		
		return $encrypt_key;
	}
	
	/**
     * ����֧���������ӿ�
     * @param $para_temp �����������
     * @return ���ύHTML��Ϣ/֧��������XML������
     */
	function alipay_interface($para_temp) {
		//��ȡԶ������/���ɱ��ύHTML�ı���Ϣ
		$alipaySubmit = new AlipaySubmit();
		$html_text = "";
		//����ݲ�ͬ�Ľӿ����ԣ�ѡ��һ������ʽ
		//1.������ύHTML����:��$method�ɸ�ֵΪget��post��
		//$alipaySubmit->buildForm($para_temp, $this->alipay_gateway_new, "get", $button_name, $this->aliapy_config);
		//2.����ģ��Զ��HTTP��POST���󣬻�ȡ֧�����ķ���XML������:
		//ע�⣺��Ҫʹ��Զ��HTTP��ȡ���ݣ����뿪ͨSSL���񣬸÷������ҵ�php.ini�����ļ����ÿ����������������������Ա��ϵ�����
		//$alipaySubmit->sendPostInfo($para_temp, $this->alipay_gateway_new, $this->aliapy_config);
		
		return $html_text;
	}
}
?>