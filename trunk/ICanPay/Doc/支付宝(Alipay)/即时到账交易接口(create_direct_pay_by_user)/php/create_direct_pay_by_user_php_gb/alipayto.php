<?php
/* *
 * ���ܣ���ʱ���ʽӿڽ���ҳ
 * �汾��3.2
 * �޸����ڣ�2011-03-25
 * ˵����
 * ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
 * �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���

 *************************ע��*************************
 * ������ڽӿڼ��ɹ������������⣬���԰��������;�������
 * 1���̻��������ģ�https://b.alipay.com/support/helperApply.htm?action=consultationApply�����ύ���뼯��Э�������ǻ���רҵ�ļ�������ʦ������ϵ��Э�����
 * 2���̻��������ģ�http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9��
 * 3��֧������̳��http://club.alipay.com/read-htm-tid-8681712.html��
 * �������ʹ����չ���������չ���ܲ�������ֵ��
 */

require_once("alipay.config.php");
require_once("lib/alipay_service.class.php");

/**************************�������**************************/

//�������//

//�������վ����ϵͳ�е�Ψһ������ƥ��
$out_trade_no = date('Ymdhis');
//�������ƣ���ʾ��֧��������̨��ġ���Ʒ���ơ����ʾ��֧�����Ľ��׹���ġ���Ʒ���ơ����б��
$subject      = $_POST['subject'];
//����������������ϸ��������ע����ʾ��֧��������̨��ġ���Ʒ��������
$body         = $_POST['body'];
//�����ܽ���ʾ��֧��������̨��ġ�Ӧ���ܶ��
$total_fee    = $_POST['total_fee'];



//��չ���ܲ�������Ĭ��֧����ʽ//

//Ĭ��֧����ʽ��ȡֵ������ʱ���ʽӿڡ������ĵ��е���������б�
$paymethod    = '';
//Ĭ���������ţ������б������ʱ���ʽӿڡ������ĵ�����¼�����������б�
$defaultbank  = '';


//��չ���ܲ�������������//

//������ʱ���
$anti_phishing_key  = '';
//��ȡ�ͻ��˵�IP��ַ�����飺��д��ȡ�ͻ���IP��ַ�ĳ���
$exter_invoke_ip = '';
//ע�⣺
//1.������ѡ���Ƿ��������㹦��
//2.exter_invoke_ip��anti_phishing_keyһ����ʹ�ù�����ô���Ǿͻ��Ϊ�������
//3.���������㹦�ܺ󣬷��������������Ա���֧��SSL�������úøû�����
//ʾ����
//$exter_invoke_ip = '202.1.1.1';
//$ali_service_timestamp = new AlipayService($aliapy_config);
//$anti_phishing_key = $ali_service_timestamp->query_timestamp();//��ȡ������ʱ�������


//��չ���ܲ�����������//

//��Ʒչʾ��ַ��Ҫ�� http://��ʽ������·�����������?id=123�����Զ������
$show_url			= 'http://www.xxx.com/order/myorder.php';
//�Զ���������ɴ���κ����ݣ���=��&�������ַ��⣩��������ʾ��ҳ����
$extra_common_param = '';

//��չ���ܲ�����������(��Ҫʹ�ã��밴��ע��Ҫ��ĸ�ʽ��ֵ)
$royalty_type		= "";			//������ͣ���ֵΪ�̶�ֵ��10������Ҫ�޸�
$royalty_parameters	= "";
//ע�⣺
//�����Ϣ��������Ҫ����̻���վ���������̬��ȡÿ�ʽ��׵ĸ������տ��˺š��������������˵�������ֻ������10��
//����������ܺ���С�ڵ���total_fee
//�����Ϣ����ʽΪ���տEmail_1^���1^��ע1|�տEmail_2^���2^��ע2
//ʾ����
//royalty_type 		= "10"
//royalty_parameters= "111@126.com^0.01^����עһ|222@126.com^0.01^����ע��"

/************************************************************/

//����Ҫ����Ĳ�������
$parameter = array(
		"service"			=> "create_direct_pay_by_user",
		"payment_type"		=> "1",
		
		"partner"			=> trim($aliapy_config['partner']),
		"_input_charset"	=> trim(strtolower($aliapy_config['input_charset'])),
        "seller_email"		=> trim($aliapy_config['seller_email']),
        "return_url"		=> trim($aliapy_config['return_url']),
        "notify_url"		=> trim($aliapy_config['notify_url']),
		
		"out_trade_no"		=> $out_trade_no,
		"subject"			=> $subject,
		"body"				=> $body,
		"total_fee"			=> $total_fee,
		
		"paymethod"			=> $paymethod,
		"defaultbank"		=> $defaultbank,
		
		"anti_phishing_key"	=> $anti_phishing_key,
		"exter_invoke_ip"	=> $exter_invoke_ip,
		
		"show_url"			=> $show_url,
		"extra_common_param"=> $extra_common_param,
		
		"royalty_type"		=> $royalty_type,
		"royalty_parameters"=> $royalty_parameters
);

//���켴ʱ���ʽӿ�
$alipayService = new AlipayService($aliapy_config);
$html_text = $alipayService->create_direct_pay_by_user($parameter);
echo $html_text;

?>
