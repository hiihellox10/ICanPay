using System;

namespace com.yeepay
{
	/// <summary>
	/// MotoOrd ��ժҪ˵����
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
			// TODO: �ڴ˴���ӹ��캯���߼�
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
