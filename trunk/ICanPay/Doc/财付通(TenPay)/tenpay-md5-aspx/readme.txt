一:文件说明:
1:md5_pay.aspx 支付页面.
2:md5_query.aspx 查询页面
3:payresult.aspx 支付结果显示页面
4:queryresult.aspx 查询结果显示页面
5:Md5Pay.cs 各功能实现类

二:注意点.
1:因为让.net自动转化处理URL中的中文字符,所以请在web.config中加入以下配置.
  <globalization requestEncoding="gb2312" responseEncoding="gb2312" culture="zh-CN" fileEncoding="gb2312" />

2:注意最好别把商户KEY明文显示.

3:读取回跳地址的地方可以用配置或者是相对地址(但在发起请求时必须组合成全地址)来实现,这样方便自已网站的部署.
  另外,回跳地址推荐使用IP地址.
  