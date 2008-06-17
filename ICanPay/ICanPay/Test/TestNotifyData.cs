using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay.Test
{
    /// <summary>
    /// 网关通知数据
    /// </summary>
    public class TestNotifyData
    {
        Dictionary<string, string> notifyData;
        DateTime notifyDateTime;
        string notifyIP;
        string notifyUrl;

        /// <remarks>初始化</remarks>
        public TestNotifyData()
        {
            notifyData = new Dictionary<string, string>();
        }


        /// <summary>
        /// Form、Url通知数据
        /// </summary>
        public Dictionary<string, string> NotifyData
        {
            get
            {
                return notifyData;
            }
            set
            {
                notifyData = value;
            }
        }


        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return notifyDateTime;
            }
            set
            {
                notifyDateTime = value;
            }
        }


        /// <summary>
        /// 通知IP
        /// </summary>
        public string IP
        {
            get
            {
                return notifyIP;
            }
            set
            {
                notifyIP = value;
            }
        }


        /// <summary>
        /// 通知Url
        /// </summary>
        public string Url
        {
            get
            {
                return notifyUrl;
            }
            set
            {
                notifyUrl = value;
            }
        }
    }
}
