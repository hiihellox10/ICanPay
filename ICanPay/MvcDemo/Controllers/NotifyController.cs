using ICanPay;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class NotifyController : Controller
    {

        private static List<Merchant> _merchantList;

        static NotifyController()
        {
            InitMerchantList();
        }

        private static void InitMerchantList()
        {
            Merchant alipayMerchant = new Merchant
            {
                GatewayType = GatewayType.Alipay,
                UserName = "000000000000000",                       // 合作伙伴身份（PID）
                Key = "000000000000000000000000000000000000000000"  // MD5密钥
            };

            Merchant weChatPaymentMerchant = new Merchant
            {
                GatewayType = GatewayType.WeChatPay,
                UserName = "000000000000000",                        // 微信支付商户号
                Key = "000000000000000000000000000000000000000000"   // API密钥
            };

            Merchant tenpayMerchant = new Merchant
            {
                GatewayType = GatewayType.Tenpay,
                UserName = "000000000000000",                       // 商户号
                Key = "000000000000000000000000000000000000000000"  // 密钥
            };

            Merchant yeepayMerchant = new Merchant
            {
                GatewayType = GatewayType.Yeepay,
                UserName = "000000000000000",                       // 商户编号
                Key = "000000000000000000000000000000000000000000"  // 商户密钥
            };

            _merchantList = new List<Merchant>
            {
                alipayMerchant,
                weChatPaymentMerchant,
                tenpayMerchant,
                yeepayMerchant
            };
        }


        public ActionResult Index()
        {
            // 订阅支付通知事件
            PaymentNotify notify = new PaymentNotify(_merchantList);
            notify.PaymentSucceed += notify_PaymentSucceed;
            notify.PaymentFailed += notify_PaymentFailed;
            notify.UnknownGateway += notify_UnknownGateway;

            // 接收并处理支付通知
            notify.Received();

            return View();
        }

        private void notify_PaymentSucceed(object sender, PaymentSucceedEventArgs e)
        {
            // 支付成功时的处理代码
            if (e.PaymentNotifyMethod == PaymentNotifyMethod.AutoReturn)
            {
                // 当前是用户的浏览器自动返回时显示支付成功页面
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
    }
}