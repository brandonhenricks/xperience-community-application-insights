using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc.Filters;
using XperienceCommunity.ApplicationInsights.Extensions;

namespace XperienceCommunity.ApplicationInsights
{
    public sealed class XperienceApplicationInsightsFilter : IActionFilter
    {
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly IWebPageDataContextRetriever _webPageDataContextRetriever;

        public XperienceApplicationInsightsFilter(IWebsiteChannelContext websiteChannelContext, IWebPageDataContextRetriever webPageDataContextRetriever)
        {
            _websiteChannelContext = websiteChannelContext;
            _webPageDataContextRetriever = webPageDataContextRetriever;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var requestTelemetry = context.HttpContext.Features.Get<RequestTelemetry>();

            if (requestTelemetry is null)
            {
                return;
            }

            requestTelemetry.Properties.Add(nameof(IWebsiteChannelContext.WebsiteChannelID), $"{_websiteChannelContext.WebsiteChannelID}");
            requestTelemetry.Properties.Add(nameof(IWebsiteChannelContext.WebsiteChannelName), _websiteChannelContext.WebsiteChannelName);
            requestTelemetry.Properties.Add(nameof(IWebsiteChannelContext.IsPreview), $"{_websiteChannelContext.IsPreview}");

            if (_webPageDataContextRetriever.TryRetrieve(out var page))
            {
                requestTelemetry.AddRoutedWebPageTelemetry(page.WebPage);
            }
        }
    }
}
