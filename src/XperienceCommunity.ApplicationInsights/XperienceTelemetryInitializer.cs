using CMS.ContactManagement;
using CMS.Core;
using CMS.Websites.Routing;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace XperienceCommunity.ApplicationInsights
{
    public sealed class XperienceTelemetryInitializer: ITelemetryInitializer
    {
        private readonly IWebsiteChannelContext _websiteChannelContext;
        private readonly IWebFarmService _webFarmService;
        private readonly ICurrentContactProvider _contactProvider;

        public XperienceTelemetryInitializer(IWebsiteChannelContext websiteChannelContext, IWebFarmService webFarmService, ICurrentContactProvider contactProvider)
        {
            _websiteChannelContext = websiteChannelContext;
            _webFarmService = webFarmService;
            _contactProvider = contactProvider;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is RequestTelemetry)
            {
                telemetry.Context.GlobalProperties.Add(nameof(IWebsiteChannelContext.WebsiteChannelName),
                    _websiteChannelContext.WebsiteChannelName);
                telemetry.Context.GlobalProperties.Add(nameof(IWebsiteChannelContext.WebsiteChannelID),
                    $"{_websiteChannelContext.WebsiteChannelID}");
                telemetry.Context.GlobalProperties.Add("WebFarmServerName", _webFarmService.ServerName);

                var contact = _contactProvider.GetCurrentContact();

                if (contact != null)
                {
                    telemetry.Context.GlobalProperties.Add(nameof(ContactInfo.ContactID), $"{contact.ContactID}");
                    telemetry.Context.GlobalProperties.Add(nameof(ContactInfo.ContactGUID), $"{contact.ContactGUID}");
                }
            }
        }
    }
}
