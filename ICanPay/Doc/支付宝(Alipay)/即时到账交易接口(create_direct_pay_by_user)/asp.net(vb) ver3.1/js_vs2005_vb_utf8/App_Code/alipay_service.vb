Imports Microsoft.VisualBasic
Imports System.Text

Namespace AlipayClass
    '类名：alipay_service
    '功能：支付宝外部服务接口控制
    '详细：该页面是请求参数核心处理文件，不需要修改
    '版本：3.1
    '修改日期：2010-11-16
    '说明：
    '以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    '该代码仅供学习和研究支付宝接口使用，只是提供一个参考
    Public Class alipay_service

        Private gateway As String = ""          '网关地址
        Private _key As String = ""             '交易安全校验码
        Private _input_charset As String = ""   '编码格式
        Private _sign_type As String = ""       '签名方式
        Private mysign As String = ""           '签名结果
        Private sPara As String()                '需要签名的已经过滤后的参数数组

        '构造函数
        '从配置文件及入口文件中初始化变量
        '输入 String() inputPara 需要签名的参数数组
        '     String   key 安全检验码
        '     String input_charset 字符编码格式 目前支持 gbk 或 utf-8
        '     String sign_type 签名方式 不需修改
        Public Sub alipay_service(ByVal inputPara As String(), ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)
            gateway = "https://mapi.alipay.com/gateway.do?"
            _key = key.Trim()
            _input_charset = input_charset.Trim().ToLower()
            _sign_type = sign_type.Trim().ToUpper()

            Dim sParaTemp As String() = alipay_function.para_filter(inputPara)
            sPara = alipay_function.arg_sort(sParaTemp)     '得到从字母a到z排序后的签名参数数组
            '获得签名结果
            mysign = alipay_function.build_mysign(sPara, _key, _sign_type, _input_charset)
        End Sub

        '构造表单提交HTML
        '输出 表单提交HTML文本
        Public Function Build_Form() As String
            Dim sHtml As StringBuilder = New StringBuilder()

            'GET方式传递
            sHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" & gateway & "_input_charset=" & _input_charset & "' method='get'>")

            'POST方式传递（GET与POST二必选一）
            'sHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" & gateway & "_input_charset=" & _input_charset & "' method='post'>")

            Dim nCount As Integer = UBound(sPara)
            Dim i, pos, nLen As Integer
            Dim itemName, itemValue As String
            For i = 0 To nCount
                '把sArray的数组里的元素格式：变量名=值，分割开来
                pos = InStr(sPara(i), "=")           '获得=字符的位置
                nLen = Len(sPara(i))                '获得字符串长度
                itemName = Left(sPara(i), pos - 1)     '获得变量名
                itemValue = Right(sPara(i), nLen - pos) '获得变量的值

                sHtml = sHtml.Append("<input type='hidden' name='" & itemName & "' value='" & itemValue & "'/>")
            Next

            sHtml = sHtml.Append("<input type='hidden' name='sign' value='" & mysign & "'/>")
            sHtml = sHtml.Append("<input type='hidden' name='sign_type' value='" & _sign_type & "'/>")

            'submit按钮控件请不要含有name属性
            sHtml = sHtml.Append("<input type=""submit"" value=""支付宝确认付款""></form>")

            sHtml = sHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>")

            Build_Form = sHtml.ToString()
        End Function

    End Class
End Namespace