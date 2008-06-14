using System;
using System.Text;
using System.Net;
using System.IO;

namespace com.yeepay
{
	/// <summary>
	/// HttpUtils ��ժҪ˵����
	/// </summary>
	public class HttpUtils
	{
		public HttpUtils()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		public string SendRequest(string strUrl,string strPara)
		{
			string strQuery=strUrl + "?" + strPara;

			try
			{
				WebRequest wrq=WebRequest.Create(strQuery);
                wrq.Method = "GET";

				WebResponse wrp=wrq.GetResponse();

				StreamReader sr = new StreamReader(wrp.GetResponseStream(),Encoding.GetEncoding("gb2312"));

				string url="";
				string line;
				while((line=sr.ReadLine())!=null)
				{
					url += "&" + line;
				}
				return url;
			}

			catch(Exception ex)
			{
				return ex.ToString();
			}
		}
	}
}