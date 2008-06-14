using System;
using System.Web.Security;

namespace net.pay.cncard.Security
{
	/// <summary>
	/// Summary description for cnSecurity.
	/// </summary>
	public class cnSecurity
	{
		public cnSecurity()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static string EncryptMD5(string Str)
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(Str ,"MD5").ToLower();
		}
	}
}
