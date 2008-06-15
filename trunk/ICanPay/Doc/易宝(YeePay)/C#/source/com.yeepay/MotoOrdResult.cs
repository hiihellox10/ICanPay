using System;

namespace com.yeepay
{
	/// <summary>
	/// MotoOrd 的摘要说明。
	/// </summary>
	public class MotoOrdResult
	{
		public string ReturnCode;
		public string ReturnAmt;
		public string ReturnPid;
		public string ReturnOrder;
		public string ReturnMotoId;

		public string ReturnAllPara;

		public MotoOrdResult(	string returnCode,
								string returnAmt,
								string returnPid,
								string returnOrder,
								string returnMotoId,

								string returnAllPara)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.ReturnCode		= returnCode;
			this.ReturnAmt		= returnAmt;
			this.ReturnPid		= returnPid;
			this.ReturnOrder	= returnOrder;
			this.ReturnMotoId	= returnMotoId;

			this.ReturnAllPara	= returnAllPara;
	
		}
	}
}
