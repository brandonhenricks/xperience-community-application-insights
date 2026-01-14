# XperienceCommunity.ApplicationInsights

Welcome to the **XperienceCommunity.ApplicationInsights** project! This repository provides seamless integration of Microsoft Application Insights with Xperience By Kentico websites. The primary goal is to enrich Request Telemetry with detailed RoutedWebPage data and WebsiteChannel Context data, enabling deeper insights into web application performance and user interactions.

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://github.com/brandonhenricks/xperience-community-application-insights/actions/workflows/dotnet.yml/badge.svg)](https://github.com/brandonhenricks/xperience-community-application-insights/actions)


## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## Introduction

**XperienceCommunity.ApplicationInsights** enhances your Xperience By Kentico websites by integrating Microsoft Application Insights. This integration allows you to capture and analyze telemetry data, providing valuable insights into your website's performance and user behavior. By enriching Request Telemetry with RoutedWebPage and WebsiteChannel Context data, this project ensures that you have a comprehensive understanding of your site's operational metrics.

## Features

- **Seamless Integration**: Easily integrate Microsoft Application Insights with Xperience By Kentico websites.
- **Enhanced Telemetry**: Enrich Request Telemetry with RoutedWebPage data and WebsiteChannel Context data.
- **In-depth Analysis**: Gain deeper insights into web application performance and user interactions.
- **Extensibility**: Extend and customize telemetry data to suit your specific needs.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

## Library Version Matrix

| Xperience Version | Library Version | .NET Version     |
| ----------------- | --------------- | ---------------- |
| >= 29.7.3         | >= 0.0.0.2      | .NET 6.0/7.0/8.0 |
| >= 29.1.4         | 0.0.0.1         | .NET 6.0/7.0/8.0 |

## Installation

To install **XperienceCommunity.ApplicationInsights**, follow these steps:

1. **NuGet Package**: Install the NuGet package via the Package Manager Console.

   ```shell
   Install-Package XperienceCommunity.ApplicationInsights
   ```

2. **Configure Services**: Add the necessary services to your `Startup.cs` file.

   ```csharp
   public void ConfigureServices(IServiceCollection services)
   {
       services.AddApplicationInsightsTelemetry();
       services.AddXperienceApplicationInsights();
       services.AddControllersWithViews(options =>
       {
           options.Filters.Add(typeof(XperienceApplicationInsightsFilter));
       });
   }
   ```

## Configuration

Configure the integration by updating your `appsettings.json` file with the necessary Application Insights settings.

```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "YOUR_INSTRUMENTATION_KEY"
  }
}
```

## Contributing

Contributions are welcome! Please read our [Contributing Guide](CONTRIBUTING.md) to learn how you can help improve this project.

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Push to the branch.
5. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Acknowledgements

Special thanks to the community for their continuous support and contributions. 

---

Thank you for using **XperienceCommunity.ApplicationInsights**! We hope this project enhances your telemetry data and provides valuable insights into your Xperience By Kentico websites. For any questions or issues, please open an issue on GitHub or reach out to our community.

Happy coding!
