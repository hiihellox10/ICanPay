using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICanPay
{
    /// <summary>
    /// ֧������ز���
    /// </summary>
    internal static class PayUtility
    {
        /// <summary>
        /// ����ַ�����MD5ֵ
        /// </summary>
        /// <param name="text">��Ҫ���MD5ֵ��֧����</param>
        public static string MD5(string text)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5");
        }


        /// <summary>
        /// �ַ����Ƿ�ȫ�����֣�ȫ��������ɷ���true
        /// </summary>
        /// <param name="text">��Ҫ��֤���ַ���</param>
        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, "^[0-9]*$");
        }


        /// <summary>
        /// ���ݲ���˳�򣬻�ȡ�����������е�ֵ
        /// </summary>
        /// <param name="parma">����˳��</param>
        /// <param name="otherData">��������</param>
        public static string GetOtherDataValue(string[] parma, Dictionary<string, string> otherData)
        {
            StringBuilder value = new StringBuilder();

            foreach (string s in parma)
            {
                if (otherData.ContainsKey(s))
                {
                    value.Append(otherData[s]);
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// ���������Ƿ���������е����м�
        /// </summary>
        /// <param name="parma">����</param>
        /// <param name="otherData">������</param>
        public static bool ContainsKey(string[] parma, Dictionary<string, string> otherData)
        {
            foreach (string s in parma)
            {
                if (!otherData.ContainsKey(s))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
