Imports Microsoft.VisualBasic
Imports System.Text
Imports System.IO
Imports System.Net

Namespace AlipayClass
    '类名：alipay_notify
    '功能：付款过程中服务器通知类
    '详细：该页面是通知返回核心处理文件，不需要修改
    '版本：3.1
    '修改日期：2010-11-16
    '说明：
    '以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    '该代码仅供学习和研究支付宝接口使用，只是提供一个参考。

    '''''''''''''''''''''''注意''''''''''''''''''''''''
    '调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常
    '''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Class alipay_notify
        Private gateway As String = ""          '网关地址
        Private _partner As String = ""         '合作身份者ID
        Private _key As String = ""             '交易安全校验码
        Private _input_charset As String = ""   '编码格式
        Private _sign_type As String = ""       '签名方式
        Private _mysign As String = ""           '签名结果
        Private _responseTxt As String = ""      '服务器ATN结果
        Private sPara As String()               '需要签名的已经过滤后的参数数组
        Private _preSignStr As String = ""       '待签名的字符串

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

        '构造函数
        '从配置文件中初始化变量
        '输入：String() inputPara 通知返回来的参数数组
        '      String notify_id 验证通知ID
        '      String partner 合作身份者ID
        '      String key 安全校验码
        '      String input_charset 编码格式
        '      String sign_type 签名类型
        Public Sub alipay_notify(ByVal inputPara As String(), ByVal notify_id As String, ByVal partner As String, ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)

            gateway = "https://mapi.alipay.com/gateway.do?"

            _partner = partner.Trim()
            _key = key.Trim()
            _input_charset = input_charset
            _sign_type = sign_type.ToUpper()

            Dim sParaTemp As String() = alipay_function.para_filter(inputPara)  '过滤空值、sign与sign_type参数
            sPara = alipay_function.arg_sort(sParaTemp)                         '得到从字母a到z排序后的签名参数数组
            _preSignStr = alipay_function.create_linkstring(sPara)              '获取待签名字符串（调试用）
            '获得签名结果
            Mysign = alipay_function.build_mysign(sPara, _key, _sign_type, _input_charset)

            '获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            ResponseTxt = Verify(notify_id)
        End Sub

        '验证是否是支付宝服务器发来的请求
        '输入 String notify_id 验证通知ID
        '输出 String 验证结果
        Private Function Verify(ByVal notify_id As String) As String
            Dim veryfy_url As String = ""
            veryfy_url = gateway & "service=notify_verify&partner=" & _partner & "&notify_id=" & notify_id

            Verify = Get_Http(veryfy_url, 120000)
        End Function

        '获取远程服务器ATN结果
        '输入 String a_strUrl 指定URL路径地址
        '     String timeout 超时时间设置
        '输出 String 服务器ATN结果字符串
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