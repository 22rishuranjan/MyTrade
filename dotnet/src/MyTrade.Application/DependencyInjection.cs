using MyTrade.Application.Common.Interfaces;
using MyTrade.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MyTrade.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(
            this IServiceCollection services)
        {
            // application-layer services 
            services.AddScoped<ITradeService, TradeService>();

            return services;
        }
    }
}
