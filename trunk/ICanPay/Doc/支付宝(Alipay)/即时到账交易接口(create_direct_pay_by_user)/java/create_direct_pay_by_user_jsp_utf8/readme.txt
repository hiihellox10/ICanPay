
            ╭───────────────────────╮
  ╭────┤           支付宝代码示例结构说明             ├────╮
  │        ╰───────────────────────╯        │
　│                                                                  │
　│     接口名称：支付宝即时到帐接口（create_direct_pay_by_user）    │
　│　   代码版本：3.2                                                │
  │     开发语言：JAVA                                               │
  │     版    权：支付宝（中国）网络技术有限公司                     │
　│     制 作 者：支付宝商户事业部技术支持组                         │
  │     联系方式：商户服务电话0571-88158090                          │
  │                                                                  │
  ╰─────────────────────────────────╯

───────
 代码文件结构
───────

create_direct_pay_by_user_jsp_utf8
  │
  ├src┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈类文件夹
  │  │
  │  ├com.alipay.config
  │  │  │
  │  │  └AlipayConfig.java┈┈┈┈┈基础配置类文件
  │  │
  │  ├com.alipay.util
  │  │  │
  │  │  └AlipayService.java ┈┈┈┈支付宝各接口构造类文件
  │  │
  │  ├com.alipay.util
  │  │  │
  │  │  ├AlipayCore.java┈┈┈┈┈┈支付宝接口公用函数类文件
  │  │  │
  │  │  ├AlipayNotify.java┈┈┈┈┈支付宝通知处理类文件
  │  │  │
  │  │  ├AlipaySubmit.java┈┈┈┈┈支付宝各接口请求提交类文件
  │  │  │
  │  │  ├AlipayMd5Encrypt.java┈┈┈支付宝MD5签名类文件
  │  │  │
  │  │  └UtilDate.java┈┈┈┈┈┈┈支付宝自定义订单类文件
  │  │
  │  └com.alipay.util.httpClient（已封装）
  │      │
  │      ├HttpProtocolHandler.java ┈支付宝HttpClient处理类文件
  │      │
  │      ├HttpRequest.java ┈┈┈┈┈支付宝HttpClient请求类文件
  │      │
  │      ├HttpResponse.java┈┈┈┈┈支付宝HttpClient返回类文件
  │      │
  │      └HttpResultType.java┈┈┈┈支付宝HttpClient返回的结果字符方式类文件
  │
  ├WebRoot┈┈┈┈┈┈┈┈┈┈┈┈┈┈页面文件夹
  │  │
  │  ├alipayto.jsp ┈┈┈┈┈┈┈┈┈支付宝接口入口文件
  │  │
  │  ├index.jsp┈┈┈┈┈┈┈┈┈┈┈支付宝调试入口页面
  │  │
  │  ├notify_url.jsp ┈┈┈┈┈┈┈┈服务器异步通知页面文件
  │  │
  │  ├return_url.jsp ┈┈┈┈┈┈┈┈页面跳转同步通知文件
  │  │
  │  └WEB-INF
  │   	  │
  │      └lib（如果JAVA项目中包含这些架包，则不需要导入）
  │   	     │
  │   	     ├commons-codec-1.3.jar
  │   	     │
  │   	     ├commons-httpclient-3.0.1.jar
  │   	     │
  │   	     ├commons-logging-1.1.1.jar
  │   	     │
  │   	     ├dom4j-1.6.1.jar
  │   	     │
  │   	     └jaxen-1.1-beta-6.jar
  │
  └readme.txt ┈┈┈┈┈┈┈┈┈使用说明文本

※注意※
需要配置的文件是：
alipay_config.jsp、
alipayto.jsp、
return_url.jsp、
notify_url.jsp、
引用包：com.alipay.config.*、com.alipay.util.*、com.alipay.util.services

本代码示例（demo）中模拟获取远程HTTP信息使用的是commons-httpclient-3.0版本的第三方架包、及支付宝的httpClient封装类。
如果您不想使用该方式实现模拟获取远程HTTP功能，那么可以用其他方式代替，此时需您自行编写代码。





─────────
 类文件函数结构
─────────

AlipayCore.java

public static String buildMysign(Map<String, String> sArray)
功能：生成签名结果
输入：Map<String, String> sArray 要签名的数组
输出：String 签名结果字符串

public static Map paraFilter(Map<String, String> sArray)
功能：除去数组中的空值和签名参数
输入：Map<String, String> sArray 要签名的数组
输出：Map<String, String> 去掉空值与签名参数后的新签名参数组

public static String createLinkString(Map<String, String> params)
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
输入：Map<String, String> params 需要拼接的数组
输出：String 拼接完成以后的字符串

public static void logResult(String sWord)
功能：写日志，方便测试（看网站需求，也可以改成存入数据库）
输入：String sWord 要写入日志里的文本内容

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

AlipayMd5Encrypt.java

public static String md5(String text)
功能：对字符串进行MD5签名
输入：String text 明文
输出：String 签名结果

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉


AlipayNotify.java

public static boolean verify(Map<String, String> params)
功能：根据反馈回来的信息，生成签名结果
输入：Map<String, String>  Params 通知返回来的参数数组
输出：boolean 验证结果

private static String getMysign(Map<String, String> Params)
功能：根据反馈回来的信息，生成签名结果
输入：Map<String, String>  Params 通知返回来的参数数组
输出：String 签名结果

private static String verifyResponse(String notify_id)
功能：获取远程服务器ATN结果,验证返回URL
输入：String notify_id 验证通知ID
输出：String 验证结果

private static String checkUrl(String urlvalue)
功能：获取远程服务器ATN结果
输入：String urlvalue 指定URL路径地址
输出：String 服务器ATN结果字符串

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

AlipaySubmit.java

private static Map<String, String> buildRequestPara(Map<String, String> sParaTemp)
功能：生成要请求给支付宝的参数数组
输入：Map<String, String> sParaTemp
输出：Map<String, String> 要请求的参数数组

public static String buildForm(
	Map<String, String> sParaTemp, 
	String gateway, 
	String strMethod,
	String strButtonName)
功能：构造提交表单HTML数据
输入：Map<String, String> sParaTemp 请求参数数组
      String gateway 网关地址
      String strMethod 提交方式。两个值可选：post、get
      String strButtonName 确认按钮显示文字
输出：String 提交表单HTML文本

private static NameValuePair[] generatNameValuePair(Map<String, String> properties)
功能：MAP类型数组转换成NameValuePair类型
输入：Map<String, String> sParaTemp MAP类型数组
输出：NameValuePair[] NameValuePair类型数组

public static String sendPostInfo(Map<String, String> sParaTemp, String gateway)throws Exception
功能：构造模拟远程HTTP的POST请求，获取支付宝的返回XML处理结果
输入：Map<String, String> sParaTemp 请求参数数组
      String gateway 网关地址
输出：String 支付宝返回XML处理结果


┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

AlipayService.java

public static String create_direct_pay_by_user(Map<String, String> sParaTemp)
功能：构造即时到帐接口
输入：Map<String, String> sParaTemp 请求参数集合
输出：string 表单提交HTML信息

public static String query_timestamp()
功能：用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
输出：String 时间戳字符串

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

UtilDate.java

public  static String getOrderNum()
功能：自动生出订单号，格式yyyyMMddHHmmss
输出：String 订单号

public  static String getDateFormatter()
功能：获取日期，格式：yyyy-MM-dd HH:mm:ss
输出：String 日期

public static String getDate()
功能：获取日期，格式：yyyyMMdd
输出：String 日期

public static String getThree()
功能：产生随机的三位数
输出：String 随机三位数

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉


──────────
 出现问题，求助方法
──────────

如果在集成支付宝接口时，有疑问或出现问题，可使用下面的链接，提交申请。
https://b.alipay.com/support/helperApply.htm?action=supportHome
我们会有专门的技术支持人员为您处理




