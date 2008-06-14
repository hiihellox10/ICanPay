using System;

namespace com.yeepay
{
	/// <summary>
	/// QueryOrd 的摘要说明。
	/// </summary>
	public class QueryOrdResult
	{
		public string ReturnCode;
		public string ReturnTrxId;
		public string ReturnAmt;
		public string ReturnPid;
		public string ReturnOrder;

        public string ReturnMP;
        public string ReturnPayStatus;
        public string ReturnRefundCount;
        public string ReturnRefundAmt;
        public string ReturnAllPara;

		public QueryOrdResult(	string returnCode,
								string returnTrxId,
								string returnAmt,
								string returnPid,
								string returnOrder,
                                
                                string returnMP,
            					string returnPayStatus,
            					string returnRefundCount,
            					string returnRefundAmt,
								string returnAllPara)
		{
			this.ReturnCode		   = returnCode;
			this.ReturnTrxId	   = returnTrxId;
			this.ReturnAmt		   = returnAmt;
			this.ReturnPid		   = returnPid;
            this.ReturnOrder       = returnOrder;

            this.ReturnMP          = returnMP;
            this.ReturnPayStatus   = returnPayStatus;
            this.ReturnRefundCount = returnRefundCount;
            this.ReturnRefundAmt   = returnRefundAmt;
			this.ReturnAllPara	   = returnAllPara;
		}
	}
}
