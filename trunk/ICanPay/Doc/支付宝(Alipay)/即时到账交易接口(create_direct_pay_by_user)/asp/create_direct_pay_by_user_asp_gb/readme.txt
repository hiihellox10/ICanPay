
                �q�����������������������������������������������r
  �q��������������           ֧��������ʾ���ṹ˵��             ���������������r
  ��            �t�����������������������������������������������s            ��
����                                                                          ��
����     �ӿ����ƣ�֧������ʱ���ʽӿڣ�create_direct_pay_by_user��            ��
������   ����汾��3.2                                                        ��
  ��     �������ԣ�ASP                                                        ��
  ��     ��    Ȩ��֧�������й������缼�����޹�˾                             ��
����     �� �� �ߣ�֧�����̻���ҵ������֧����                                 ��
  ��     ��ϵ��ʽ���̻�����绰0571-88158090                                  ��
  ��                                                                          ��
  �t���������������������������������������������������������������������������s


��������������
 �����ļ��ṹ
��������������

create_direct_pay_by_user_asp_gb
  ��
  ��class�����������������������������������������ļ���
  ��  ��
  ��  ��alipay_core.asp������������������������֧�����ӿڹ��ú����ļ�
  ��  ��
  ��  ��alipay_md5.asp ������������������������MD5ǩ�������ļ�
  ��  ��
  ��  ��alipay_notify.asp����������������������֧����֪ͨ�������ļ�
  ��  ��
  ��  ��alipay_submit.asp����������������������֧�������ӿ������ύ���ļ�
  ��  ��
  ��  ��alipay_service.asp ��������������������֧�������ӿڹ������ļ�
  ��  ��
  ��  ��alipay_config.asp�������������������������������ļ�
  ��
  ��log������������������������������������������־�ļ���
  ��
  ��alipayto.asp ������������������������������֧�����ӿ�����ļ�
  ��
  ��index.asp����������������������������������֧�����������ҳ��
  ��
  ��notify_url.asp �����������������������������������첽֪ͨҳ���ļ�
  ��
  ��return_url.asp ����������������������������ҳ����תͬ��֪ͨ�ļ�
  ��
  ��readme.txt ��������������������������������ʹ��˵���ı�

��ע���
��Ҫ���õ��ļ��ǣ�alipay_config.asp��alipayto.asp��notify_url.asp��return_url.asp




������������������
 ���ļ������ṹ
������������������

alipay_core.asp

Function BuildMysign(sPara, key, sign_type,input_charset)
���ܣ�����ǩ�����
���룺Array  sPara Ҫǩ��������
      String key ��ȫУ����
      String sign_type ǩ������
�����String ǩ������ַ���

Function CreateLinkstring(sPara)
���ܣ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
���룺Array  sPara ��Ҫƴ�ӵ�����
�����String ƴ������Ժ���ַ���

Function CreateLinkstringUrlEncode(sPara)
���ܣ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ��������Ҷ�����URLENCODE����
���룺Array  sPara ��Ҫƴ�ӵ�����
�����String ƴ������Ժ���ַ���

Function FilterPara(sPara)
���ܣ���ȥ�����еĿ�ֵ��ǩ������
���룺Array  sPara ǩ��������
�����Array  ȥ����ֵ��ǩ�����������ǩ��������

Function SortPara(sPara)
���ܣ�����������
���룺Array  sPara ����ǰ������
�����Array  ����������

Function Sign(prestr,sign_type,input_charset)
���ܣ�ǩ���ַ���
���룺String prestr ��Ҫǩ�����ַ���
      String sign_type ǩ������
      String input_charset �����ʽ
�����String ǩ�����

Function LogResult(sWord)
���ܣ�д��־��������ԣ�����վ����Ҳ���Ըĳɴ������ݿ⣩
���룺String sWord Ҫд����־����ı�����

Function GetDateTimeFormat()
���ܣ���ȡ��ǰʱ��
��ʽ����[4λ]-��[2λ]-��[2λ] Сʱ[2λ 24Сʱ��]:��[2λ]:��[2λ]���磺2007-10-01 13:13:13
�����String ʱ���ʽ�����
˵��������

Function GetDateTime()
���ܣ���ȡ��ǰʱ��
��ʽ����[4λ]��[2λ]��[2λ]Сʱ[2λ 24Сʱ��]��[2λ]��[2λ]���磺20071001131313
�����String ʱ���ʽ�����

