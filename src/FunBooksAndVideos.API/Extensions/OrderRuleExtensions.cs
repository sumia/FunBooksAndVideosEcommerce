using FunBooksAndVideos.Application.PurchaseOrders.Factories;
using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;

namespace FunBooksAndVideos.API.Extensions
{
    public static class OrderRuleExtensions
    {
        public static void ConfigureOrderRulesFactory(this WebApplicationBuilder builder)
        {
            // Register all IOrderRule implementations dynamically
            var assembly = typeof(IOrderRuleFactory).Assembly;
            var ruleTypes = assembly
                .GetTypes()
                .Where(t => typeof(IOrderRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var ruleType in ruleTypes)
            {
                builder.Services.AddScoped(typeof(IOrderRule), ruleType);
            }

            builder.Services.AddScoped<IOrderRuleFactory, OrderRuleFactory>();
        }
    }
}
