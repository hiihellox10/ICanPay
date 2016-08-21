
            ╭───────────────────────╮
    ────┤           支付宝代码示例结构说明             ├────
            ╰───────────────────────╯ 
　                                                                  
　       接口名称：支付宝即时到账交易接口（create_direct_pay_by_user）
　 　    代码版本：3.4
         开发语言：ASP.NET(c#)
         版    权：支付宝（中国）网络技术有限公司
　       制 作 者：支付宝商户事业部技术支持组
         联系方式：商户服务电话0571-88158090

    ─────────────────────────────────

───────
 代码文件结构
───────

create_direct_pay_by_user-CSHARP-UTF-8
  │
  ├app_code ┈┈┈┈┈┈┈┈┈┈类文件夹
  │  │
  │  ├AlipayConfig.cs┈┈┈┈┈基础配置类文件
  │  │
  │  ├AlipayCore.cs┈┈┈┈┈┈支付宝接口公用函数类文件
  │  │
  │  ├AlipayNotify.cs┈┈┈┈┈支付宝通知处理类文件
  │  │
  │  ├AlipaySubmit.cs┈┈┈┈┈支付宝各接口请求提交类文件
  │  │
  │  └MD5.cs ┈┈┈┈┈┈┈┈┈MD5类库
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
AlipayConfig.cs

统一命名空间为：namespace Com.Alipiay

─────────
 类文件函数结构
─────────

AlipayCore.cs

public static Dictionary<string, string> ParaFilter(SortedDictionary<string, string> dicArrayPre)
功能：除去数组中的空值和签名参数并以字母a到z的顺序排序
输入：SortedDictionary<string, string> dicArrayPre 过滤前的参数组
输出：Dictionary<string, string>  去掉空值与签名参数后的新签名参数组

public static string CreateLinkString(Dictionary<string, string> dicArray)
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
输入：Dictionary<string, string> dicArray 需要拼接的数组
输出：string 拼接完成以后的字符串

public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
功能：把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
输入：Dictionary<string, string> dicArray 需要拼接的数组
      Encoding code 字符编码
输出：string 拼接完成以后的字符串

public static void log_result(string sPath, string sWord)
功能：写日志，方便测试（看网站需求，也可以改成存入数据库）
输入：string sPath 日志的本地绝对路径
      string sWord 要写入日志里的文本内容

public static string GetAbstractToMD5(Stream sFile)
功能：获取文件的md5摘要
输入：Stream sFile 文件流
输出：string MD5摘要结果

public static string GetAbstractToMD5(byte[] dataFile)
功能：获取文件的md5摘要
输入：byte[] dataFile 文件流
输出：string MD5摘要结果


┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉┉

MD5.cs

public static string Sign(string prestr, string key, string _input_charset)
功能：签名字符串
输入：string prestr 需要签名的字符串
      string key 密钥
      string _input_charset 编码格式
输出：string 签名结果

public static bool Verify(string prestr, string sign, string key, string _input_charset)
功能：验证签名
输入：string prestr 需要签名的字符串
      string sign 签名结果
      string key 密钥
      string _input_charset 编码格式
输出：string 验证结果


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
输出：bool 验证结果

private string GetPreSignStr(SortedDictionary<string, string> inputPara)
功能：获取待签名字符串（调试用）
输入：SortedDictionary<string, string> inputPara 通知返回参数数组
输出：string 待签名字符串

private bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign)
功能：获取返回回来的待签名数组签名后结果
输入：SortedDictionary<string, string> inputPara 通知返回参数数组
      string sign 支付宝生成的签名结果
输出：bool 签名验证结果

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

private static string BuildRequestMysign(Dictionary<string, string> sPara)
功能：生成签名结果
输入：Dictionary<string, string>  sPara 要签名的数组
输出：string 签名结果字符串

private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
功能：生成要请求给支付宝的参数数组
输入：SortedDictionary<string, string> sParaTemp 请求前的参数数组
输出：Dictionary<string, string> 要请求的参数数组

private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
功能：生成要请求给支付宝的参数数组
输入：SortedDictionary<string, string> sParaTemp 请求前的参数数组
      Encoding code 字符编码
输出：string 要请求的参数数组字符串

public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
功能：建立请求，以表单HTML形式构造（默认）
输入：SortedDictionary<string, string> sParaTemp 请求参数数组
	  string strMethod 提交方式。两个值可选：post、get
	  string strButtonValue 确认按钮显示文字
输出：string 提交表单HTML文本

public static string Query_timestamp()
功能：用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
输出：string 时间戳字符串


──────────
 出现问题，求助方法
──────────

如果在集成支付宝接口时，有疑问或出现问题，可使用下面的链接，提交申请。
https://support.open.alipay.com/alipay/support/index.htm
我们会有专门的技术支持人员为您处理




