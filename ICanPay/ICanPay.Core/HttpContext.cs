
namespace ICanPay
{
    public static class HttpContext
    {

#if NET35

        public static System.Web.HttpContext Current => System.Web.HttpContext.Current;

#elif NETSTANDARD2_0

        private static Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => HttpContextAccessor.HttpContext;

        internal static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

#endif
    }
}
