
            �q�����������������������������������������������r
  �q����������           ֧��������ʾ���ṹ˵��             �����������r
  ��        �t�����������������������������������������������s        ��
����                                                                  ��
����     �ӿ����ƣ�֧������ʱ���ʽӿڣ�create_direct_pay_by_user��    ��
������   ����汾��3.2                                                ��
  ��     �������ԣ�JAVA                                               ��
  ��     ��    Ȩ��֧�������й������缼�����޹�˾                     ��
����     �� �� �ߣ�֧�����̻���ҵ������֧����                         ��
  ��     ��ϵ��ʽ���̻�����绰0571-88158090                          ��
  ��                                                                  ��
  �t�������������������������������������������������������������������s

��������������
 �����ļ��ṹ
��������������

create_direct_pay_by_user_jsp_utf8
  ��
  ��src�����������������������������������ļ���
  ��  ��
  ��  ��com.alipay.config
  ��  ��  ��
  ��  ��  ��AlipayConfig.java���������������������ļ�
  ��  ��
  ��  ��com.alipay.util
  ��  ��  ��
  ��  ��  ��AlipayService.java ��������֧�������ӿڹ������ļ�
  ��  ��
  ��  ��com.alipay.util
  ��  ��  ��
  ��  ��  ��AlipayCore.java������������֧�����ӿڹ��ú������ļ�
  ��  ��  ��
  ��  ��  ��AlipayNotify.java����������֧����֪ͨ�������ļ�
  ��  ��  ��
  ��  ��  ��AlipaySubmit.java����������֧�������ӿ������ύ���ļ�
  ��  ��  ��
  ��  ��  ��AlipayMd5Encrypt.java������֧����MD5ǩ�����ļ�
  ��  ��  ��
  ��  ��  ��UtilDate.java��������������֧�����Զ��嶩�����ļ�
  ��  ��
  ��  ��com.alipay.util.httpClient���ѷ�װ��
  ��      ��
  ��      ��HttpProtocolHandler.java ��֧����HttpClient�������ļ�
  ��      ��
  ��      ��HttpRequest.java ����������֧����HttpClient�������ļ�
  ��      ��
  ��      ��HttpResponse.java����������֧����HttpClient�������ļ�
  ��      ��
  ��      ��HttpResultType.java��������֧����HttpClient���صĽ���ַ���ʽ���ļ�
  ��
  ��WebRoot����������������������������ҳ���ļ���
  ��  ��
  ��  ��alipayto.jsp ������������������֧�����ӿ�����ļ�
  ��  ��
  ��  ��index.jsp����������������������֧�����������ҳ��
  ��  ��
  ��  ��notify_url.jsp �����������������������첽֪ͨҳ���ļ�
  ��  ��
  ��  ��return_url.jsp ����������������ҳ����תͬ��֪ͨ�ļ�
  ��  ��
  ��  ��WEB-INF
  ��   	  ��
  ��      ��lib�����JAVA��Ŀ�а�����Щ�ܰ�������Ҫ���룩
  ��   	     ��
  ��   	     ��commons-codec-1.3.jar
  ��   	     ��
  ��   	     ��commons-httpclient-3.0.1.jar
  ��   	     ��
  ��   	     ��commons-logging-1.1.1.jar
  ��   	     ��
  ��   	     ��dom4j-1.6.1.jar
  ��   	     ��
  ��   	     ��jaxen-1.1-beta-6.jar
  ��
  ��readme.txt ������������������ʹ��˵���ı�

��ע���
��Ҫ���õ��ļ��ǣ�
alipay_config.jsp��
alipayto.jsp��
return_url.jsp��
notify_url.jsp��
���ð���com.alipay.config.*��com.alipay.util.*��com.alipay.util.services

������ʾ����demo����ģ���ȡԶ��HTTP��Ϣʹ�õ���commons-httpclient-3.0�汾�ĵ������ܰ�����֧������httpClient��װ�ࡣ
���������ʹ�ø÷�ʽʵ��ģ���ȡԶ��HTTP���ܣ���ô������������ʽ���棬��ʱ�������б�д���롣





������������������
 ���ļ������ṹ
������������������

AlipayCore.java

public static String buildMysign(Map<String, String> sArray)
���ܣ�����ǩ�����
���룺Map<String, String> sArray Ҫǩ��������
�����String ǩ������ַ���

