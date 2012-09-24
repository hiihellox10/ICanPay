Imports Microsoft.VisualBasic
Imports System.Text
Imports System.IO
Imports System.Net

Namespace AlipayClass
    '������alipay_notify
    '���ܣ���������з�����֪ͨ��
    '��ϸ����ҳ����֪ͨ���غ��Ĵ����ļ�������Ҫ�޸�
    '�汾��3.1
    '�޸����ڣ�2010-11-16
    '˵����
    '���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
    '�ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���

    '''''''''''''''''''''''ע��''''''''''''''''''''''''
    '����֪ͨ����ʱ���ɲ鿴���дlog��־��д��TXT������ݣ������֪ͨ�����Ƿ�����
    '''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class alipay_notify
        Private gateway As String = ""          '���ص�ַ
        Private _partner As String = ""         '���������ID
        Private _key As String = ""             '���װ�ȫУ����
        Private _input_charset As String = ""   '�����ʽ
        Private _sign_type As String = ""       'ǩ����ʽ
        Private _mysign As String = ""           'ǩ�����
        Private _responseTxt As String = ""      '������ATN���
        Private sPara As String()               '��Ҫǩ�����Ѿ����˺�Ĳ�������
        Private _preSignStr As String = ""       '��ǩ�����ַ���

        Public Property Mysign() As String
            Get
                Return _mysign
            End Get
            Set(ByVal value As String)
                _mysign = value
            End Set
        End Property

        Public Property ResponseTxt() As String
            Get
                Return _responseTxt
            End Get
            Set(ByVal value As String)
                _responseTxt = value
            End Set
        End Property

        Public Property PreSignStr() As String
            Get
                Return _preSignStr
            End Get
            Set(ByVal value As String)
                _preSignStr = value
            End Set
        End Property

        '���캯��
        '�������ļ��г�ʼ������
        '���룺String() inputPara ֪ͨ�������Ĳ�������
        '      String notify_id ��֤֪ͨID
        '      String partner ���������ID
        '      String key ��ȫУ����
        '      String input_charset �����ʽ
        '      String sign_type ǩ������
        Public Sub alipay_notify(ByVal inputPara As String(), ByVal notify_id As String, ByVal partner As String, ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)

            gateway = "https://mapi.alipay.com/gateway.do?"

            _partner = partner.Trim()
            _key = key.Trim()
            _input_charset = input_charset
            _sign_type = sign_type.ToUpper()

            Dim sParaTemp As String() = alipay_function.para_filter(inputPara)  '���˿�ֵ��sign��sign_type����
            sPara = alipay_function.arg_sort(sParaTemp)                         '�õ�����ĸa��z������ǩ����������
            _preSignStr = alipay_function.create_linkstring(sPara)              '��ȡ��ǩ���ַ����������ã�
            '���ǩ�����
            Mysign = alipay_function.build_mysign(sPara, _key, _sign_type, _input_charset)

            '��ȡԶ�̷�����ATN�������֤�Ƿ���֧��������������������
            ResponseTxt = Verify(notify_id)
        End Sub

        '��֤�Ƿ���֧��������������������
        '���� String notify_id ��֤֪ͨID
        '��� String ��֤���
        Private Function Verify(ByVal notify_id As String) As String
            Dim veryfy_url As String = ""
            veryfy_url = gateway & "service=notify_verify&partner=" & _partner & "&notify_id=" & notify_id

            Verify = Get_Http(veryfy_url, 120000)
        End Function

        '��ȡԶ�̷�����ATN���
        '���� String a_strUrl ָ��URL·����ַ
        '     String timeout ��ʱʱ������
        '��� String ������ATN����ַ���
        Function Get_Http(ByVal a_strUrl As String, ByVal timeout As Integer) As String
            Dim strResult As String = ""
            Try
                Dim myReq As HttpWebRequest = Nothing
                myReq = WebRequest.Create(a_strUrl)
                myReq.Timeout = timeout

                Dim HttpWResp As HttpWebResponse = myReq.GetResponse
                Dim myStream As Stream = HttpWResp.GetResponseStream
                Dim sr As New StreamReader(myStream, Encoding.Default)
                Dim strBuilder As New StringBuilder

                Do While (sr.Peek() <> -1)
                    strBuilder.Append(sr.ReadLine())
                Loop
                strResult = strBuilder.ToString()
            Catch ex As Exception

            End Try

            Return strResult
        End Function
    End Class
End Namespace