using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// ֧���ͻ�����
    /// </summary>
    public class Customer
    {
        string name;
        string address;
        string email;
        string post;
        string telephone;

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// ͨѶ��ַ
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// �ʱ�
        /// </summary>
        public string Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
            }
        }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Telephone
        {
            get
            {
                return telephone;
            }
            set
            {
                telephone = value;
            }
        }
    }
}
