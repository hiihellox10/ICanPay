#if NETSTANDARD2_0
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
#endif

namespace ICanPay
{
#if NETSTANDARD2_0

    internal static class StaticHttpContextExtensions
    {
        public static void AddStaticHttpContext(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }

#endif
}
