
            �q�����������������������������������������������r
  �q����������           ֧��������ʾ���ṹ˵��             �����������r
  ��        �t�����������������������������������������������s        ��
����                                                                  ��
����     �ӿ����ƣ�֧������ʱ���ʽӿڣ�create_direct_pay_by_user��    ��
������   ����汾��3.2                                                ��
  ��     �������ԣ�PHP                                                ��
  ��     ��    Ȩ��֧�������й������缼�����޹�˾                     ��
����     �� �� �ߣ�֧�����̻���ҵ������֧����                         ��
  ��     ��ϵ��ʽ���̻�����绰0571-88158090                          ��
  ��                                                                  ��
  �t�������������������������������������������������������������������s

��������������
 �����ļ��ṹ
��������������

create_direct_pay_by_user_php_utf8
  ��
  ��lib�����������������������������������������ļ���
  ��  ��
  ��  ��alipay_core.function.php ������������֧�����ӿڹ��ú����ļ�
  ��  ��
  ��  ��alipay_notify.class.php��������������֧����֪ͨ�������ļ�
  ��  ��
  ��  ��alipay_submit.class.php��������������֧�������ӿ������ύ���ļ�
  ��  ��
  ��  ��alipay_service.class.php ������������֧�������ӿڹ������ļ�
  ��
  ��log.txt������������������������������������־�ļ�
  ��
  ��alipay.config.php�����������������������������������ļ�
  ��
  ��alipayto.php ����������������������������֧�����ӿ�����ļ�
  ��
  ��index.php��������������������������������֧�����������ҳ��
  ��
  ��notify_url.php ���������������������������������첽֪ͨҳ���ļ�
  ��
  ��return_url.php ��������������������������ҳ����תͬ��֪ͨ�ļ�
  ��
  ��readme.txt ������������������������������ʹ��˵���ı�

��ע���
��Ҫ���õ��ļ��ǣ�alipay.config.php��alipayto.php��notify_url.php��return_url.php

������ʾ����DEMO������fsockopen()�ķ���Զ��HTTP��ȡ���ݡ�����DOMDocument()�ķ�������XML���ݡ�

������̻���վ��������������Ƿ�ʹ�ô���ʾ���еķ�ʽ����
�����ʹ��fsockopen����ô������curl�����棻
�����������PHP5�汾�������ϣ���ô����������������DOMDocument()��

curl��XML���������������б�д���롣


������������������
 ���ļ������ṹ
������������������

alipay_core.function.php

function buildMysign($sort_para,$key,$sign_type = "MD5")
���ܣ�����ǩ�����
���룺Array  $sort_para Ҫǩ��������
      String $key ֧�������װ�ȫУ����
      String $sign_type ǩ������ Ĭ��ֵ MD5
�����String ǩ������ַ���

function createLinkstring($para)
���ܣ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
���룺Array  $para ��Ҫƴ�ӵ�����
�����String ƴ������Ժ���ַ���

function createLinkstringUrlencode($para)
���ܣ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ��������Բ���ֵurlencode
���룺Array  $para ��Ҫƴ�ӵ�����
�����String ƴ������Ժ���ַ���

function paraFilter($para)
���ܣ���ȥ�����еĿ�ֵ��ǩ������
���룺Array  $para ǩ��������
�����Array  ȥ����ֵ��ǩ�����������ǩ��������

function argSort($para)
���ܣ�����������
���룺Array  $para ����ǰ������
�����Array  ����������

function sign($prestr,$sign_type='MD5')
���ܣ�ǩ���ַ���
���룺String $prestr ��Ҫǩ�����ַ���
      String $sign_type ǩ������ Ĭ��ֵ��MD5
�����String ǩ�����

function logResult($word='')
���ܣ�д��־��������ԣ�����վ����Ҳ���Ըĳɴ������ݿ⣩
���룺String $word Ҫд����־����ı����� Ĭ��ֵ����ֵ

