using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    internal interface IQueryForm
    {
        /// <summary>
        /// 建立Form提交的订单查询HTML代码
        /// </summary>
        string BuildQueryForm();
    }
}
