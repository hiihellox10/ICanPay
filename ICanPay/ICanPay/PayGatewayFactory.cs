using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ICanPay.Providers;

namespace ICanPay
{
    /// <summary>
    /// 接受网关通知并创建相应的支付网关的实现
    /// </summary>
    public static class PayGatewayFactory
    {
        /// <summary>
        /// 获取网关的通知，并创建相应的网关实现。如果是无法识别的网关返回null。
        /// </summary>
        /// <returns>判断网关类别，创建相应网关实现。如果没有收到可识别网关通知返回Null</returns>
        public static PaymentNotify GetGatewayNotify()
        {
            Dictionary<string, string> notifyData = ReadNotifyData();
            PaymentNotify notify = new PaymentNotify();

            ProcessNotify validate = new ProcessNotify(notifyData);
            notify.PayGateway = validate.GetGateway();

            // 网关的通知无法识别。
            if (notify.PayGateway == null)
            {
                return null; 
            }

            notify.PayGateway.OtherData = notifyData;
            return notify;
        }




        /// <summary>
        /// 读取网关发回的数据
        /// </summary>
        private static Dictionary<string, string> ReadNotifyData()
        {
            Dictionary<string, string> notifyData = new Dictionary<string, string>();
            System.Collections.Specialized.NameValueCollection coll;
            string[] keys;

            // 读取通过Get传入的值
            coll = HttpContext.Current.Request.QueryString;
            keys = coll.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                notifyData[keys[i]] = coll[keys[i]];
            }

            // 读取通过Post传入的值
            coll = HttpContext.Current.Request.Form;
            keys = coll.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                notifyData[keys[i]] = coll[keys[i]];
            }

            return notifyData;
        }
    }
}
