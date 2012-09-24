<?php
/* *
 * ������AlipaySubmit
 * ���ܣ�֧�������ӿ������ύ��
 * ��ϸ������֧�������ӿڱ�HTML�ı�����ȡԶ��HTTP����
 * �汾��3.2
 * ���ڣ�2011-03-25
 * ˵����
 * ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 * �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
 */
require_once("alipay_core.function.php");
class AlipaySubmit {
	/**
     * ����Ҫ�����֧�����Ĳ�������
     * @param $para_temp ����ǰ�Ĳ�������
     * @param $aliapy_config ����������Ϣ����
     * @return Ҫ����Ĳ�������
     */
	function buildRequestPara($para_temp,$aliapy_config) {
		//��ȥ��ǩ�����������еĿ�ֵ��ǩ������
		$para_filter = paraFilter($para_temp);

		//�Դ�ǩ��������������
		$para_sort = argSort($para_filter);

		//����ǩ�����
		$mysign = buildMysign($para_sort, trim($aliapy_config['key']), strtoupper(trim($aliapy_config['sign_type'])));
		
		//ǩ�������ǩ����ʽ���������ύ��������
		$para_sort['sign'] = $mysign;
		$para_sort['sign_type'] = strtoupper(trim($aliapy_config['sign_type']));
		
		return $para_sort;
	}

	/**
     * ����Ҫ�����֧�����Ĳ�������
     * @param $para_temp ����ǰ�Ĳ�������
	 * @param $aliapy_config ����������Ϣ����
     * @return Ҫ����Ĳ��������ַ���
     */
	function buildRequestParaToString($para_temp,$aliapy_config) {
		//�������������
		$para = $this->buildRequestPara($para_temp,$aliapy_config);
		
		//�Ѳ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ��������Բ���ֵ��urlencode����
		$request_data = createLinkstringUrlencode($para);
		
		return $request_data;
	}
	
    /**
     * �����ύ��HTML����
     * @param $para_temp �����������
     * @param $gateway ���ص�ַ
     * @param $method �ύ��ʽ������ֵ��ѡ��post��get
     * @param $button_name ȷ�ϰ�ť��ʾ����
     * @return �ύ��HTML�ı�
     */
	function buildForm($para_temp, $gateway, $method, $button_name, $aliapy_config) {
		//�������������
		$para = $this->buildRequestPara($para_temp,$aliapy_config);
		
		$sHtml = "<form id='alipaysubmit' name='alipaysubmit' action='".$gateway."_input_charset=".trim(strtolower($aliapy_config['input_charset']))."' method='".$method."'>";
		while (list ($key, $val) = each ($para)) {
            $sHtml.= "<input type='hidden' name='".$key."' value='".$val."'/>";
        }

		//submit��ť�ؼ��벻Ҫ����name����
        $sHtml = $sHtml."<input type='submit' value='".$button_name."'></form>";
		
		$sHtml = $sHtml."<script>document.forms['alipaysubmit'].submit();</script>";
		
		return $sHtml;
	}
	
	/**
     * ����ģ��Զ��HTTP��POST���󣬻�ȡ֧�����ķ���XML������
	 * ע�⣺�ù���PHP5����������֧�֣���˱�������������ص�����װ��֧��DOMDocument��SSL��PHP���û��������鱾�ص���ʱʹ��PHP�������
     * @param $para_temp �����������
     * @param $gateway ���ص�ַ
	 * @param $aliapy_config ����������Ϣ����
     * @return ֧��������XML������
     */
	function sendPostInfo($para_temp, $gateway, $aliapy_config) {
		$xml_str = '';
		
		//��������������ַ���
		$request_data = $this->buildRequestParaToString($para_temp,$aliapy_config);
		//�����url��������
		$url = $gateway . $request_data;
		//Զ�̻�ȡ����
		$xml_data = getHttpResponse($url,trim(strtolower($aliapy_config['input_charset'])));
		//����XML
		$doc = new DOMDocument();
		$doc->loadXML($xml_data);

		return $doc;
	}
}
?>