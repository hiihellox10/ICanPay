using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// �̻�����
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
        /// �̻��ʺ�
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
                    throw new ArgumentNullException("UserName", "�̻��ʺ�û������");
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
                    throw new ArgumentNullException("UserName", "�̻��ʺŲ���Ϊ��");
                }
            }
        }

        /// <summary>
        /// �̻���Կ
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
                    throw new ArgumentNullException("Key", "�̻���Կû������");
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
                    throw new ArgumentNullException("Key", "�̻���Կ����Ϊ��");
                }
            }
        }

        /// <summary>
        /// ���ػط�֪ͨURL
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
                    throw new ArgumentNullException("NotifyUrl", "����֪ͨUrlû������");
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
                    throw new ArgumentNullException("NotifyUrl", "����֪ͨUrl����Ϊ��");
                }
            }
        }
    }
}
