using ICanPay;
using ICanPay.Providers;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class QueryPaymentController : Controller
    {
        public ActionResult Index()
        {
            QueryWeChatPayOrder();

            return View();
        }


        /// <summary>
        /// 查询微信的订单支付状态
        /// </summary>
        private void QueryWeChatPayOrder()
        {
            PaymentSetting querySetting = new PaymentSetting(GatewayType.WeChatPay);
            querySetting.Merchant = new WeChatPayMerchant()         // 微信支付需要额外的 AppId 参数，这里使用继承自 Merchant 的 WeChatPayMerchant。
            {
                AppId = "wx000000000000000",                        // 公众号APPID
                UserName = "000000000000000",                       // 微信支付商户号
                Key = "0000000000000000000000000000000000000000"    // API密钥
            };
            
            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            querySetting.Order = new Order
            {
                Id = "20",
                Amount = 0.01
            };

            if (querySetting.CanQueryNow && querySetting.QueryNow())
            {
                // 订单已支付
            }
        }


        /// <summary>
        /// 查询财付通的订单支付状态
        /// </summary>
        private void QueryTenpayOrder()
        {
            PaymentSetting querySetting = new PaymentSetting(GatewayType.Tenpay);
            querySetting.Merchant = new Merchant
            {
                UserName = "000000000000000",                       // 商户号
                Key = "0000000000000000000000000000000000000000"    // 密钥
            };
            
            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            querySetting.Order = new Order
            {
                Id = "885",
                Amount = 0.01
            };

            if (querySetting.CanQueryNow && querySetting.QueryNow())
            {
                // 订单已支付
            }
        }


        /// <summary>
        /// 查询易宝的订单支付状态
        /// </summary>
        private void QueryYeepayOrder()
        {
            PaymentSetting querySetting = new PaymentSetting(GatewayType.Yeepay);
            querySetting.Merchant = new Merchant
            {
                UserName = "000000000000000",                         // 商户编号
                Key = "0000000000000000000000000000000000000000"      // 商户密钥
            };

            // 查询时需要设置订单的Id与金额，在查询结果中将会核对订单的Id与金额，如果不相符会返回查询失败。
            querySetting.Order = new Order
            {
                Id = "15",
                Amount = 0.01
            };

            if (querySetting.CanQueryNow && querySetting.QueryNow())
            {
                // 订单已支付
            }
        }
    }
}