# XperienceCommunity.ApplicationInsights - AI Agent Instructions

## Project Overview
This is a .NET library that integrates Microsoft Application Insights with **Xperience by Kentico** CMS. It enriches Application Insights telemetry with Kentico-specific context data including web page routing information, channel context, and contact data.

**Core Architecture**: This is a single-assembly NuGet package targeting .NET 6.0, 7.0, and 8.0. It uses dependency injection to register telemetry enhancers that run during ASP.NET Core request processing.

## Quick Start - Build & Test

### Required Before Each Change
1. **Restore dependencies**: `dotnet restore` (updates packages.lock.json if needed)
2. **Build**: `dotnet build --no-restore` (must succeed for all three target frameworks)
3. **Verify no warnings**: Build output must show 0 warnings, 0 errors

### Full CI Verification
- **Local build**: `dotnet build` - builds for net6.0, net7.0, and net8.0
- **Package creation**: `dotnet pack -c Release` - creates NuGet package with symbols
- **CI/CD**: GitHub Actions workflow runs on master branch (see `.github/workflows/dotnet.yml`)

### Testing
- **No test project currently exists** - manual verification required
- When making changes, verify they work by building successfully
- Consider whether changes warrant adding test infrastructure in the future

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

## Repository Structure
```
├── .github/
│   ├── copilot-instructions.md    # This file - AI agent guidance
│   └── workflows/                  # CI/CD workflows
├── src/
│   └── XperienceCommunity.ApplicationInsights/
│       ├── DependencyInjection.cs              # Service registration
│       ├── XperienceTelemetryInitializer.cs    # Global telemetry properties
│       ├── XperienceApplicationInsightsFilter.cs # Action-scoped enrichment
│       ├── Extensions/                         # Extension methods
│       └── packages.lock.json                  # Lock file (must commit changes)
├── Directory.Build.props           # Shared build properties & metadata
├── Directory.Build.targets         # Shared build targets
├── Directory.Packages.props        # Central package version management
├── README.md                       # User documentation
└── CONTRIBUTING.md                 # Contributor guidelines

```

## Build Configuration Details
- **Multi-target build**: `dotnet build` compiles for net6.0, net7.0, and net8.0 simultaneously
- **Central Package Management**: All package versions defined in `Directory.Packages.props`
- **Lock file**: `RestorePackagesWithLockFile=true` - always commit `packages.lock.json` changes
- **Nullable reference types**: Enforced via `<Nullable>enable</Nullable>` and `<WarningsAsErrors>nullable</WarningsAsErrors>`
- **XML documentation**: Generated but warnings suppressed via `<NoWarn>1591</NoWarn>`

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

## Boundaries & Restrictions

### What You Must NOT Do
1. **Never commit secrets or API keys** to source code or configuration files
2. **Never remove multi-target framework support** - must build for net6.0, net7.0, and net8.0
3. **Never disable nullable reference type checking** - this is enforced project-wide
4. **Never skip committing packages.lock.json** changes after updating dependencies
5. **Never add dependencies without updating Directory.Packages.props** - uses central package management
6. **Never suppress null warnings** - fix the underlying null-safety issue instead
7. **Never change build artifacts or bin/obj folders** - these are git-ignored
8. **Never disable XML documentation generation** - it's required for NuGet package
9. **Never break backward compatibility** without major version bump

### What You Should Minimize
1. Adding new dependencies - only add if absolutely necessary
2. Breaking existing telemetry property names - consumers may depend on them
3. Changes to public API surface - this is a library used by other applications
4. Refactoring working code - prefer surgical, minimal changes

### What to Always Do
1. **Always verify null safety** - use null checks before processing
2. **Always test all target frameworks** - changes must work on net6.0, 7.0, and 8.0
3. **Always update README** if changing public API or setup instructions
4. **Always use nameof()** for property keys to avoid magic strings
5. **Always commit packages.lock.json** when dependencies change
