Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports System.Xml

Namespace AlipayClass
    ' 功能：支付宝接口公用函数类
    ' 详细：该类是请求、通知返回两个文件所调用的公用函数核心处理文件，不需要修改
    ' 版本：3.1
    ' 修改日期：2010-11-16
    ' 说明：
    ' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    ' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    Public Class alipay_function

        '生成签名结果
        '输入 String() sArray 要签名的数组
        '     String   key 安全校验码
        '     String   sign_type 签名类型
        '     String   _input_charset 编码格式
        '输出 String 签名结果字符串
        Public Shared Function build_mysign(ByVal sArray As String(), ByVal key As String, ByVal sign_type As String, ByVal input_charset As String) As String
            Dim prestr, mysign As String
            Dim nLen As Integer

            prestr = create_linkstring(sArray)      '把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            '去掉最後一個&字符
            nLen = Len(prestr)
            prestr = Left(prestr, nLen - 1)

            prestr = prestr & key                   '把拼接后的字符串再与安全校验码直接连接起来
            mysign = Sign(prestr, sign_type, input_charset)   '把最终的字符串签名，获得签名结果

            build_mysign = mysign
        End Function

        '把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        '输入 String() sArray 需要拼接的数组
        '输出 String 拼接完成以后的字符串
        Public Shared Function create_linkstring(ByVal sArray As String()) As String
            Dim nCount, i As Integer
            Dim prestr As StringBuilder = New StringBuilder()
            nCount = UBound(sArray)

            For i = 0 To nCount
                prestr = prestr.Append(sArray(i) & "&")
            Next

            create_linkstring = prestr.ToString()
        End Function

        '除去数组中的空值和签名参数
        '输入 String() sArray 签名参数组
        '输出 String() 去掉空值与签名参数后的新签名参数组
        Public Shared Function para_filter(ByVal sArray As String()) As String()
            Dim para As String()
            Dim nCount As Integer
            Dim i, j As Integer

            nCount = UBound(sArray)
            j = 0
            For i = 0 To nCount
                '把sArray的数组里的元素格式：变量名=值，分割开来
                Dim pos, nLen As Integer
                Dim itemName, itemValue As String
                pos = InStr(sArray(i), "=")          '获得=字符的位置
                nLen = Len(sArray(i))               '获得字符串长度
                itemName = Left(sArray(i), pos - 1)    '获得变量名
                itemValue = Right(sArray(i), nLen - pos) '获得变量的值

                If itemName <> "sign" And itemName <> "sign_type" And itemValue <> "" Then
                    ReDim Preserve para(j)
                    para(j) = sArray(i)
                    j = j + 1
                End If
            Next
            para_filter = para
        End Function

        '对数组排序
        '输入 String() sArray 排序前的数组
        '输出 String() 排序后的数组
        Public Shared Function arg_sort(ByVal sArray As String()) As String()
            '交换标志
            Dim i, j As Integer
            Dim temp As String
            Dim exchange As Boolean
            For i = 0 To sArray.Length - 1 '最多做R.Length-1趟排序
                exchange = False '本趟排序开始前，交换标志应为假
                For j = sArray.Length - 2 To i Step -1
                    If System.String.CompareOrdinal(sArray(j + 1), sArray(j)) < 0 Then  '交换条件
                        temp = sArray(j + 1)
                        sArray(j + 1) = sArray(j)
                        sArray(j) = temp
                        exchange = True '发生了交换，故将交换标志置为真
                    End If
                Next
                If Not exchange Then '本趟排序未发生交换，提前终止算法
                    Exit For
                End If
            Next
            arg_sort = sArray
        End Function

        '签名字符串
        '输入 String prestr 需要签名的字符串
        '     String sign_type 签名类型
        '     String input_charset 编码格式
        '输出 String 签名结果
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

        '写日志，方便测试（看网站需求，也可以改成存入数据库）
        '输入 String sWord 要写入日志里的文本内容
        Public Shared Sub log_result(ByVal sPath As String, ByVal sWord As String)
            Dim sw As StreamWriter = New StreamWriter(sPath, True) 'Application.StartupPath
            sw.WriteLine(sWord)
            sw.Close()
            sw = Nothing
        End Sub

        '用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
        '注意：远程解析XML出错，与IIS服务器配置有关
        '输入 String partner 合作身份者ID
        '输出 String 时间戳字符串
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