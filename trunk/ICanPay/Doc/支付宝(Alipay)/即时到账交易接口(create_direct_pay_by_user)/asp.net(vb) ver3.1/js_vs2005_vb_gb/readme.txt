            �q�����������������������������������������������r
  �q����������           ֧��������ʾ���ṹ˵��             �����������r
  ��        �t�����������������������������������������������s        ��
����                                                                  ��
����     �ӿ����ƣ�֧������ʱ���ʽӿڣ�create_direct_pay_by_user��    ��
������   ����汾��3.1                                                ��
  ��     �������ԣ�ASP.NET(VB)                                        ��
  ��     ��    Ȩ��֧�������й������缼�����޹�˾                     ��
����     �� �� �ߣ�֧�����̻���ҵ������֧����                         ��
  ��     ��ϵ��ʽ���̻�����绰0571-88158090                          ��
  ��                                                                  ��
  �t�������������������������������������������������������������������s

��������������
 �����ļ��ṹ
��������������

js_vs2005_vb_gb
  ��
  ��app_code �����������������������ļ���
  ��  ��
  ��  ��alipay_config.vb ��������������Ϣ�����������ļ�
  ��  ��
  ��  ��alipay_function.vb ���������ú������ļ�
  ��  ��
  ��  ��alipay_notify.vb ��������֧����֪ͨ�������ļ�
  ��  ��
  ��  ��alipay_service.vb��������֧�������������ļ�
  ��
  ��images ����������������������ͼƬ��CSS��ʽ�ļ���
  ��
  ��log����������������������������־�ļ���
  ��
  ��alipayto.aspx����������������֧�����ӿ�����ļ�
  ��alipayto.aspx.vb ������������֧�����ӿ�����ļ�
  ��
  ��default.aspx �������������������ٸ������ģ���ļ�
  ��default.aspx.vb�����������������ٸ������ģ���ļ�
  ��
  ��notify_url.aspx���������������������첽֪ͨҳ���ļ�
  ��notify_url.aspx.vb �����������������첽֪ͨҳ���ļ�
  ��
  ��return_url.aspx��������������ҳ����תͬ��֪ͨ�ļ�
  ��return_url.aspx.vb ����������ҳ����תͬ��֪ͨ�ļ�
  ��
  ��Web.Config �����������������������ļ�������ʱɾ����
  ��
  ��readme.txt ������������������ʹ��˵���ı�

��ע���
��Ҫ���õ��ļ��ǣ�alipay_config.vb��alipayto.aspx��alipayto.aspx.vb
ͳһ�����ռ�Ϊ��namespace AlipayClass

index.aspx����֧�����ṩ�ĸ������ģ���ļ�����ѡ��ʹ�á�
����̻���վ����ҵ��������Ҫʹ�ã����alipayto.aspx��Ϊ���̻���վ��վ���ν�ҳ�档
�����Ҫʹ��default.aspx����ôalipayto.aspx�ļ�������ģ�ֻ�����ú�alipay_config.vb�ļ�
�õ�default.aspxҳ�����̻���վ�е�HTTP·���������̻���վ����Ҫ��λ�ã�����ֱ��ʹ��֧�����ӿڡ�



������������������
 ���ļ������ṹ
������������������

alipay_function.vb

Public Shared Function build_mysign(ByVal sArray As String(), ByVal key As String, ByVal sign_type As String, ByVal input_charset As String) As String
���ܣ�����ǩ�����
���룺String() sArray Ҫǩ��������
      string   key ��ȫУ����
      string   sign_type ǩ������
      string   _input_charset �����ʽ
�����string   ǩ������ַ���

Public Shared Function create_linkstring(ByVal sArray As String()) As String
���ܣ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
���룺String() sArray ��Ҫƴ�ӵ�����
�����String   ƴ������Ժ���ַ���

Public Shared Function para_filter(ByVal sArray As String()) As String()
���ܣ���ȥ�����еĿ�ֵ��ǩ������
���룺String() sArray ����ǰ�Ĳ�����
�����String() ȥ����ֵ��ǩ�����������ǩ��������

Public Shared Function arg_sort(ByVal sArray As String()) As String()
���ܣ�����������
���룺String() sArray ����ǰ������
�����String() ����������

Public Shared Function Sign(ByVal prestr As String, ByVal sign_type As String, ByVal _input_charset As String) As String
���ܣ�ǩ���ַ���
���룺String prestr ��Ҫǩ�����ַ���
      String sign_type ǩ������
      String _input_charset �����ʽ
�����String ǩ�����

Public Shared Function query_timestamp(ByVal partner As String) As String
���ܣ����ڷ����㣬���ýӿ�query_timestamp����ȡʱ����Ĵ�����
���룺String partner ���������ID
�����String ʱ����ַ���

Public Shared Sub log_result(ByVal sPath As String, ByVal sWord As String)
���ܣ�д��־��������ԣ�����վ����Ҳ���Ըĳɴ������ݿ⣩
���룺String sPath ��־�ı��ؾ���·��
      String sWord Ҫд����־����ı�����

��������������������������������������������������������������

alipay_notify.vb

Public Sub alipay_notify(ByVal inputPara As String(), ByVal notify_id As String, ByVal partner As String, ByVal key As String, ByVal input_charset As String, ByVal sign_type As String, ByVal transport As String)
���ܣ����캯��
      �������ļ��г�ʼ������
���룺String() inputPara ֪ͨ�������Ĳ�������
      String notify_id ��֤֪ͨID
      String partner ���������ID
      String key ��ȫУ����
      String input_charset �����ʽ
      String sign_type ǩ������
      String transport ����ģʽ

Private Function Verify(ByVal notify_id As String) As String
���ܣ���֤�Ƿ���֧��������������������
���룺String notify_id ��֤֪ͨID
�����String ��֤���

Function Get_Http(ByVal a_strUrl As String, ByVal timeout As Integer) As String
���ܣ���ȡԶ�̷�����ATN���
���룺String strUrl ָ��URL·����ַ
      Integer timeout ��ʱʱ������
�����String ������ATN����ַ���

��������������������������������������������������������������

alipay_service.vb

Public Sub alipay_service(ByVal inputPara As String(), ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)
���ܣ����캯��
      �������ļ�������ļ��г�ʼ������
���룺String() inputPara ��Ҫǩ���Ĳ�������
      String key ��ȫ������
      String input_charset �ַ������ʽ Ŀǰ֧�� gbk �� utf-8
      String sign_type ǩ����ʽ �����޸�

Public Function Build_Form() As String
���ܣ�������ύHTML
�����String ���ύHTML�ı�

��������������������������������������������������������������

return_url.aspx.vb

Private Function GetRequestGet() As String()
���ܣ���ȡ֧����GET����֪ͨ��Ϣ�����ԡ�������=����ֵ������ʽ�������
�����String() request��������Ϣ��ɵ�����

��������������������������������������������������������������

notify_url.aspx.vb

Private Function GetRequestPost() As String()
���ܣ���ȡ֧����POST����֪ͨ��Ϣ�����ԡ�������=����ֵ������ʽ�������
�����String() request��������Ϣ��ɵ�����

��������������������
 �������⣬��������
��������������������

����ڼ���֧�����ӿ�ʱ�������ʻ�������⣬��ʹ����������ӣ��ύ���롣
https://b.alipay.com/support/helperApply.htm?action=supportHome
���ǻ���ר�ŵļ���֧����ԱΪ������




