
            ╭───────────────────────╮
  ╭────┤           支付宝代码示例结构说明             ├────╮
  │        ╰───────────────────────╯        │
　│                                                                  │
　│     接口名称：支付宝即时到帐接口（create_direct_pay_by_user）    │
　│　   代码版本：3.2                                                │
  │     开发语言：ASP.NET(c#)                                        │
  │     版    权：支付宝（中国）网络技术有限公司                     │
　│     制 作 者：支付宝商户事业部技术支持组                         │
  │     联系方式：商户服务电话0571-88158090                          │
  │                                                                  │
  ╰─────────────────────────────────╯

───────
 代码文件结构
───────

create_direct_pay_by_user_vs2005_utf8
  │
  ├app_code ┈┈┈┈┈┈┈┈┈┈类文件夹
  │  │
  │  ├AlipayConfig.cs┈┈┈┈┈基础配置类文件
  │  │
  │  ├AlipayCore.cs┈┈┈┈┈┈支付宝接口公用函数类文件
  │  │
  │  ├AlipayNotify.cs┈┈┈┈┈支付宝通知处理类文件
  │  │
  │  ├AlipayService.cs ┈┈┈┈支付宝各接口构造类文件
  │  │
  │  └AlipaySubmit.cs┈┈┈┈┈支付宝各接口请求提交类文件
  │
  ├log┈┈┈┈┈┈┈┈┈┈┈┈┈日志文件夹
  │
  ├default.aspx ┈┈┈┈┈┈┈┈支付宝接口入口文件
  ├default.aspx.cs┈┈┈┈┈┈┈支付宝接口入口文件
  │
  ├notify_url.aspx┈┈┈┈┈┈┈服务器异步通知页面文件
  ├notify_url.aspx.cs ┈┈┈┈┈服务器异步通知页面文件
  │
  ├return_url.aspx┈┈┈┈┈┈┈页面跳转同步通知文件
  ├return_url.aspx.cs ┈┈┈┈┈页面跳转同步通知文件
  │
  ├Web.Config ┈┈┈┈┈┈┈┈┈配置文件（集成时删除）
  │
  └readme.txt ┈┈┈┈┈┈┈┈┈使用说明文本

※注意※
需要配置的文件是：
alipay_config.cs、
default.aspx、
default.aspx.cs、
return_url.aspx、
return_url.aspx.cs、
notify_url.aspx、
notify_url.aspx.cs
统一命名空间为：namespace Com.Alipiay



─────────
 类文件函数结构
─────────

AlipayCore.cs

public static string BuildMysign(Dictionary<string, string> dicArray, string key, string sign_type, string _input_charset)
功能：生成签名结果
输入：Dictionary<string, string>  dicArray 要签名的数组
      string key 安全校验码
      string sign_type 签名类型
      string _input_charset 编码格式
输出：string 签名结果字符串

public static string CreateLinkstring(Dictionary<string, string> dicArray)
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
输入：Dictionary<string, string> dicArray 需要拼接的数组
输出：string 拼接完成以后的字符串

public static Dictionary<string, string> ParaFilter(SortedDictionary<string, string> dicArrayPre)
功能：除去数组中的空值和签名参数并以字母a到z的顺序排序
输入：SortedDictionary<string, string> dicArrayPre 过滤前的参数组
输出：Dictionary<string, string>  去掉空值与签名参数后的新签名参数组

public static string Sign(string prestr, string sign_type, string _input_charset)
功能：签名字符串
输入：string prestr 需要签名的字符串
      string sign_type 签名类型
      string _input_charset 编码格式
输出：string 签名结果

public static void LogResult(string sPath, string sWord)
功能：写日志，方便测试（看网站需求，也可以改成存入数据库）
输入：string sPath 日志的本地绝对路径
      string sWord 要写入日志里的文本内容

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

AlipayNotify.cs

public Notify()
功能：构造函数
      从配置文件中初始化变量
      
public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
功能：验证消息是否是支付宝发出的合法消息
输入：SortedDictionary<string, string> inputPara 通知返回参数数组
      string notify_id 通知验证ID
      string sign 支付宝生成的签名结果
输出：string 验证结果

private string GetPreSignStr(SortedDictionary<string, string> inputPara)
功能：获取待签名字符串（调试用）
输入：SortedDictionary<string, string> inputPara 通知返回参数数组
输出：string 待签名字符串

private string GetResponseMysign(SortedDictionary<string, string> inputPara)
功能：获取返回回来的待签名数组签名后结果
输入：SortedDictionary<string, string> inputPara 通知返回参数数组
输出：string 签名结果字符串

private string GetResponseTxt(string notify_id)
功能：获取是否是支付宝服务器发来的请求的验证结果
输入：string notify_id 通知验证ID
输出：string 验证结果

private string Get_Http(string strUrl, int timeout)
功能：获取远程服务器ATN结果
输入：string strUrl 指定URL路径地址
      int timeout 超时时间设置
输出：string 服务器ATN结果字符串

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

AlipaySubmit.cs

private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
功能：生成要请求给支付宝的参数数组
输入：SortedDictionary<string, string> sParaTemp 请求前的参数数组
输出：Dictionary<string, string> 要请求的参数数组

private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp)
功能：生成要请求给支付宝的参数数组
输入：SortedDictionary<string, string> sParaTemp 请求前的参数数组
输出：string 要请求的参数数组字符串

