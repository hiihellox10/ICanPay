Imports System.IO
Imports System.Net
Imports AlipayClass

'功能：设置商品有关信息（确认订单支付宝在线购买入口页）
'详细：该页面是接口入口页面，生成支付时的URL
'版本：3.1
'日期：2010-11-16
'说明：
'以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
'该代码仅供学习和研究支付宝接口使用，只是提供一个参考。

'''''''''''''''''注意'''''''''''''''''''''''''
'如果您在接口集成过程中遇到问题，
'您可以到商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决，
'您也可以到支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）寻找相关解决方案
'如果不想使用扩展功能请把扩展功能参数赋空值。

Partial Class alipayto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''''''''''''以下参数是需要设置的相关配置参数，设置后不会更改的'''''''''''''''''''''''''
        Dim con As alipay_config = New alipay_config()
        con.alipay_config()
        Dim partner As String = con.Partner
        Dim key As String = con.Key
        Dim seller_email As String = con.Seller_email
        Dim input_charset As String = con.Input_charset
        Dim notify_url As String = con.Notify_url
        Dim return_url As String = con.Return_url
        Dim show_url As String = con.Show_url
        Dim sign_type As String = con.Sign_type

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '''''''''''''''''''''''以下参数是需要通过下单时的订单数据传入进来获得''''''''''''''''''''''''''''''
        '必填参数
        Dim out_trade_no As String = DateTime.Now.ToString("yyyyMMddHHmmss")    '请与贵网站订单系统中的唯一订单号匹配
        Dim subject As String = Request.Form("aliorder")                        '订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
        Dim body As String = Request.Form("alibody")                            '订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
        Dim total_fee As String = Request.Form("alimoney")                      '订单总金额，显示在支付宝收银台里的“应付总额”里

        '扩展功能参数——网银提前
        Dim paymethod As String = ""                                            '默认支付方式，四个值可选：bankPay(网银); cartoon(卡通); directPay(余额); CASH(网点支付)，初始值
        Dim defaultbank As String = ""                                          '默认网银代号，代号列表见http://club.alipay.com/read.php?tid=8681379 初始值
        Dim pay_mode As String = Request.Form("pay_bank")
        If pay_mode = "directPay" Then
            paymethod = "directPay"
        Else
            paymethod = "bankPay"
            defaultbank = pay_mode
        End If


        '扩展功能参数——防钓鱼
        '请慎重选择是否开启防钓鱼功能
        'exter_invoke_ip、anti_phishing_key一旦被设置过，那么它们就会成为必填参数
        '建议使用POST方式请求数据
        Dim anti_phishing_key As String = ""                                    '防钓鱼时间戳
        Dim exter_invoke_ip As String = ""                                      '获取客户端的IP地址，建议：编写获取客户端IP地址的程序
        '如：
        'exter_invoke_ip = ""
        'anti_phishing_key = alipay_function.query_timestamp(partner)

        '扩展功能参数——其他
        Dim extra_common_param As String = ""                                   '自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
        Dim buyer_email As String = ""                                          '默认买家支付宝账号

        '扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)
        Dim royalty_type As String = ""                                         '提成类型，该值为固定值：10，不需要修改
        Dim royalty_parameters As String = ""
        '提成信息集，与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
        '各分润金额的总和须小于等于total_fee
        '提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
        '如：
        'royalty_type = "10"
        'royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二"

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '构造要请求的参数数组，无需改动
        Dim para As String()
        para = New String() {"service=create_direct_pay_by_user", "payment_type=1", "partner=" & partner, "seller_email=" & seller_email, "return_url=" & return_url, "notify_url=" & notify_url, "_input_charset=" & input_charset, "show_url=" & show_url, "out_trade_no=" & out_trade_no, "subject=" & subject, "body=" & body, "total_fee=" & total_fee, "paymethod=" & paymethod, "defaultbank=" & defaultbank, "anti_phishing_key=" & anti_phishing_key, "exter_invoke_ip=" & exter_invoke_ip, "extra_common_param=" & extra_common_param, "buyer_email=" & buyer_email, "royalty_type=" & royalty_type, "royalty_parameters=" & royalty_parameters}

        '构造请求函数
        Dim aliService As alipay_service = New alipay_service()
        aliService.alipay_service(para, key, input_charset, sign_type)
        Dim sHtmlText As String = aliService.Build_Form()

        '打印页面
        lbOut_trade_no.Text = out_trade_no
        lbTotal_fee.Text = total_fee
        lbButton.Text = sHtmlText
    End Sub
End Class