Function DelStr(Str)
���ܣ����������ַ�
���룺String Str Ҫ�����˵��ַ���
�����String �ѱ����˵������ַ���

��������������������������������������������������������������

alipay_md5.asp

Public Function MD5(sMessage,input_charset)
���ܣ�MD5ǩ��
���룺String sMessage Ҫǩ�����ַ���
      String input_charset �����ʽ��utf-8��gbk
�����String ǩ�����

��������������������������������������������������������������

alipay_notify.asp

Public Function VerifyNotify()
���ܣ����notify_url��֤��Ϣ�Ƿ���֧���������ĺϷ���Ϣ
�����Bool  ��֤�����true/false

Public Function VerifyReturn()
���ܣ����return_url��֤��Ϣ�Ƿ���֧���������ĺϷ���Ϣ
�����Bool  ��֤�����true/false

Private Function GetMysign(sParaTemp)
���ܣ����ݷ�����������Ϣ������ǩ�����
���룺Array sParaTemp ֪ͨ�������Ĳ�������
��������ɵ�ǩ�����

Private Function GetResponse(notify_id)
���ܣ���ȡԶ�̷�����ATN���
���룺string notify_id ֪ͨУ��ID
�����������ATN����ַ���

Private Function GetRequestGet()
���ܣ���ȡ֧����GET����֪ͨ��Ϣ�����ԡ�������=����ֵ������ʽ�������
�����Array  request��������Ϣ��ɵ�����

Private Function GetRequestPost()
���ܣ���ȡ֧����POST����֪ͨ��Ϣ�����ԡ�������=����ֵ������ʽ�������
�����Array  request��������Ϣ��ɵ�����

��������������������������������������������������������������

alipay_submit.asp

Private Function BuildRequestPara(sParaTemp, key, sign_type, input_charset)
���ܣ�����Ҫ�����֧�����Ĳ�������
���룺Array sParaTemp ����ǰ�Ĳ�������
      string key ���װ�ȫУ����
      string sign_type ǩ������
      string input_charset �����ʽ
�����Ҫ����Ĳ�������

Private Function BuildRequestParaToString(sParaTemp, key, sign_type, input_charset)
���ܣ�����Ҫ�����֧�����Ĳ�������
���룺Array sParaTemp ����ǰ�Ĳ�������
      string key ���װ�ȫУ����
      string sign_type ǩ������
      string input_charset �����ʽ
�����Ҫ����Ĳ��������ַ���

Public Function BuildFormHtml(sParaTemp, key, sign_type, input_charset, gateway, sMethod, sButtonValue)
���ܣ������ύ��HTML����
���룺Array sParaTemp ����ǰ�Ĳ�������
      string key ���װ�ȫУ����
      string sign_type ǩ������
      string input_charset �����ʽ
      string gateway ���ص�ַ
      string sMethod �ύ��ʽ������ֵ��ѡ��post��get
      string sButtonValue ȷ�ϰ�ť��ʾ����
������ύ��HTML�ı�

Public Function SendGetInfo(sParaTemp, key, sign_type, input_charset, gateway, sParaNode)
���ܣ�����ģ��Զ��HTTP��GET���󣬻�ȡ֧�����ķ���XML������
���룺Array sParaTemp ����ǰ�Ĳ�������
      string key ���װ�ȫУ����
      string sign_type ǩ������
      string input_charset �����ʽ
      string gateway ���ص�ַ
      Array sParaNode Ҫ�����XML�ڵ���
�����֧��������XMLָ���ڵ�����

��������������������������������������������������������������

alipay_service.asp

Public Function Create_direct_pay_by_user(sParaTemp)
���ܣ����켴ʱ���ʽӿ�
���룺Array sParaTemp �����������
�����string ���ύHTML��Ϣ

Public Function Query_timestamp()
���ܣ����ڷ����㣬���ýӿ�query_timestamp����ȡʱ����Ĵ�����
�����String ʱ����ַ���

Public Function Alipay_interface(sParaTemp)
���ܣ�����(֧�����ӿ�����)�ӿ�
���룺Array sParaTemp �����������
��������ύHTML�ı�����֧��������XML������


��������������������
 �������⣬��������
��������������������

����ڼ���֧�����ӿ�ʱ�������ʻ�������⣬��ʹ����������ӣ��ύ���롣
https://b.alipay.com/support/helperApply.htm?action=supportHome
���ǻ���ר�ŵļ���֧����ԱΪ������




