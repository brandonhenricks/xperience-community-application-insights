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
        public static IServiceCollection AddXperienceApplicationInsights(this IServiceCollection services)
        {
            services.AddSingleton<ITelemetryInitializer, XperienceTelemetryInitializer>();

            return services;
        }
    }
}
