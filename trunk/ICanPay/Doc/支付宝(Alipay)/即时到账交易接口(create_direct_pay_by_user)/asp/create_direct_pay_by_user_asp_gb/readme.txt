
                ╭───────────────────────╮
  ╭──────┤           支付宝代码示例结构说明             ├──────╮
  │            ╰───────────────────────╯            │
　│                                                                          │
　│     接口名称：支付宝即时到帐接口（create_direct_pay_by_user）            │
　│　   代码版本：3.2                                                        │
  │     开发语言：ASP                                                        │
  │     版    权：支付宝（中国）网络技术有限公司                             │
　│     制 作 者：支付宝商户事业部技术支持组                                 │
  │     联系方式：商户服务电话0571-88158090                                  │
  │                                                                          │
  ╰─────────────────────────────────────╯


───────
 代码文件结构
───────

create_direct_pay_by_user_asp_gb
  │
  ├class┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈类文件夹
  │  │
  │  ├alipay_core.asp┈┈┈┈┈┈┈┈┈┈┈┈支付宝接口公用函数文件
  │  │
  │  ├alipay_md5.asp ┈┈┈┈┈┈┈┈┈┈┈┈MD5签名函数文件
  │  │
  │  ├alipay_notify.asp┈┈┈┈┈┈┈┈┈┈┈支付宝通知处理类文件
  │  │
  │  ├alipay_submit.asp┈┈┈┈┈┈┈┈┈┈┈支付宝各接口请求提交类文件
  │  │
  │  ├alipay_service.asp ┈┈┈┈┈┈┈┈┈┈支付宝各接口构造类文件
  │  │
  │  └alipay_config.asp┈┈┈┈┈┈┈┈┈┈┈基础配置文件
  │
  ├log┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈日志文件夹
  │
  ├alipayto.asp ┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈支付宝接口入口文件
  │
  ├index.asp┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈支付宝调试入口页面
  │
  ├notify_url.asp ┈┈┈┈┈┈┈┈┈┈┈┈┈┈服务器异步通知页面文件
  │
  ├return_url.asp ┈┈┈┈┈┈┈┈┈┈┈┈┈┈页面跳转同步通知文件
  │
  └readme.txt ┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈使用说明文本

※注意※
需要配置的文件是：alipay_config.asp、alipayto.asp、notify_url.asp、return_url.asp




─────────
 类文件函数结构
─────────

alipay_core.asp

Function BuildMysign(sPara, key, sign_type,input_charset)
功能：生成签名结果
输入：Array  sPara 要签名的数组
      String key 安全校验码
      String sign_type 签名类型
输出：String 签名结果字符串

Function CreateLinkstring(sPara)
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
输入：Array  sPara 需要拼接的数组
输出：String 拼接完成以后的字符串

Function CreateLinkstringUrlEncode(sPara)
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并且对其做URLENCODE编码
输入：Array  sPara 需要拼接的数组
输出：String 拼接完成以后的字符串

Function FilterPara(sPara)
功能：除去数组中的空值和签名参数
输入：Array  sPara 签名参数组
输出：Array  去掉空值与签名参数后的新签名参数组

Function SortPara(sPara)
功能：对数组排序
输入：Array  sPara 排序前的数组
输出：Array  排序后的数组

Function Sign(prestr,sign_type,input_charset)
功能：签名字符串
输入：String prestr 需要签名的字符串
      String sign_type 签名类型
      String input_charset 编码格式
输出：String 签名结果

Function LogResult(sWord)
功能：写日志，方便测试（看网站需求，也可以改成存入数据库）
输入：String sWord 要写入日志里的文本内容

Function GetDateTimeFormat()
功能：获取当前时间
格式：年[4位]-月[2位]-日[2位] 小时[2位 24小时制]:分[2位]:秒[2位]，如：2007-10-01 13:13:13
输出：String 时间格式化结果
说明：闲置

Function GetDateTime()
功能：获取当前时间
格式：年[4位]月[2位]日[2位]小时[2位 24小时制]分[2位]秒[2位]，如：20071001131313
输出：String 时间格式化结果

Function DelStr(Str)
功能：过滤特殊字符
输入：String Str 要被过滤的字符串
输出：String 已被过滤掉的新字符串

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

alipay_md5.asp

Public Function MD5(sMessage,input_charset)
功能：MD5签名
输入：String sMessage 要签名的字符串
      String input_charset 编码格式，utf-8、gbk
输出：String 签名结果

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

alipay_notify.asp

Public Function VerifyNotify()
功能：针对notify_url验证消息是否是支付宝发出的合法消息
输出：Bool  验证结果：true/false

Public Function VerifyReturn()
功能：针对return_url验证消息是否是支付宝发出的合法消息
输出：Bool  验证结果：true/false

Private Function GetMysign(sParaTemp)
功能：根据反馈回来的信息，生成签名结果
输入：Array sParaTemp 通知返回来的参数数组
输出：生成的签名结果

Private Function GetResponse(notify_id)
功能：获取远程服务器ATN结果
输入：string notify_id 通知校验ID
输出：服务器ATN结果字符串

Private Function GetRequestGet()
功能：获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
输出：Array  request回来的信息组成的数组

Private Function GetRequestPost()
功能：获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
输出：Array  request回来的信息组成的数组

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

alipay_submit.asp

Private Function BuildRequestPara(sParaTemp, key, sign_type, input_charset)
功能：生成要请求给支付宝的参数数组
输入：Array sParaTemp 请求前的参数数组
      string key 交易安全校验码
      string sign_type 签名类型
      string input_charset 编码格式
输出：要请求的参数数组

Private Function BuildRequestParaToString(sParaTemp, key, sign_type, input_charset)
功能：生成要请求给支付宝的参数数组
输入：Array sParaTemp 请求前的参数数组
      string key 交易安全校验码
      string sign_type 签名类型
      string input_charset 编码格式
输出：要请求的参数数组字符串

Public Function BuildFormHtml(sParaTemp, key, sign_type, input_charset, gateway, sMethod, sButtonValue)
功能：构造提交表单HTML数据
输入：Array sParaTemp 请求前的参数数组
      string key 交易安全校验码
      string sign_type 签名类型
      string input_charset 编码格式
      string gateway 网关地址
      string sMethod 提交方式。两个值可选：post、get
      string sButtonValue 确认按钮显示文字
输出：提交表单HTML文本

Public Function SendGetInfo(sParaTemp, key, sign_type, input_charset, gateway, sParaNode)
功能：构造模拟远程HTTP的GET请求，获取支付宝的返回XML处理结果
输入：Array sParaTemp 请求前的参数数组
      string key 交易安全校验码
      string sign_type 签名类型
      string input_charset 编码格式
      string gateway 网关地址
      Array sParaNode 要输出的XML节点名
输出：支付宝返回XML指定节点内容

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

alipay_service.asp

Public Function Create_direct_pay_by_user(sParaTemp)
功能：构造即时到帐接口
输入：Array sParaTemp 请求参数数组
输出：string 表单提交HTML信息

Public Function Query_timestamp()
功能：用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
输出：String 时间戳字符串

Public Function Alipay_interface(sParaTemp)
功能：构造(支付宝接口名称)接口
输入：Array sParaTemp 请求参数数组
输出：表单提交HTML文本或者支付宝返回XML处理结果


──────────
 出现问题，求助方法
──────────

如果在集成支付宝接口时，有疑问或出现问题，可使用下面的链接，提交申请。
https://b.alipay.com/support/helperApply.htm?action=supportHome
我们会有专门的技术支持人员为您处理




