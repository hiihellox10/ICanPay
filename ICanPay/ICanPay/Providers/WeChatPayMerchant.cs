using System;

namespace ICanPay.Providers
{
    /// <summary>
    /// 微信支付商户
    /// </summary>
    public class WeChatPayMerchant : Merchant
    {

        private string _appId;


        /// <summary>
        /// 公众号 APP Id
        /// </summary>
        public string AppId
        {
            get
            {
                if (string.IsNullOrEmpty(_appId))
                {
                    throw new ArgumentNullException("AppId", "公众号 App Id 没有设置");
                }

                return _appId;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("AppId", "公众号 App Id 不能为空");
                }

                _appId = value;
            }
        }

    }
}