public static string BuildFormHtml(
		SortedDictionary<string, string> sParaTemp, 
		string gateway, 
		string strMethod, 
		string strButtonValue)
功能：构造提交表单HTML数据
输入：SortedDictionary<string, string> sParaTemp 请求参数数组
	  string gateway 网关地址
	  string strMethod 提交方式。两个值可选：post、get
	  string strButtonValue 确认按钮显示文字
输出：string 提交表单HTML文本

public static XmlDocument SendPostInfo(SortedDictionary<string, string> sParaTemp, string gateway)
功能：构造模拟远程HTTP的POST请求，获取支付宝的返回XML处理结果
输入：SortedDictionary<string, string> sParaTemp 请求参数数组
      string gateway 网关地址
输出：XmlDocument 支付宝返回XML处理结果

public static XmlDocument SendGetInfo(SortedDictionary<string, string> sParaTemp, string gateway)
功能：构造模拟远程HTTP的GET请求，获取支付宝的返回XML处理结果
输入：SortedDictionary<string, string> sParaTemp 请求参数数组
      string gateway 网关地址
输出：XmlDocument 支付宝返回XML处理结果


┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

AlipayService.cs

public string Create_direct_pay_by_user(SortedDictionary<string, string> sParaTemp)
功能：构造即时到帐接口
输入：SortedDictionary<string, string> sParaTemp 请求参数集合
输出：string 表单提交HTML信息

public static string Query_timestamp()
功能：用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
输出：string 时间戳字符串

public string AlipayInterface(SortedDictionary<string, string> sParaTemp)
功能：构造(支付宝接口名称)接口
输入：SortedDictionary<string, string> sParaTemp 请求参数集合
输出：string 表单提交HTML文本或者支付宝返回XML处理结果

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

return_url.aspx.cs

public SortedDictionary<string, string> GetRequestGet()
功能：获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
输出：SortedDictionary<string, string> request回来的信息组成的数组

┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

notify_url.aspx.cs

public SortedDictionary<string, string> GetRequestPost()
功能：获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
输出：SortedDictionary<string, string> request回来的信息组成的数组


──────────
 出现问题，求助方法
──────────

如果在集成支付宝接口时，有疑问或出现问题，可使用下面的链接，提交申请。
https://b.alipay.com/support/helperApply.htm?action=supportHome
我们会有专门的技术支持人员为您处理




