using ICanPay;
using ICanPay.Providers;
using System;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            CreateAlipayOrder();

            return View();
        }


        /// <summary>
        /// 创建支付宝的支付订单
        /// </summary>
        private void CreateAlipayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Alipay);
            paymentSetting.Merchant = new AlipayMerchant                    // 支付宝需要额外的 SellerEmail 参数，这里需要使用继承自 Merchant 的 AlipayMerchant。
            {
                SellerEmail = "yourname@address.com",                       // 支付宝的注册邮箱
                UserName = "000000000000000",                               // 合作伙伴身份（PID）
                Key = "000000000000000000000000000000000000000000",         // MD5密钥
                NotifyUrl = new Uri("http://yourwebsite.com/Notify")
            };

            paymentSetting.Order = new Order
            {
                Amount = 0.01,
                Id = "35",
                Subject = "测试支付宝"
            };

            paymentSetting.Payment();
        }


        /// <summary>
        /// 创建微信的支付订单
        /// </summary>
        private void CreateWeChatPayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.WeChatPay);
            paymentSetting.Merchant = new WeChatPayMerchant                   // 微信支付需要额外的 AppId 参数，这里需要使用继承自 Merchant 的 WeChatPayMerchant。
            {
                AppId = "wx000000000000000",                                  // 公众号APPID
                UserName = "000000000000000",                                 // 微信支付商户号
                Key = "000000000000000000000000000000000000000000",           // API密钥
                NotifyUrl = new Uri("http://yourwebsite.com/Notify")
            };

            paymentSetting.Order = new Order
            {
                Amount = 0.01,
                Id = "31",
                Subject = "测试微信"
            };

            paymentSetting.Payment();
        }


        /// <summary>
        /// 创建财付通的支付订单
        /// </summary>
        private void CreateTenpayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Tenpay);
            paymentSetting.Merchant = new Merchant
            {
                UserName = "000000000000000",                               // 商户号
                Key = "000000000000000000000000000000000000000000",         // 密钥
                NotifyUrl = new Uri("http://yourwebsite.com/Notify")
            };

            paymentSetting.Order = new Order
            {
                Amount = 0.01,
                Id = "93",
                Subject = "测试财付通",
            };

            paymentSetting.Payment();
        }


        /// <summary>
        /// 创建易宝的支付订单
        /// </summary>
        private void CreateYeepayOrder()
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Yeepay);
            paymentSetting.Merchant = new Merchant
            {
                UserName = "000000000000000",                               // 商户编号
                Key = "000000000000000000000000000000000000000000",         // 商户密钥
                NotifyUrl = new Uri("http://yourwebsite.com/Notify")
            };

            paymentSetting.Order = new Order
            {
                Amount = 0.01,
                Id = "24",
                Subject = "测试易宝"
            };

            paymentSetting.Payment();
        }
    }
}