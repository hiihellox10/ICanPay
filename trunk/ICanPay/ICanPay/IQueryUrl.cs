using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    internal interface IQueryUrl
    {
        /// <summary>
        /// ������ѯ���ݵ�Url����
        /// </summary>
        string BuildQueryUrl();
    }
}
