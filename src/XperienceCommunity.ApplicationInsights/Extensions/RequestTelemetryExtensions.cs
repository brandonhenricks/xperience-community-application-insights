using Kentico.Content.Web.Mvc;
using Microsoft.ApplicationInsights.DataContracts;

namespace XperienceCommunity.ApplicationInsights.Extensions
{
    public static class RequestTelemetryExtensions
    {
        /// <summary>
        /// Adds telemetry properties related to a routed web page to the specified <see cref="RequestTelemetry"/>.
        /// </summary>
        /// <param name="requestTelemetry">The <see cref="RequestTelemetry"/> to add properties to.</param>
        /// <param name="routedWebPage">The <see cref="RoutedWebPage"/> containing the information to be added as properties.</param>
        public static void AddRoutedWebPageTelemetry(this RequestTelemetry? requestTelemetry,
            RoutedWebPage? routedWebPage)
        {
            if (requestTelemetry is null)
            {
                return;
            }

            if (routedWebPage is null)
            {
                return;
            }

            requestTelemetry.Properties.Add(nameof(WebPageDataContext.WebPage.ContentTypeName), routedWebPage.ContentTypeName);
            requestTelemetry.Properties.Add(nameof(WebPageDataContext.WebPage.ContentTypeID), $"{routedWebPage.ContentTypeID}");
            requestTelemetry.Properties.Add(nameof(WebPageDataContext.WebPage.WebPageItemGUID), $"{routedWebPage.WebPageItemGUID}");
            requestTelemetry.Properties.Add(nameof(WebPageDataContext.WebPage.WebPageItemID), $"{routedWebPage.WebPageItemID}");
            requestTelemetry.Properties.Add(nameof(WebPageDataContext.WebPage.LanguageName), $"{routedWebPage.LanguageID}");
            requestTelemetry.Properties.Add(nameof(WebPageDataContext.WebPage.LanguageName), $"{routedWebPage.LanguageName}");
        }
    }
}
