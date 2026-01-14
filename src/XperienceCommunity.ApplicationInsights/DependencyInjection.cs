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
        /// Adds the Xperience Application Insights Filter to the specified <see cref="Microsoft.AspNetCore.Mvc.Filters.FilterCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="Microsoft.AspNetCore.Mvc.Filters.FilterCollection"/> to add the filter to.</param>
        /// <example>
        /// <code>
        /// services.AddApplicationInsightsTelemetry();
        /// services.AddXperienceApplicationInsights();
        /// services.AddControllersWithViews(options =>
        /// {
        ///     options.Filters.AddXperienceApplicationInsights();
        /// });
        /// </code>
        /// </example>
        public static void AddXperienceApplicationInsights(this Microsoft.AspNetCore.Mvc.Filters.FilterCollection collection)
        {
            collection.Add(typeof(XperienceApplicationInsightsFilter));
        }
    }
}
