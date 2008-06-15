using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 商户数据
    /// </summary>
    public class Merchant
    {
        string userName;
        string key;
        string notifyUrl;


        public Merchant()
        {
        }
        /// <summary>
        /// 商户帐号
        /// </summary>
        public string UserName
        {
            get
            {
                if (userName != null)
                {
                    return userName;
                }
                else
                {
                    throw new ArgumentNullException("UserName", "商户帐号没有设置");
                }
            }
            set
            {
                if (value != null)
                {
                    userName = value;
                }
                else
                {
                    throw new ArgumentNullException("UserName", "商户帐号不能为空");
                }
            }
        }

        /// <summary>
        /// 商户密钥
        /// </summary>
        public string Key
        {
            get
            {
                if (key != null)
                {
                    return key;
                }
                else
                {
                    throw new ArgumentNullException("Key", "商户密钥没有设置");
                }
            }
            set
            {
                if (value != null)
                {
                    key = value;
                }
                else
                {
                    throw new ArgumentNullException("Key", "商户密钥不能为空");
                }
            }
        }

        /// <summary>
        /// 网关回发通知URL
        /// </summary>
        public string NotifyUrl
        {
            get
            {
                if (notifyUrl != null)
                {
                    return notifyUrl;
                }
                else
                {
                    throw new ArgumentNullException("NotifyUrl", "网关通知Url没有设置");
                }
            }
            set
            {
                if (value != null)
                {
                    notifyUrl = value;
                }
                else
                {
                    throw new ArgumentNullException("NotifyUrl", "网关通知Url不能为空");
                }
            }
        }
    }
}
