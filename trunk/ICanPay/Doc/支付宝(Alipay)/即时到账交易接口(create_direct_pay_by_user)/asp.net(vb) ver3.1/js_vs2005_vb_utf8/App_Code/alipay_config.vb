Imports Microsoft.VisualBasic

Namespace AlipayClass
    '功能：设置帐户有关信息及返回路径（基础配置页面）
    '版本：3.1
    '日期：2010-11-16
    '说明：
    '以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    '该代码仅供学习和研究支付宝接口使用，只是提供一个参考。

    '提示：如何获取安全校验码和合作身份者ID
    '1.访问支付宝商户服务中心(b.alipay.com)，然后用您的签约支付宝账号登陆.
    '2.访问“技术服务”→“下载技术集成文档”（https://b.alipay.com/support/helperApply.htm?action=selfIntegration）
    '3.在“自助集成帮助”中，点击“合作者身份(Partner ID)查询”、“安全校验码(Key)查询”

    '安全校验码查看时，输入支付密码后，页面呈灰色的现象，怎么办？
    '解决方法：
    '1、检查浏览器配置，不让浏览器做弹框屏蔽设置
    '2、更换浏览器或电脑，重新登录查询。
    Public Class alipay_config
        '定义变量（不需修改）
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
        ''' 获取或设置合作者身份ID
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
        ''' 获取或设置交易安全校验码
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
        ''' 获取或设置签约或卖家支付宝账号
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
        ''' 获取或设置付款完成后跳转页面路径
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
        ''' 获取或设置服务器异步通知页面路径
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
        ''' 获取或设置字符编码格式
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
        ''' 获取或设置签名方式
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
        ''' 获取或设置网站商品的展示地址
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
        ''' 获取或设置收款方姓名
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
            '↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            '合作身份者ID，以2088开头由16位纯数字组成的字符串
            _partner = ""

            '交易安全检验码，由数字和字母组成的32位字符串
            _key = ""

            '签约支付宝账号或卖家支付宝帐户
            _seller_email = ""

            '付完款后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            'return_url的域名不能写成http://localhost/js_vs2005_vb_utf8/return_url.aspx ，否则会导致return_url执行无效
            _return_url = "http://www.xxx.com/js_vs2005_vb_utf8/return_url.aspx"

            '交易过程中服务器通知的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            _notify_url = "http://www.xxx.com/js_vs2005_vb_utf8/notify_url.aspx"

            '网站商品的展示地址，不允许加?id=123这类自定义参数
            _show_url = "http://www.xxx.com/myorder.aspx"

            '收款方名称，如：公司名称、网站名称、收款人姓名等
            _mainname = "收款方名称"

            '↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑


            '字符编码格式 目前支持 gbk 或 utf-8
            _input_charset = "utf-8"

            '签名方式 不需修改
            _sign_type = "MD5"

            '访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http
            _transport = "https"
        End Sub
    End Class
End Namespace