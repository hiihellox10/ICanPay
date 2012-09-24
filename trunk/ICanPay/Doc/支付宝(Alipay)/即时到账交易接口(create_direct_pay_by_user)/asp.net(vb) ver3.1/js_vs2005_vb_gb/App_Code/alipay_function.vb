Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports System.Xml

Namespace AlipayClass
    ' ���ܣ�֧�����ӿڹ��ú�����
    ' ��ϸ������������֪ͨ���������ļ������õĹ��ú������Ĵ����ļ�������Ҫ�޸�
    ' �汾��3.1
    ' �޸����ڣ�2010-11-16
    ' ˵����
    ' ���´���ֻ��Ϊ�˷����̻����Զ��ṩ���������룬�̻����Ը����Լ���վ����Ҫ�����ռ����ĵ���д,����һ��Ҫʹ�øô��롣
    ' �ô������ѧϰ���о�֧�����ӿ�ʹ�ã�ֻ���ṩһ���ο���
    Public Class alipay_function

        '����ǩ�����
        '���� String() sArray Ҫǩ��������
        '     String   key ��ȫУ����
        '     String   sign_type ǩ������
        '     String   _input_charset �����ʽ
        '��� String ǩ������ַ���
        Public Shared Function build_mysign(ByVal sArray As String(), ByVal key As String, ByVal sign_type As String, ByVal input_charset As String) As String
            Dim prestr, mysign As String
            Dim nLen As Integer

            prestr = create_linkstring(sArray)      '����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
            'ȥ������һ��&�ַ�
            nLen = Len(prestr)
            prestr = Left(prestr, nLen - 1)

            prestr = prestr & key                   '��ƴ�Ӻ���ַ������밲ȫУ����ֱ����������
            mysign = Sign(prestr, sign_type, input_charset)   '�����յ��ַ���ǩ�������ǩ�����

            build_mysign = mysign
        End Function

        '����������Ԫ�أ����ա�����=����ֵ����ģʽ�á�&���ַ�ƴ�ӳ��ַ���
        '���� String() sArray ��Ҫƴ�ӵ�����
        '��� String ƴ������Ժ���ַ���
        Public Shared Function create_linkstring(ByVal sArray As String()) As String
            Dim nCount, i As Integer
            Dim prestr As StringBuilder = New StringBuilder()
            nCount = UBound(sArray)

            For i = 0 To nCount
                prestr = prestr.Append(sArray(i) & "&")
            Next

            create_linkstring = prestr.ToString()
        End Function

        '��ȥ�����еĿ�ֵ��ǩ������
        '���� String() sArray ǩ��������
        '��� String() ȥ����ֵ��ǩ�����������ǩ��������
        Public Shared Function para_filter(ByVal sArray As String()) As String()
            Dim para As String()
            Dim nCount As Integer
            Dim i, j As Integer

            nCount = UBound(sArray)
            j = 0
            For i = 0 To nCount
                '��sArray���������Ԫ�ظ�ʽ��������=ֵ���ָ��
                Dim pos, nLen As Integer
                Dim itemName, itemValue As String
                pos = InStr(sArray(i), "=")          '���=�ַ���λ��
                nLen = Len(sArray(i))               '����ַ�������
                itemName = Left(sArray(i), pos - 1)    '��ñ�����
                itemValue = Right(sArray(i), nLen - pos) '��ñ�����ֵ

                If itemName <> "sign" And itemName <> "sign_type" And itemValue <> "" Then
                    ReDim Preserve para(j)
                    para(j) = sArray(i)
                    j = j + 1
                End If
            Next
            para_filter = para
        End Function

        '����������
        '���� String() sArray ����ǰ������
        '��� String() ����������
        Public Shared Function arg_sort(ByVal sArray As String()) As String()
            '������־
            Dim i, j As Integer
            Dim temp As String
            Dim exchange As Boolean
            For i = 0 To sArray.Length - 1 '�����R.Length-1������
                exchange = False '��������ʼǰ��������־ӦΪ��
                For j = sArray.Length - 2 To i Step -1
                    If System.String.CompareOrdinal(sArray(j + 1), sArray(j)) < 0 Then  '��������
                        temp = sArray(j + 1)
                        sArray(j + 1) = sArray(j)
                        sArray(j) = temp
                        exchange = True '�����˽������ʽ�������־��Ϊ��
                    End If
                Next
                If Not exchange Then '��������δ������������ǰ��ֹ�㷨
                    Exit For
                End If
            Next
            arg_sort = sArray
        End Function

        'ǩ���ַ���
        '���� String prestr ��Ҫǩ�����ַ���
        '     String sign_type ǩ������
        '     String input_charset �����ʽ
        '��� String ǩ�����
        Public Shared Function Sign(ByVal prestr As String, ByVal sign_type As String, ByVal _input_charset As String) As String
            Dim sResult As String
            If sign_type = "MD5" Then
                Dim md5 As MD5
                md5 = New MD5CryptoServiceProvider()
                Dim t As Byte() = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr))
                Dim sb As StringBuilder = New StringBuilder(32)
                For i As Integer = 0 To t.Length - 1
                    sb.Append(t(i).ToString("x").PadLeft(2, "0"))
                Next

                sResult = sb.ToString()
            Else
                sResult = ""
            End If

            Sign = sResult
        End Function

        'д��־��������ԣ�����վ����Ҳ���Ըĳɴ������ݿ⣩
        '���� String sWord Ҫд����־����ı�����
        Public Shared Sub log_result(ByVal sPath As String, ByVal sWord As String)
            Dim sw As StreamWriter = New StreamWriter(sPath, True) 'Application.StartupPath
            sw.WriteLine(sWord)
            sw.Close()
            sw = Nothing
        End Sub

        '���ڷ����㣬���ýӿ�query_timestamp����ȡʱ����Ĵ�����
        'ע�⣺Զ�̽���XML������IIS�����������й�
        '���� String partner ���������ID
        '��� String ʱ����ַ���
        Public Shared Function query_timestamp(ByVal partner As String) As String
            Dim url As String = "https://mapi.alipay.com/gateway.do?service=query_timestamp&partner=" & partner
            Dim encrypt_key As String

            Dim Reader As XmlTextReader = New XmlTextReader(url)
            Dim xmlDoc As XmlDocument = New XmlDocument()
            xmlDoc.Load(Reader)

            encrypt_key = xmlDoc.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText

            query_timestamp = encrypt_key
        End Function

    End Class
End Namespace