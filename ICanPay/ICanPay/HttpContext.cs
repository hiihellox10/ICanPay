using System;

namespace ICanPay
{
    public static class HttpContext
    {

#if NET35

        public static System.Web.HttpContext Current => System.Web.HttpContext.Current;

#endif

#if NETSTANDARD2_0
        private static IServiceProvider ServiceProvider;

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
#endif

    }
}
