using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ICanPay.Providers;

namespace ICanPay
{
    /// <summary>
    /// ��������֪ͨ��������Ӧ��֧�����ص�ʵ��
    /// </summary>
    public static class PayGatewayFactory
    {
        /// <summary>
        /// ��ȡ���ص�֪ͨ����������Ӧ������ʵ��
        /// </summary>
        /// <returns>�ж�������𣬴�����Ӧ����ʵ�֡����û���յ���ʶ������֪ͨ����Null</returns>
        public static PaymentNotify GetGatewayNotify()
        {
            Dictionary<string, string> notifyData = ReadNotifyData();
            PaymentNotify notify = new PaymentNotify();

            ProcessNotify validate = new ProcessNotify(notifyData);
            notify.PayGateway = validate.GetGateway();

            if (notify.PayGateway != null)
            {
                notify.PayGateway.OtherData = notifyData;
            }

            return notify;
        }




        /// <summary>
        /// ��ȡ���ط��ص�����
        /// </summary>
        private static Dictionary<string, string> ReadNotifyData()
        {
            Dictionary<string, string> notifyData = new Dictionary<string, string>();
            System.Collections.Specialized.NameValueCollection coll;
            string[] keys;

            // ��ȡͨ��Get�����ֵ
            coll = HttpContext.Current.Request.QueryString;
            keys = coll.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                notifyData[keys[i]] = coll[keys[i]];
            }

            // ��ȡͨ��Post�����ֵ
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
