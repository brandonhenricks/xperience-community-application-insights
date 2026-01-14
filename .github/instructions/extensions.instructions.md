---
applyTo: "**/Extensions/**/*.cs"
---

# Extension Methods Guidelines

When creating or modifying extension methods in the `Extensions/` folder, follow these specific patterns:

## Naming Conventions
- Extension class names should describe what they extend: `{Type}Extensions.cs`
- Example: `RequestTelemetryExtensions.cs` for extensions on `RequestTelemetry`

## Method Signature Pattern
Always use nullable `this` parameter to enable null-conditional operator:
```csharp
public static void AddRoutedWebPageTelemetry(this RequestTelemetry? requestTelemetry, RoutedWebPage routedWebPage)
{
    if (requestTelemetry is null) return;
    // Implementation
}
```

## Required Elements
1. **XML Documentation**: Every extension method must have:
   ```csharp
   /// <summary>
   /// Brief description of what the extension does
   /// </summary>
   /// <param name="requestTelemetry">Description of parameter</param>
   /// <param name="routedWebPage">Description of parameter</param>
   ```

2. **Null Checks**: Always check for null on both the extended object and parameters:
   ```csharp
   if (requestTelemetry is null) return;
   if (routedWebPage is null) return;
   ```

3. **Property Keys**: Use `nameof()` for property keys to avoid magic strings:
   ```csharp
   requestTelemetry.Properties.Add(nameof(RoutedWebPage.WebPageItemID), $"{routedWebPage.WebPageItemID}");
   ```

4. **String Conversion**: Application Insights requires string properties, so convert all values:
   ```csharp
   // Good
   requestTelemetry.Properties.Add(nameof(prop), $"{value}");
   
   // Bad - don't add non-string values
   requestTelemetry.Properties.Add(nameof(prop), value);
   ```

## Class Structure
- Extension classes must be `static`
- Extension classes must be `public` and `sealed` is not applicable
- Group related extensions in the same file
- Place using statements at the top, not inside namespace

## Example Complete Extension Method
```csharp
/// <summary>
/// Adds routed web page telemetry data to the Application Insights request.
/// </summary>
/// <param name="requestTelemetry">The request telemetry instance to enrich.</param>
/// <param name="routedWebPage">The routed web page containing the data to add.</param>
public static void AddRoutedWebPageTelemetry(this RequestTelemetry? requestTelemetry, RoutedWebPage? routedWebPage)
{
    if (requestTelemetry is null) return;
    if (routedWebPage is null) return;

    requestTelemetry.Properties.Add(nameof(RoutedWebPage.WebPageItemID), $"{routedWebPage.WebPageItemID}");
    requestTelemetry.Properties.Add(nameof(RoutedWebPage.WebPageItemGUID), $"{routedWebPage.WebPageItemGUID}");
    // ... more properties
}
```

## Testing Extension Methods
- Build the project to ensure all target frameworks compile successfully
- Verify nullable reference type warnings are not suppressed
- Manually test that properties are correctly added to telemetry
