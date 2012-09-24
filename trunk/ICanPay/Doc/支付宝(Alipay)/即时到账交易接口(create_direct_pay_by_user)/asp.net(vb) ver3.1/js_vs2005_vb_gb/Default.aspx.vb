Imports AlipayClass

'功能：快速付款入口模板页
'详细：该页面是针对不涉及到购物车流程、充值流程等业务流程，只需要实现买家能够快速付款给卖家的付款功能。
'版本：3.1
'日期：2010-11-16
'说明：
'以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
'该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected show_url As String = ""
    Protected mainname As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim alicon As alipay_config = New alipay_config()
        alicon.alipay_config()
        show_url = alicon.Show_url
        mainname = alicon.Mainname
    End Sub
End Class
