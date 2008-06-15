using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay.Test
{
    /// <summary>
    /// ����֪ͨ����
    /// </summary>
    public class TestNotifyData
    {
        Dictionary<string, string> notifyData;
        DateTime notifyDateTime;
        string notifyIP;
        string notifyUrl;

        /// <remarks>��ʼ��</remarks>
        public TestNotifyData()
        {
            notifyData = new Dictionary<string, string>();
        }


        /// <summary>
        /// Form��Url֪ͨ����
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
        /// ֪ͨʱ��
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
        /// ֪ͨIP
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
        /// ֪ͨUrl
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
