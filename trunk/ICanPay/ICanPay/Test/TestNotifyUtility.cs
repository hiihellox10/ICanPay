using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ICanPay.Test
{
    /// <summary>
    /// 网关通知测试工具
    /// </summary>
    public static class TestNotifyUtility
    {
        /// <summary>
        /// 获取网关通知数据
        /// </summary>
        public static TestNotifyData GetNotify()
        {
            TestNotifyData notify = new TestNotifyData();
            notify.NotifyData = ReadNotifyData();
            notify.IP = HttpContext.Current.Request.UserHostAddress;
            notify.Url = HttpContext.Current.Request.RawUrl;
            notify.DateTime = DateTime.Now;

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
