using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    internal interface IQueryUrl
    {
        /// <summary>
        /// 订单查询数据的Url键接
        /// </summary>
        string BuildQueryUrl();
    }
}