function getHttpResponse($url, $input_charset = '', $time_out = "60")
���ܣ�Զ�̻�ȡ����
���룺String $url ָ��URL����·����ַ
      String $input_charset �����ʽ��Ĭ��ֵ����ֵ
      String $time_out ��ʱʱ�䡣Ĭ��ֵ��60
�����String Զ�����������

function charsetEncode($input,$_output_charset ,$_input_charset)
���ܣ�ʵ�ֶ����ַ����뷽ʽ
���룺String $input ��Ҫ������ַ���
      String $_output_charset ����ı����ʽ
      String $_input_charset ����ı����ʽ
�����String �������ַ���

function charsetDecode($input,$_input_charset ,$_output_charset) 
���ܣ�ʵ�ֶ����ַ����뷽ʽ
���룺String $input ��Ҫ������ַ���
      String $_output_charset ����Ľ����ʽ
      String $_input_charset ����Ľ����ʽ
�����String �������ַ���

��������������������������������������������������������������

alipay_notify.class.php

function verifyNotify()
���ܣ���notify_url����֤
�����Bool  ��֤�����true/false

function verifyReturn()
���ܣ���return_url����֤
�����Bool  ��֤�����true/false

function getMysign($para_temp)
���ܣ����ݷ�����������Ϣ������ǩ�����
���룺Array $para_temp ֪ͨ�������Ĳ�������
�����String ���ɵ�ǩ�����

function getResponse($notify_id)
���ܣ���ȡԶ�̷�����ATN���,��֤����URL
���룺String $notify_id ֪ͨУ��ID
�����String ������ATN���

��������������������������������������������������������������

alipay_submit.class.php

function buildRequestPara($para_temp,$aliapy_config)
���ܣ����ݷ�����������Ϣ������ǩ�����
���룺Array $para_temp ����ǰ�Ĳ�������
      Array $aliapy_config ����������Ϣ����
�����String Ҫ����Ĳ�������

function buildRequestPara($para_temp,$aliapy_config)
���ܣ����ݷ�����������Ϣ������ǩ�����
���룺Array $para_temp ����ǰ�Ĳ�������
      Array $aliapy_config ����������Ϣ����
�����String Ҫ����Ĳ��������ַ���

function buildForm($para_temp, $gateway, $method, $button_name,$aliapy_config)
���ܣ������ύ��HTML����
���룺Array $para_temp ����ǰ�Ĳ�������
      String $gateway ���ص�ַ
      String $method �ύ��ʽ������ֵ��ѡ��post��get
      String $button_name ȷ�ϰ�ť��ʾ����
      Array $aliapy_config ����������Ϣ����
�����String �ύ��HTML�ı�

function sendPostInfo($para_temp, $gateway, $aliapy_config)
���ܣ�����ģ��Զ��HTTP��POST���󣬻�ȡ֧�����ķ���XML������
���룺Array $para_temp ����ǰ�Ĳ�������
      String $gateway ���ص�ַ
      Array $aliapy_config ����������Ϣ����
�����DOMDocument ֧��������XML������

��������������������������������������������������������������

alipay_service.class.php

function create_direct_pay_by_user($para_temp)
���ܣ����켴ʱ���ʽӿ�
���룺Array $para_temp �����������
�����String �ύ��HTML�ı�

function query_timestamp() 
���ܣ����ڷ����㣬���ýӿ�query_timestamp����ȡʱ����Ĵ�����
�����String ʱ����ַ���

function alipay_interface($para_temp)
���ܣ�����֧���������ӿ�(ʾ��)
���룺Array $para_temp �����������
�����String ���ύHTML��Ϣ/֧��������XML������


��������������������
 �������⣬��������
��������������������

����ڼ���֧�����ӿ�ʱ�������ʻ�������⣬��ʹ����������ӣ��ύ���롣
https://b.alipay.com/support/helperApply.htm?action=supportHome
���ǻ���ר�ŵļ���֧����ԱΪ������




