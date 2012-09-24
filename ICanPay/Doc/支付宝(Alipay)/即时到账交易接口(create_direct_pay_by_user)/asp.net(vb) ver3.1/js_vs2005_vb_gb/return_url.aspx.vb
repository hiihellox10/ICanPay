Imports System.IO
Imports System.Net
Imports AlipayClass

'功能：付完款后跳转的页面（页面跳转同步通知页面）
'版本：3.1
'日期：2010-11-16
'说明：
'以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
'该代码仅供学习和研究支付宝接口使用，只是提供一个参考。

''''''''页面功能说明''''''''''''''''
'该页面可在本机电脑测试
'该页面称作“页面跳转同步通知页面”，是由支付宝服务器同步调用，可当作是支付完成后的提示信息页，如“您的某某某订单，多少金额已支付成功”。
'可放入HTML等美化页面的代码和订单交易完成后的数据库更新程序代码
'该页面可以使用ASP开发工具调试，也可以使用写文本函数log_result进行调试，该函数已被默认关闭，见alipay_notify.asp中的函数return_verify
' TRADE_FINISHED(表示交易已经成功结束，并不能再对该交易做后续操作);
' TRADE_SUCCESS(表示交易已经成功结束，可以对该交易做后续操作，如：分润、退款等);
Partial Class return_url
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''''''''''''以下参数是需要设置的相关配置参数，设置后不会更改的'''''''''''''''''''''''''
        Dim con As alipay_config = New alipay_config()
        con.alipay_config()
        Dim partner As String = con.Partner
        Dim key As String = con.Key
        Dim input_charset As String = con.Input_charset
        Dim sign_type As String = con.Sign_type

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim sArray As String() = GetRequestGet()

        If sArray.Length > 0 Then   '判断是否有带返回参数
            Dim aliNotify As alipay_notify = New alipay_notify()
            aliNotify.alipay_notify(sArray, Request.QueryString("notify_id"), partner, key, input_charset, sign_type)
            Dim responseTxt As String = aliNotify.ResponseTxt   '获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            Dim sign As String = Request.QueryString("sign")    '获取支付宝反馈回来的sign结果
            Dim mysign As String = aliNotify.Mysign             '获取通知返回后计算后（验证）的签名结果

            '写日志记录（若要调试，请取消下面两行注释）
            'Dim sWord As String = "responseTxt=" & responseTxt & "\n return_url_log:sign=" & Request.QueryString("sign") & "&mysign=" & mysign & "\n return回来的参数：" & aliNotify.PreSignStr
            'alipay_function.log_result(Server.MapPath("log/" & DateTime.Now.ToString().Replace(":", "")) & ".txt", sWord)

            '判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
            'responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            'mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关

            If responseTxt = "true" And sign = mysign Then    '验证成功
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '请在这里加上商户的业务逻辑程序代码

                '——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                '获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                Dim trade_no As String = Request.QueryString("trade_no")          '支付宝交易号
                Dim order_no As String = Request.QueryString("out_trade_no")      '获取订单号
                Dim total_fee As String = Request.QueryString("total_fee")        '获取总金额
                Dim subject As String = Request.QueryString("subject")            '商品名称、订单名称
                Dim body As String = Request.QueryString("body")                  '商品描述、订单备注、描述
                Dim buyer_email As String = Request.QueryString("buyer_email")    '买家支付宝账号
                Dim trade_status As String = Request.QueryString("trade_status")  '交易状态

                '打印页面
                lbTrade_no.Text = trade_no
                lbOut_trade_no.Text = order_no
                lbTotal_fee.Text = total_fee
                lbSubject.Text = subject
                lbBody.Text = body
                lbBuyer_email.Text = buyer_email
                lbTrade_status.Text = trade_status
                lbVerify.Text = "验证成功"

                If Request.QueryString("trade_status") = "TRADE_FINISHED" Or Request.QueryString("trade_status") = "TRADE_SUCCESS" Then
                    '判断该笔订单是否在商户网站中已经做过处理
                    '如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    '如果有做过处理，不执行商户的业务程序
                Else
                    Response.Write("trade_status=" & Request.QueryString("trade_status"))
                End If
                '——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Else    '验证失败
                lbVerify.Text = "验证失败"
            End If
        Else
            lbVerify.Text = "无返回参数"
        End If
    End Sub

    '获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
    '输出 String() request回来的信息组成的数组
    Private Function GetRequestGet() As String()
        Dim sArray As String()
        Dim i As Integer
        Dim coll As NameValueCollection
        'Load Form variables into NameValueCollection variable.
        coll = Request.QueryString
        ' Get names of all forms into a string array
        Dim requestItem As String() = coll.AllKeys

        For i = 0 To requestItem.Length - 1
            ReDim Preserve sArray(i)
            sArray(i) = requestItem(i) & "=" & Request.QueryString(requestItem(i))
        Next

        GetRequestGet = sArray
    End Function
End Class
