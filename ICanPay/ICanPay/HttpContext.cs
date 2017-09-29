
namespace ICanPay
{
    public static class HttpContext
    {

#if NET35

        public static System.Web.HttpContext Current => System.Web.HttpContext.Current;

#elif NETSTANDARD2_0

        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _accessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;

        internal static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

#endif
    }
}
