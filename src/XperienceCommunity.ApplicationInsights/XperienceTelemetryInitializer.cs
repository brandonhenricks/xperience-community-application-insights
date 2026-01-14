using CMS.ContactManagement;
using CMS.Core;
using CMS.DataEngine;
using CMS.Websites.Routing;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace XperienceCommunity.ApplicationInsights
{
    public sealed class XperienceTelemetryInitializer : ITelemetryInitializer
    {
        private readonly ICurrentContactProvider _contactProvider;
        private readonly IWebFarmService _webFarmService;
        private readonly IWebsiteChannelContext _websiteChannelContext;

        public XperienceTelemetryInitializer(IWebsiteChannelContext websiteChannelContext,
            IWebFarmService webFarmService, ICurrentContactProvider contactProvider)
        {
            _websiteChannelContext = websiteChannelContext;
            _webFarmService = webFarmService;
            _contactProvider = contactProvider;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (!CMSApplication.ApplicationInitialized.HasValue)
            {
                return;
            }

            if (telemetry is not RequestTelemetry)
            {
                return;
            }

            telemetry.Context.GlobalProperties.TryAdd(nameof(IWebsiteChannelContext.WebsiteChannelName),
                _websiteChannelContext.WebsiteChannelName);
            telemetry.Context.GlobalProperties.TryAdd(nameof(IWebsiteChannelContext.WebsiteChannelID),
                $"{_websiteChannelContext.WebsiteChannelID}");
            telemetry.Context.GlobalProperties.TryAdd("WebFarmServerName", _webFarmService.ServerName);

            var contact = _contactProvider.GetCurrentContact();

            if (contact is not null)
            {
                telemetry.Context.GlobalProperties.TryAdd(nameof(ContactInfo.ContactID), $"{contact.ContactID}");
                telemetry.Context.GlobalProperties.TryAdd(nameof(ContactInfo.ContactGUID), $"{contact.ContactGUID}");
            }            
        }
    }
}
