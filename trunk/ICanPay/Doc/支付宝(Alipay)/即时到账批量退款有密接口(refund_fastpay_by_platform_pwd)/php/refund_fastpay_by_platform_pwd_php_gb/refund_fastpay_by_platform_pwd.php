<?php
/* *
 * ���ܣ���ʱ���������˿����ܽӿڽ���ҳ
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

//�˿�����ڣ���ȡ�������ڣ���ʽ����[4λ]-��[2λ]-��[2λ] Сʱ[2λ 24Сʱ��]:��[2λ]:��[2λ]���磺2007-10-01 13:13:13
$refund_date= date('Y-m-d H:i:s',time());

//�̼���վ������κţ���֤��Ψһ�ԣ���ʽ����������[8λ]+���к�[3��24λ]���磺201008010000001
$batch_no	= date('Ymdhis');

//�˿������������detail_data��ֵ�У���#���ַ����ֵ�������1�����֧��1000�ʣ�����#���ַ����ֵ�����999����
$batch_num	= $_POST['batch_num'];

//�˿���ϸ����
$detail_data= $_POST['detail_data'];	
//��ʽ����һ�ʽ���#�ڶ��ʽ���#�����ʽ���
//��N�ʽ��׸�ʽ�������˿���Ϣ
//�����˿���Ϣ��ʽ��ԭ����֧�������׺�^�˿��ܽ��^�˿�����
//ע�⣺
//1.detail_data�е��˿�����ܺ�Ҫ���ڲ���batch_num��ֵ
//2.detail_data��ֵ�в����С�^������|������#������$����Ӱ��detail_data�ĸ�ʽ�������ַ�
//3.detail_data���˿��ܽ��ܴ��ڽ����ܽ��
//4.һ�ʽ��׿��Զ���˿ֻ��Ҫ���ض���˿���ܽ������ñʽ��׸���ʱ��
//5.�����˿�ӿڲ�֧���˷�����

/************************************************************/

//����Ҫ����Ĳ������飬����Ķ�
$parameter = array(
		"service"			=> "refund_fastpay_by_platform_pwd",
		"partner"			=> trim($aliapy_config['partner']),
		"seller_email"			=> trim($aliapy_config['seller_email']),
		"notify_url"			=> trim($aliapy_config['notify_url']),
		"_input_charset"	=> trim(strtolower($aliapy_config['input_charset'])),
		"refund_date"		=> $refund_date,
		"batch_no"			=> $batch_no,
		"batch_num"			=> $batch_num,
		"detail_data"		=> $detail_data
);

//���켴ʱ���������˿����ܽӿ�
$alipayService = new AlipayService($aliapy_config);
$html_text = $alipayService->refund_fastpay_by_platform_pwd($parameter);
echo $html_text;

?>
