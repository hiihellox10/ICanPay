using System;

namespace ICanPay.Core
{
    /// <summary>
    /// �̻�����
    /// </summary>
    public class Merchant
    {

        #region ˽���ֶ�

        string userName;
        string key;
        Uri notifyUrl;

        #endregion


        #region ���캯��

        public Merchant()
        {
        }


        public Merchant(string userName, string key, Uri notifyUrl, GatewayType gatewayType)
        {
            this.userName = userName;
            this.key = key;
            this.notifyUrl = notifyUrl;
            GatewayType = gatewayType;
        }

        #endregion


        #region ����

        /// <summary>
        /// �̻��ʺ�
        /// </summary>
        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException("UserName", "�̻��ʺ�û������");
                }

                return userName;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("UserName", "�̻��ʺŲ���Ϊ��");
                }

                userName = value;
            }
        }


        /// <summary>
        /// �̻���Կ
        /// </summary>
        public string Key
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("Key", "�̻���Կû������");
                }

                return key;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Key", "�̻���Կ����Ϊ��");
                }

                key = value;
            }
        }


        /// <summary>
        /// ���ػط�֪ͨURL
        /// </summary>
        public Uri NotifyUrl
        {
            get
            {
                if (notifyUrl == null)
                {
                    throw new ArgumentNullException("NotifyUrl", "����֪ͨUrlû������");
                }

                return notifyUrl;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("NotifyUrl", "����֪ͨUrl����Ϊ��");
                }

                notifyUrl = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public GatewayType GatewayType { get; set; }

        #endregion

    }
}