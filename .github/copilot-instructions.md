# XperienceCommunity.ApplicationInsights - AI Agent Instructions

## Project Overview
This is a .NET library that integrates Microsoft Application Insights with **Xperience by Kentico** CMS. It enriches Application Insights telemetry with Kentico-specific context data including web page routing information, channel context, and contact data.

**Core Architecture**: This is a single-assembly NuGet package targeting .NET 6.0, 7.0, and 8.0. It uses dependency injection to register telemetry enhancers that run during ASP.NET Core request processing.

## Key Components & Data Flow

### 1. Telemetry Initialization (`XperienceTelemetryInitializer`)
- Implements `ITelemetryInitializer` to add **global properties** to all telemetry
- Runs early in the request pipeline before actions execute
- Adds: `WebsiteChannelName`, `WebsiteChannelID`, `WebFarmServerName`, `ContactID`, `ContactGUID`
- Only initializes if `CMSApplication.ApplicationInitialized.HasValue` is true

### 2. Action Filter Enrichment (`XperienceApplicationInsightsFilter`)
- Implements `IActionFilter` and runs during **OnActionExecuted** (after action completes)
- Retrieves `RequestTelemetry` from `HttpContext.Features`
- Adds channel context properties: `WebsiteChannelID`, `WebsiteChannelName`, `IsPreview`
- If page data exists via `IWebPageDataContextRetriever`, adds routed page properties via extension method

### 3. Extension Method Pattern (`RequestTelemetryExtensions`)
- `AddRoutedWebPageTelemetry()` adds: `ContentTypeName`, `ContentTypeID`, `WebPageItemGUID`, `WebPageItemID`, `LanguageID`, `LanguageName`
- Uses nullable reference types - always checks for null before processing

## Critical Dependencies
- **Kentico.Xperience.WebApp** (v29.1.4+) - provides `IWebPageDataContextRetriever`, `RoutedWebPage`
- **Kentico.Xperience.Core** (v29.1.4+) - provides `IWebsiteChannelContext`, `ICurrentContactProvider`, `IWebFarmService`
- **Microsoft.ApplicationInsights.AspNetCore** (v2.22.0+) - provides `ITelemetryInitializer`, `RequestTelemetry`

## Setup Pattern for Consuming Apps
Users must register in **two places** (see README example):

```csharp
services.AddApplicationInsightsTelemetry();
services.AddXperienceApplicationInsights(); // Registers XperienceTelemetryInitializer
services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(XperienceApplicationInsightsFilter)); // Registers action filter
});
```

**Why two registrations?**: The initializer runs globally for all telemetry, while the filter only runs for MVC actions to access `IWebPageDataContextRetriever` from action context.

## Build & Development Workflow

### Build Configuration
- Multi-target build: `dotnet build` compiles for net6.0, net7.0, and net8.0 simultaneously
- Uses **Central Package Management** via `Directory.Packages.props` - all versions defined centrally
- Lock file enabled (`RestorePackagesWithLockFile=true`) - commit `packages.lock.json` changes
- Nullable reference types enforced: `<Nullable>enable</Nullable>` and `<WarningsAsErrors>nullable</WarningsAsErrors>`

### Key Build Commands
```bash
dotnet restore              # Restore with lock file
dotnet build                # Build all targets
dotnet pack -c Release      # Create NuGet package with symbols (.snupkg)
```

### CI/CD
- GitHub Actions workflow (`.github/workflows/dotnet.yml`) runs on master branch pushes/PRs
- Builds using .NET 8.0 SDK but produces multi-target outputs
- No test project currently exists

## Coding Conventions

### Naming & Structure
- **Sealed classes** for all implementations (`sealed class XperienceApplicationInsightsFilter`)
- Constructor injection for all dependencies
- Extension methods in dedicated `Extensions/` folder
- XML documentation required (`GenerateDocumentationFile=true`) but warnings suppressed (`<NoWarn>1591</NoWarn>`)

### Null Safety Pattern
Always use null-checks before processing:
```csharp
if (requestTelemetry is null) return;
if (routedWebPage is null) return;
```

### Property Addition Pattern
Use nameof() for property keys to avoid magic strings:
```csharp
requestTelemetry.Properties.Add(nameof(IWebsiteChannelContext.WebsiteChannelID), $"{value}");
```

### Dependency Injection
- Register services via extension methods on `IServiceCollection`
- Use `AddSingleton<ITelemetryInitializer, T>` for telemetry initializers
- Filters registered via MVC options, not DI container

## When Adding Features

### To Add New Telemetry Properties
1. Inject required Kentico service in constructor of `XperienceTelemetryInitializer` or `XperienceApplicationInsightsFilter`
2. Add property in appropriate method using `nameof()` pattern
3. Convert values to strings (Application Insights requires string properties)
4. **Decision point**: Use `XperienceTelemetryInitializer` for global properties available early; use `XperienceApplicationInsightsFilter` for action-scoped data

### To Add New Extension Methods
1. Create static class in `Extensions/` folder
2. Use `this RequestTelemetry?` pattern for extensions
3. Add XML documentation with `<summary>` and `<param>` tags
4. Always null-check parameters before processing

## Version Compatibility
- Kentico Xperience by Kentico v29.1.4+ (uses new namespace structure)
- Application Insights SDK v2.22.0+
- .NET 6.0 is minimum, but project should maintain multi-target support for all three frameworks
