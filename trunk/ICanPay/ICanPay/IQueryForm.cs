using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    internal interface IQueryForm
    {
        /// <summary>
        /// ����Form�ύ�Ķ�����ѯHTML����
        /// </summary>
        string BuildQueryForm();
    }
}
