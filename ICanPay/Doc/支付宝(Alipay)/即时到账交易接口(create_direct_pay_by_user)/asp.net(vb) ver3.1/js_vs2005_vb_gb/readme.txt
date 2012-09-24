            ╭───────────────────────╮
  ╭────┤           支付宝代码示例结构说明             ├────╮
  │        ╰───────────────────────╯        │
　│                                                                  │
　│     接口名称：支付宝即时到帐接口（create_direct_pay_by_user）    │
　│　   代码版本：3.1                                                │
  │     开发语言：ASP.NET(VB)                                        │
  │     版    权：支付宝（中国）网络技术有限公司                     │
　│     制 作 者：支付宝商户事业部技术支持组                         │
  │     联系方式：商户服务电话0571-88158090                          │
  │                                                                  │
  ╰─────────────────────────────────╯

───────
 代码文件结构
───────

js_vs2005_vb_gb
  │
  ├app_code ┈┈┈┈┈┈┈┈┈┈类文件夹
  │  │
  │  ├alipay_config.vb ┈┈┈┈基础信息配置属性类文件
  │  │
  │  ├alipay_function.vb ┈┈┈公用函数类文件
  │  │
  │  ├alipay_notify.vb ┈┈┈┈支付宝通知处理类文件
  │  │
  │  └alipay_service.vb┈┈┈┈支付宝请求处理类文件
  │
  ├images ┈┈┈┈┈┈┈┈┈┈┈图片、CSS样式文件夹
  │
  ├log┈┈┈┈┈┈┈┈┈┈┈┈┈日志文件夹
  │
  ├alipayto.aspx┈┈┈┈┈┈┈┈支付宝接口入口文件
  ├alipayto.aspx.vb ┈┈┈┈┈┈支付宝接口入口文件
  │
  ├default.aspx ┈┈┈┈┈┈┈┈快速付款入口模板文件
  ├default.aspx.vb┈┈┈┈┈┈┈快速付款入口模板文件
  │
  ├notify_url.aspx┈┈┈┈┈┈┈服务器异步通知页面文件
  ├notify_url.aspx.vb ┈┈┈┈┈服务器异步通知页面文件
  │
  ├return_url.aspx┈┈┈┈┈┈┈页面跳转同步通知文件
  ├return_url.aspx.vb ┈┈┈┈┈页面跳转同步通知文件
  │
  ├Web.Config ┈┈┈┈┈┈┈┈┈配置文件（集成时删除）
  │
  └readme.txt ┈┈┈┈┈┈┈┈┈使用说明文本

※注意※
需要配置的文件是：alipay_config.vb、alipayto.aspx、alipayto.aspx.vb
统一命名空间为：namespace AlipayClass

index.aspx仅是支付宝提供的付款入口模板文件，可选择使用。
如果商户网站根据业务需求不需要使用，请把alipayto.aspx作为与商户网站网站相衔接页面。
如果需要使用default.aspx，那么alipayto.aspx文件无需更改，只需配置好alipay_config.vb文件
拿到default.aspx页面在商户网站中的HTTP路径放置在商户网站中需要的位置，就能直接使用支付宝接口。



─────────
 类文件函数结构
─────────

alipay_function.vb

Public Shared Function build_mysign(ByVal sArray As String(), ByVal key As String, ByVal sign_type As String, ByVal input_charset As String) As String
功能：生成签名结果
输入：String() sArray 要签名的数组
      string   key 安全校验码
      string   sign_type 签名类型
      string   _input_charset 编码格式
输出：string   签名结果字符串

Public Shared Function create_linkstring(ByVal sArray As String()) As String
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
输入：String() sArray 需要拼接的数组
输出：String   拼接完成以后的字符串

Public Shared Function para_filter(ByVal sArray As String()) As String()
功能：除去数组中的空值和签名参数
输入：String() sArray 过滤前的参数组
输出：String() 去掉空值与签名参数后的新签名参数组

Public Shared Function arg_sort(ByVal sArray As String()) As String()
功能：对数组排序
输入：String() sArray 排序前的数组
输出：String() 排序后的数组

Public Shared Function Sign(ByVal prestr As String, ByVal sign_type As String, ByVal _input_charset As String) As String
功能：签名字符串
输入：String prestr 需要签名的字符串
      String sign_type 签名类型
      String _input_charset 编码格式
输出：String 签名结果

Public Shared Function query_timestamp(ByVal partner As String) As String
功能：用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
输入：String partner 合作身份者ID
输出：String 时间戳字符串

Public Shared Sub log_result(ByVal sPath As String, ByVal sWord As String)
功能：写日志，方便测试（看网站需求，也可以改成存入数据库）
输入：String sPath 日志的本地绝对路径
      String sWord 要写入日志里的文本内容

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

alipay_notify.vb

Public Sub alipay_notify(ByVal inputPara As String(), ByVal notify_id As String, ByVal partner As String, ByVal key As String, ByVal input_charset As String, ByVal sign_type As String, ByVal transport As String)
功能：构造函数
      从配置文件中初始化变量
输入：String() inputPara 通知返回来的参数数组
      String notify_id 验证通知ID
      String partner 合作身份者ID
      String key 安全校验码
      String input_charset 编码格式
      String sign_type 签名类型
      String transport 访问模式

Private Function Verify(ByVal notify_id As String) As String
功能：验证是否是支付宝服务器发来的请求
输入：String notify_id 验证通知ID
输出：String 验证结果

Function Get_Http(ByVal a_strUrl As String, ByVal timeout As Integer) As String
功能：获取远程服务器ATN结果
输入：String strUrl 指定URL路径地址
      Integer timeout 超时时间设置
输出：String 服务器ATN结果字符串

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

alipay_service.vb

Public Sub alipay_service(ByVal inputPara As String(), ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)
功能：构造函数
      从配置文件及入口文件中初始化变量
输入：String() inputPara 需要签名的参数数组
      String key 安全检验码
      String input_charset 字符编码格式 目前支持 gbk 或 utf-8
      String sign_type 签名方式 不需修改

Public Function Build_Form() As String
功能：构造表单提交HTML
输出：String 表单提交HTML文本

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

return_url.aspx.vb

Private Function GetRequestGet() As String()
功能：获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
输出：String() request回来的信息组成的数组

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

notify_url.aspx.vb

Private Function GetRequestPost() As String()
功能：获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
输出：String() request回来的信息组成的数组

──────────
 出现问题，求助方法
──────────

如果在集成支付宝接口时，有疑问或出现问题，可使用下面的链接，提交申请。
https://b.alipay.com/support/helperApply.htm?action=supportHome
我们会有专门的技术支持人员为您处理




