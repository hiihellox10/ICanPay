#if NETSTANDARD2_0
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
#endif

namespace ICanPay
{
#if NETSTANDARD2_0

    public static class ICanPayExtensions
    {
        public static void AddICanPay(this IServiceCollection services)
        {
            StaticHttpContextExtensions.AddStaticHttpContext(services);
        }

        public static IApplicationBuilder UseICanPay(this IApplicationBuilder app)
        {
            StaticHttpContextExtensions.UseStaticHttpContext(app);

            return app;
        }
    }

#endif
}
