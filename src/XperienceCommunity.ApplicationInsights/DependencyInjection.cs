using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace XperienceCommunity.ApplicationInsights
{
    public static class DependencyInjection
    {

        /// <summary>
        /// Adds Xperience Application Insights to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the Xperience Application Insights to.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        /// <example>
        /// <code>
        /// services.AddApplicationInsightsTelemetry();
        /// services.AddXperienceApplicationInsights();
        /// </code>
        /// </example>
        public static IServiceCollection AddXperienceApplicationInsights(this IServiceCollection services)
        {
            services.AddSingleton<ITelemetryInitializer, XperienceTelemetryInitializer>();
            return services;
        }

        /// <summary>
        /// Adds the Xperience Application Insights Filter
        /// </summary>
        /// <param name="collection">The MVC <see cref="Microsoft.AspNetCore.Mvc.Filters.FilterCollection"/> to which the Xperience Application Insights filter is added.</param>
        public static void AddXperienceApplicationInsights(this Microsoft.AspNetCore.Mvc.Filters.FilterCollection collection)
        {
            collection.Add(typeof(XperienceApplicationInsightsFilter));
        }
    }
}
