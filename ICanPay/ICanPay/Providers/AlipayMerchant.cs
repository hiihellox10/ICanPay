using System;
using System.Text.RegularExpressions;

namespace ICanPay.Providers
{
    /// <summary>
    /// 支付宝商户
    /// </summary>
    public class AlipayMerchant : Merchant
    {

        private const string EmailRegexString = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        private string _sellerEmail;


        /// <summary>
        /// 卖家支付宝邮箱
        /// </summary>
        public string SellerEmail
        {
            get
            {
                if (string.IsNullOrEmpty(_sellerEmail))
                {
                    throw new ArgumentNullException("SellerEmail", "卖家支付宝邮箱没有设置");
                }

                return _sellerEmail;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("SellerEmail", "卖家支付宝邮箱不能为空");
                }

                if (!ValidEmail(value))
                {
                    throw new ArgumentException("SellerEmail", "卖家支付宝邮箱地址格式不正确");
                }

                _sellerEmail = value;
            }
        }


        /// <summary>
        /// 是否是正确格式的Email地址
        /// </summary>
        /// <param name="emailAddress">Email地址</param>
        private bool ValidEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }

            return Regex.IsMatch(emailAddress, EmailRegexString);
        }

    }
}
