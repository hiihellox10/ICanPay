Imports Microsoft.VisualBasic

Namespace AlipayClass
    '���ܣ������ʻ��й���Ϣ������·������������ҳ�棩
    '�汾��3.1
    '���ڣ�2010-11-16
    '˵����
    '���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
    '�ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���

    '��ʾ����λ�ȡ��ȫУ����ͺ��������ID
    '1.����֧�����̻���������(b.alipay.com)��Ȼ��������ǩԼ֧�����˺ŵ�½.
    '2.���ʡ��������񡱡������ؼ��������ĵ�����https://b.alipay.com/support/helperApply.htm?action=selfIntegration��
    '3.�ڡ��������ɰ������У���������������(Partner ID)��ѯ��������ȫУ����(Key)��ѯ��

    '��ȫУ����鿴ʱ������֧�������ҳ��ʻ�ɫ��������ô�죿
    '���������
    '1�������������ã������������������������
    '2���������������ԣ����µ�¼��ѯ��
    Public Class alipay_config
        '��������������޸ģ�
        Private _partner As String = ""
        Private _key As String = ""
        Private _seller_email As String = ""
        Private _return_url As String = ""
        Private _notify_url As String = ""
        Private _input_charset As String = ""
        Private _sign_type As String = ""
        Private _show_url As String = ""
        Private _mainname As String = ""

        ''' <summary>
        ''' ��ȡ�����ú��������ID
        ''' </summary>
        Public Property Partner() As String
            Get
                Return _partner
            End Get
            Set(ByVal value As String)
                _partner = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ�����ý��װ�ȫУ����
        ''' </summary>
        Public Property Key() As String
            Get
                Return _key
            End Get
            Set(ByVal value As String)
                _key = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ������ǩԼ������֧�����˺�
        ''' </summary>
        Public Property Seller_email() As String
            Get
                Return _seller_email
            End Get
            Set(ByVal value As String)
                _seller_email = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ�����ø�����ɺ���תҳ��·��
        ''' </summary>
        Public Property Return_url() As String
            Get
                Return _return_url
            End Get
            Set(ByVal value As String)
                _return_url = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ�����÷������첽֪ͨҳ��·��
        ''' </summary>
        Public Property Notify_url() As String
            Get
                Return _notify_url
            End Get
            Set(ByVal value As String)
                _notify_url = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ�������ַ������ʽ
        ''' </summary>
        Public Property Input_charset() As String
            Get
                Return _input_charset
            End Get
            Set(ByVal value As String)
                _input_charset = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ������ǩ����ʽ
        ''' </summary>
        Public Property Sign_type() As String
            Get
                Return _sign_type
            End Get
            Set(ByVal value As String)
                _sign_type = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ��������վ��Ʒ��չʾ��ַ
        ''' </summary>
        Public Property Show_url() As String
            Get
                Return _show_url
            End Get
            Set(ByVal value As String)
                _show_url = value
            End Set
        End Property

        ''' <summary>
        ''' ��ȡ�������տ����
        ''' </summary>
        Public Property Mainname() As String
            Get
                Return _mainname
            End Get
            Set(ByVal value As String)
                _mainname = value
            End Set
        End Property

        Public Sub alipay_config()
            '�����������������������������������Ļ�����Ϣ������������������������������

            '���������ID����2088��ͷ��16λ��������ɵ��ַ���
            _partner = ""

            '���װ�ȫ�����룬�����ֺ���ĸ��ɵ�32λ�ַ���
            _key = ""

            'ǩԼ֧�����˺Ż�����֧�����ʻ�
            _seller_email = ""

            '��������ת��ҳ�� Ҫ�� http://��ʽ������·�����������?id=123�����Զ������
            'return_url����������д��http://localhost/js_vs2005_vb_utf8/return_url.aspx ������ᵼ��return_urlִ����Ч
            _return_url = "http://www.xxx.com/js_vs2005_vb_utf8/return_url.aspx"

            '���׹����з�����֪ͨ��ҳ�� Ҫ�� http://��ʽ������·�����������?id=123�����Զ������
            _notify_url = "http://www.xxx.com/js_vs2005_vb_utf8/notify_url.aspx"

            '��վ��Ʒ��չʾ��ַ���������?id=123�����Զ������
            _show_url = "http://www.xxx.com/myorder.aspx"

            '�տ���ƣ��磺��˾���ơ���վ���ơ��տ���������
            _mainname = "�տ����"

            '�����������������������������������Ļ�����Ϣ������������������������������


            '�ַ������ʽ Ŀǰ֧�� gbk �� utf-8
            _input_charset = "utf-8"

            'ǩ����ʽ �����޸�
            _sign_type = "MD5"

            '����ģʽ,�����Լ��ķ������Ƿ�֧��ssl���ʣ���֧����ѡ��https������֧����ѡ��http
            _transport = "https"
        End Sub
    End Class
End Namespace