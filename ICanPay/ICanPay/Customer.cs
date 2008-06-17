using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 支付客户资料
    /// </summary>
    public class Customer
    {
        string name;
        string address;
        string email;
        string post;
        string telephone;

        /// <summary>
        /// 姓名
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
        /// 通讯地址
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
        /// 邮编
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
        /// 联系电话
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
