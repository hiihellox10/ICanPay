using System;

namespace ICanPay
{
    /// <summary>
    /// 商户数据
    /// </summary>
    public class Merchant
    {

        #region 私有字段

        private string _userName;
        private string _key;
        private Uri _notifyUrl;

        #endregion


        #region 构造函数

        public Merchant()
        {
        }


        public Merchant(string userName, string key, Uri notifyUrl, GatewayType gatewayType)
        {
            UserName = userName;
            Key = key;
            NotifyUrl = notifyUrl;
            GatewayType = gatewayType;
        }

        #endregion


        #region 属性

        /// <summary>
        /// 商户帐号
        /// </summary>
        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_userName))
                {
                    throw new ArgumentNullException("UserName", "商户帐号没有设置");
                }

                return _userName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("UserName", "商户帐号不能为空");
                }

                _userName = value;
            }
        }


        /// <summary>
        /// 商户密钥
        /// </summary>
        public string Key
        {
            get
            {
                if (string.IsNullOrEmpty(_key))
                {
                    throw new ArgumentNullException("Key", "商户密钥没有设置");
                }

                return _key;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Key", "商户密钥不能为空");
                }

                _key = value;
            }
        }


        /// <summary>
        /// 网关回发通知URL
        /// </summary>
        public Uri NotifyUrl
        {
            get
            {
                if (_notifyUrl == null)
                {
                    throw new ArgumentNullException("NotifyUrl", "网关通知Url没有设置");
                }

                return _notifyUrl;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("NotifyUrl", "网关通知Url不能为空");
                }

                _notifyUrl = value;
            }
        }


        /// <summary>
        /// 网关类型
        /// </summary>
        public GatewayType GatewayType { get; set; }

        #endregion

    }
}