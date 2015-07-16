# 如何使用 #

  * [介绍](http://code.google.com/p/icanpay/wiki/GettingStarted#介绍)
  * [创建订单](http://code.google.com/p/icanpay/wiki/GettingStarted#创建订单)
  * [接收支付网关的支付通知](http://code.google.com/p/icanpay/wiki/GettingStarted#接收支付网关的支付通知)
  * [查询订单](http://code.google.com/p/icanpay/wiki/GettingStarted#查询订单)
  * [设置额外的数据](http://code.google.com/p/icanpay/wiki/GettingStarted#设置额外的数据)
  * [订单Id的建议](http://code.google.com/p/icanpay/wiki/GettingStarted#订单Id的建议)


---


# 介绍 #
ICanPay仅支持人民币支付。为了能够支持多个支付网关，创建支付网关订单的页面跟接收支付网关支付通知的页面均使用GB2312编码。如果使用其他编码可能会造成异常。

如果你的程序需要使用其它编码而不能使用GB2312编码，你可以在程序里新建一个用于放置创建支付订单跟接受网关通知页面的新文件夹。然后将创建支付订单跟接受网关通知的页面放置在这个文件夹中，接着这个文件夹中增加一个Web.config文件，修改Web.config文件。在的configuration/system.web节点中增加下面的代码，这样就可以使得只有这个文件夹中的页面使用GB2312编码，而其他页面将使用其他的编码。
```
<globalization requestEncoding="gb2312" responseEncoding="gb2312" />
```


---


# 创建订单 #

下面将演示如使用创建一个支付订单。首先你需要新建一个名为Payment的Web窗体，并在Payment.aspx页面仅保留如下代码：
```
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Demo.Payment" %>
```
这样的目的是因为实现了IPaymentForm接口的支付网关在创建订单时会生成并输出完整的Form表单，如果当前页面包含其他内容会造成无法提交订单。

下面的代码将演示如何使用易宝、支付宝来创建订单

```
private void CreateYeepayOrder()
{
    PaymentSetting<YeepayGateway> setting = new PaymentSetting<YeepayGateway>();
    setting.Merchant.UserName = "0000000000";
    setting.Merchant.Key = "00000000000000000000000000";
    setting.Merchant.NotifyUrl = new Uri("http://yousite.com/Payment/PaymentNotify.aspx");
    setting.Order.Amount = 0.01;
    setting.Order.OrderId = "9";

    setting.Payment();
}


private void CreateAlipayOrder()
{
    PaymentSetting<AlipayGateway> setting = new PaymentSetting<AlipayGateway>();
    setting.SetGatewayParameterValue("seller_email", "name@address.com");
    setting.Merchant.UserName = "0000000000";
    setting.Merchant.Key = "00000000000000000000000000";
    setting.Merchant.NotifyUrl = new Uri("http://yousite.com/Payment/PaymentNotify.aspx");
    setting.Order.Amount = 0.01;
    setting.Order.OrderId = "10";

    setting.Payment();
}
```

在上面的代码我们使用了易宝YeepayGateway、支付宝AlipayGateway的支付网关来创建订单，在创建订单时需要设置商户的、订单、接收网关支付通知的Url这些必须的数据。在使用支付宝AlipayGateway时，需要注意这里需要再设置卖家支付宝账号的邮箱，在支付或查询时一些额外数据通过使用SetGatewayParameterValue方法来添加。

在完成必须的设置后，使用Payment方法将会在当前页面输出提交订单的Form或者Url，然后会将用户定向到相应的支付网站。


---

# 接收支付网关的支付通知 #

首先你新建之前创建订单中用来接收支付网关通知的名为ServerNotify的Web窗体，并在ServerNotify.aspx页面仅保留如下代码：
```
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerNotify.aspx.cs" Inherits="Demo.ServerNotify" %>
```
这样做的原因是因为接收支付网关通知的页面可能需要输出一些字符表示已接收到支付网关的通知，如果有其他页面内容会造成干扰。

在这个页面将接收并处理多个不同的网关返回的支付通知，代码如下：

```
protected void Page_Load(object sender, EventArgs e)
{
    Merchant alipayMerchant = new Merchant();
    alipayMerchant.GatewayType = GatewayType.Alipay;
    alipayMerchant.UserName = "0000000000";
    alipayMerchant.Key = "00000000000000000000000000";

    Merchant yeepayMerchant = new Merchant();
    yeepayMerchant.GatewayType = GatewayType.Yeepay;
    yeepayMerchant.UserName = "1000000000000";
    yeepayMerchant.Key = "00000000000000000000000000000000000000000000";

    List<Merchant> merchantList = new List<Merchant>();
    merchantList.Add(alipayMerchant);
    merchantList.Add(yeepayMerchant);

    PaymentNotify notify = new PaymentNotify(merchantList);
    notify.PaymentSucceed += new PaymentSucceedEventHandler(notify_PaymentSucceed);
    notify.PaymentFailed += new PaymentFailedEventHandler(notify_PaymentFailed);
    notify.UnknownGateway += new UnknownGatewayEventHandler(notify_UnknownGateway);

    notify.Received();
}


private void notify_PaymentSucceed(object sender, PaymentSucceedEventArgs e)
{
    // 支付成功时的处理代码
    if (e.PaymentNotifyMethod == PaymentNotifyMethod.AutoReturn)
    {
        // 用户通过浏览器自动返回时充值成功提示
    }
}


private void notify_PaymentFailed(object sender, PaymentFailedEventArgs e)
{
    // 支付失败时的处理代码
}


private void notify_UnknownGateway(object sender, UnknownGatewayEventArgs e)
{
    // 无法识别支付网关时的处理代码
}
```

首先创建支付网关的商户数据，然后将他们添加到集合。在创建PaymentNotify对象时将商户数据的集合传入构造函数中。接着PaymentNotify在支付成功、失败、无法识别支付网关时事件上注册回调方法，最后使用Received方法来接收支付网关返回的支付通知，它将完成支付通知的处理并引发相应的事件。

在回调函数中你可以通过e.PaymentNotifyMethod属性来获得支付通知是用户通过浏览器自动返回还是由网关服务器发送。如果是用户通过浏览器自动返回这时你可以在页面提示充值以便让用户获得更好的体验。


---


# 查询订单 #

**查询订单有2种不同的方式**

1、查询订单跟提交订单的使用类似。提交查询以后，网关服务器给指定页面发送支付通知，然后你使用处理网关服务器返回付款通知一样的方法来处理订单的查询。

下面的代码演示了提交一个查询
```
private void QueryChinabankOrder()
{
    PaymentSetting<ChinabankGateway> setting = new PaymentSetting<ChinabankGateway>();
    setting.Merchant.UserName = "10000000000";
    setting.Merchant.Key = "0000000000000000000000000000000000000000";
    setting.Merchant.NotifyUrl = new Uri("http://yousite.com/Payment/PaymentNotify.aspx");
    setting.Order.OrderId = "1564515";

    setting.Query();
}
```

不是所有支付网关都支持查询，你通过PaymentSetting.CanQuery属性可以知道当前支付网关是否支持查询。支持查询订单的网关都实现了IQueryForm或者IQueryUrl接口。


2、有的支付网关是通过向支付网关查询页面发送需要查询的订单数据后，查询页面将输出查询结果。使用查询方式的支付网关都实现了IQueryPayment接口。

实现了IQueryPayment接口的网关，将使用如下方式查询订单的状态
```
private void QueryYeepayOrder()
{
    PaymentSetting<YeepayGateway> setting = new PaymentSetting<YeepayGateway>();
    setting.Merchant.UserName = "10000000000";
    setting.Merchant.Key = "0000000000000000000000000000000000000000";
    setting.Order.OrderId = "1564515";
    setting.Order.Amount = 0.01;

    if (setting.Gateway.QueryPayment())
    {
        // 订单已支付时的处理代码
    }
}
```

---


# 设置额外的数据 #

一些支付网关可能在生成支付订单或查询时需要一些额外的数据，这些数据通过`PaymentSetting<T>.SetGatewayParameterValue`方法来设置。

下面演示目前以实现的支付网关中有2个需要设置额外数据的的地方。

1、支付宝在创建订单时需要设置seller\_email，也就是是卖家支付宝账号的邮箱。
```
PaymentSetting<AlipayGateway> setting = new PaymentSetting<AlipayGateway>();
setting.SetGatewayParameterValue("seller_email", "name@address.com");
```

2、财付通在查询订单时需要设置date，date是订单的交易日期，日期格式为yyyyMMdd。
```
PaymentSetting<TenpayGateway> setting = new PaymentSetting<TenpayGateway>();
setting.SetGatewayParameterValue("date", "20090101");
```


---


# 订单Id的建议 #

不同支付网关对订单Id有不同的限制。它可能被要求的只能是数字、数字英文跟一些指定字符组成，或者有长度限制，详细的说明请阅读相关支付网关的问题。

但共同点就是他们都可以使用数字作为订单Id，如果你需要同时使用多个支付网关，就应该考虑只使用数字作为订单Id。