public static Map paraFilter(Map<String, String> sArray)
���ܣ���ȥ�����еĿ�ֵ��ǩ������
���룺Map<String, String> sArray Ҫǩ��������
�����Map<String, String> ȥ����ֵ��ǩ�����������ǩ��������

public static String createLinkString(Map<String, String> params)
���ܣ�����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
���룺Map<String, String> params ��Ҫƴ�ӵ�����
�����String ƴ������Ժ���ַ���

public static void logResult(String sWord)
���ܣ�д��־��������ԣ�����վ����Ҳ���Ըĳɴ������ݿ⣩
���룺String sWord Ҫд����־����ı�����

��������������������������������������������������������������

AlipayMd5Encrypt.java

public static String md5(String text)
���ܣ����ַ�������MD5ǩ��
���룺String text ����
�����String ǩ�����

��������������������������������������������������������������


AlipayNotify.java

public static boolean verify(Map<String, String> params)
���ܣ����ݷ�����������Ϣ������ǩ�����
���룺Map<String, String>  Params ֪ͨ�������Ĳ�������
�����boolean ��֤���

private static String getMysign(Map<String, String> Params)
���ܣ����ݷ�����������Ϣ������ǩ�����
���룺Map<String, String>  Params ֪ͨ�������Ĳ�������
�����String ǩ�����

private static String verifyResponse(String notify_id)
���ܣ���ȡԶ�̷�����ATN���,��֤����URL
���룺String notify_id ��֤֪ͨID
�����String ��֤���

private static String checkUrl(String urlvalue)
���ܣ���ȡԶ�̷�����ATN���
���룺String urlvalue ָ��URL·����ַ
�����String ������ATN����ַ���

��������������������������������������������������������������

AlipaySubmit.java

private static Map<String, String> buildRequestPara(Map<String, String> sParaTemp)
���ܣ�����Ҫ�����֧�����Ĳ�������
���룺Map<String, String> sParaTemp
�����Map<String, String> Ҫ����Ĳ�������

public static String buildForm(
	Map<String, String> sParaTemp, 
	String gateway, 
	String strMethod,
	String strButtonName)
���ܣ������ύ��HTML����
���룺Map<String, String> sParaTemp �����������
      String gateway ���ص�ַ
      String strMethod �ύ��ʽ������ֵ��ѡ��post��get
      String strButtonName ȷ�ϰ�ť��ʾ����
�����String �ύ��HTML�ı�

private static NameValuePair[] generatNameValuePair(Map<String, String> properties)
���ܣ�MAP��������ת����NameValuePair����
���룺Map<String, String> sParaTemp MAP��������
�����NameValuePair[] NameValuePair��������

public static String sendPostInfo(Map<String, String> sParaTemp, String gateway)throws Exception
���ܣ�����ģ��Զ��HTTP��POST���󣬻�ȡ֧�����ķ���XML������
���룺Map<String, String> sParaTemp �����������
      String gateway ���ص�ַ
�����String ֧��������XML������


��������������������������������������������������������������

AlipayService.java

public static String create_direct_pay_by_user(Map<String, String> sParaTemp)
���ܣ����켴ʱ���ʽӿ�
���룺Map<String, String> sParaTemp �����������
�����string ���ύHTML��Ϣ

public static String query_timestamp()
���ܣ����ڷ����㣬���ýӿ�query_timestamp����ȡʱ����Ĵ�����
�����String ʱ����ַ���

��������������������������������������������������������������

UtilDate.java

public  static String getOrderNum()
���ܣ��Զ����������ţ���ʽyyyyMMddHHmmss
�����String ������

public  static String getDateFormatter()
���ܣ���ȡ���ڣ���ʽ��yyyy-MM-dd HH:mm:ss
�����String ����

public static String getDate()
���ܣ���ȡ���ڣ���ʽ��yyyyMMdd
�����String ����

public static String getThree()
���ܣ������������λ��
�����String �����λ��

��������������������������������������������������������������


��������������������
 �������⣬��������
��������������������

����ڼ���֧�����ӿ�ʱ�������ʻ�������⣬��ʹ����������ӣ��ύ���롣
https://b.alipay.com/support/helperApply.htm?action=supportHome
���ǻ���ר�ŵļ���֧����ԱΪ������




