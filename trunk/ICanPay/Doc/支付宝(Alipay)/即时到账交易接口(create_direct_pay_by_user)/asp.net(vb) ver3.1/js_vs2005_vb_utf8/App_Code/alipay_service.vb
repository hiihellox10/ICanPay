Imports Microsoft.VisualBasic
Imports System.Text

Namespace AlipayClass
    '������alipay_service
    '���ܣ�֧�����ⲿ����ӿڿ���
    '��ϸ����ҳ��������������Ĵ����ļ�������Ҫ�޸�
    '�汾��3.1
    '�޸����ڣ�2010-11-16
    '˵����
    '���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
    '�ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο�
    Public Class alipay_service

        Private gateway As String = ""          '���ص�ַ
        Private _key As String = ""             '���װ�ȫУ����
        Private _input_charset As String = ""   '�����ʽ
        Private _sign_type As String = ""       'ǩ����ʽ
        Private mysign As String = ""           'ǩ�����
        Private sPara As String()                '��Ҫǩ�����Ѿ����˺�Ĳ�������

        '���캯��
        '�������ļ�������ļ��г�ʼ������
        '���� String() inputPara ��Ҫǩ���Ĳ�������
        '     String   key ��ȫ������
        '     String input_charset �ַ������ʽ Ŀǰ֧�� gbk �� utf-8
        '     String sign_type ǩ����ʽ �����޸�
        Public Sub alipay_service(ByVal inputPara As String(), ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)
            gateway = "https://mapi.alipay.com/gateway.do?"
            _key = key.Trim()
            _input_charset = input_charset.Trim().ToLower()
            _sign_type = sign_type.Trim().ToUpper()

            Dim sParaTemp As String() = alipay_function.para_filter(inputPara)
            sPara = alipay_function.arg_sort(sParaTemp)     '�õ�����ĸa��z������ǩ����������
            '���ǩ�����
            mysign = alipay_function.build_mysign(sPara, _key, _sign_type, _input_charset)
        End Sub

        '������ύHTML
        '��� ���ύHTML�ı�
        Public Function Build_Form() As String
            Dim sHtml As StringBuilder = New StringBuilder()

            'GET��ʽ����
            sHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" & gateway & "_input_charset=" & _input_charset & "' method='get'>")

            'POST��ʽ���ݣ�GET��POST����ѡһ��
            'sHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" & gateway & "_input_charset=" & _input_charset & "' method='post'>")

            Dim nCount As Integer = UBound(sPara)
            Dim i, pos, nLen As Integer
            Dim itemName, itemValue As String
            For i = 0 To nCount
                '��sArray���������Ԫ�ظ�ʽ��������=ֵ���ָ��
                pos = InStr(sPara(i), "=")           '���=�ַ���λ��
                nLen = Len(sPara(i))                '����ַ�������
                itemName = Left(sPara(i), pos - 1)     '��ñ�����
                itemValue = Right(sPara(i), nLen - pos) '��ñ�����ֵ

                sHtml = sHtml.Append("<input type='hidden' name='" & itemName & "' value='" & itemValue & "'/>")
            Next

            sHtml = sHtml.Append("<input type='hidden' name='sign' value='" & mysign & "'/>")
            sHtml = sHtml.Append("<input type='hidden' name='sign_type' value='" & _sign_type & "'/>")

            'submit��ť�ؼ��벻Ҫ����name����
            sHtml = sHtml.Append("<input type=""submit"" value=""֧����ȷ�ϸ���""></form>")

            sHtml = sHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>")

            Build_Form = sHtml.ToString()
        End Function

    End Class
End Namespace