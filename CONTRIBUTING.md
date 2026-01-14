# Contributing to XperienceCommunity.ApplicationInsights

Thank you for your interest in contributing to **XperienceCommunity.ApplicationInsights**! We welcome contributions from the community to help improve this project.

## How to Contribute

### Reporting Issues

If you encounter a bug or have a feature request, please [open an issue](https://github.com/brandonhenricks/xperience-community-application-insights/issues) on GitHub. When reporting issues, please include:

- A clear and descriptive title
- Steps to reproduce the issue
- Expected behavior vs. actual behavior
- Your environment details (.NET version, Kentico Xperience version, etc.)
- Any relevant error messages or logs

### Submitting Pull Requests

We love pull requests! Here's how to submit one:

1. **Fork the repository** to your own GitHub account
2. **Clone your fork** to your local machine
3. **Create a feature branch** from `master`:
   ```bash
   git checkout -b feature/your-feature-name
   ```
4. **Make your changes** following our coding standards (see below)
5. **Test your changes** by building the project:
   ```bash
   dotnet restore
   dotnet build
   ```
6. **Commit your changes** with clear, descriptive commit messages
7. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```
8. **Create a Pull Request** against the `master` branch of the main repository

### Development Setup

#### Prerequisites

- .NET 8.0 SDK (for building all target frameworks)
- An IDE or editor (Visual Studio, VS Code, Rider, etc.)
- Git

#### Building the Project

```bash
# Clone the repository
git clone https://github.com/brandonhenricks/xperience-community-application-insights.git
cd xperience-community-application-insights

# Restore dependencies
dotnet restore

# Build the project (builds for net6.0, net7.0, and net8.0)
dotnet build

# Create NuGet package
dotnet pack -c Release
```

## Coding Standards

Please follow these conventions when contributing:

### Code Style

- **Sealed classes**: Use sealed classes for all implementation classes
- **Constructor injection**: Use constructor injection for all dependencies
- **Nullable reference types**: Ensure nullable reference types are properly handled
- **XML documentation**: Add XML documentation for public APIs
- **Extension methods**: Place extension methods in the `Extensions/` folder

### Naming Conventions

- Use `nameof()` for property keys instead of magic strings
- Follow standard C# naming conventions (PascalCase for classes/methods, camelCase for private fields)

### Null Safety

Always use null-checks before processing:
```csharp
if (requestTelemetry is null) return;
if (routedWebPage is null) return;
```

### Dependency Injection

- Register services via extension methods on `IServiceCollection`
- Use `AddSingleton<ITelemetryInitializer, T>` for telemetry initializers
- Filters are registered via MVC options, not the DI container

## Testing

Currently, this project does not have a dedicated test project. When making changes:

1. Ensure the project builds successfully for all target frameworks
2. Manually verify your changes don't break existing functionality
3. If adding new features, consider whether test coverage should be added in the future

## Package Management

This project uses **Central Package Management** via `Directory.Packages.props`. All package versions are defined centrally in this file, not in individual project files.

When adding or updating dependencies:
- Update versions in `Directory.Packages.props`
- Run `dotnet restore` to update the `packages.lock.json` file
- **Always commit the updated `packages.lock.json`** file

## Pull Request Guidelines

- Keep pull requests focused on a single feature or bug fix
- Update documentation if your changes affect how users interact with the library
- Ensure your code builds without warnings or errors
- Follow the existing code style and patterns
- Write clear commit messages describing your changes

## Code of Conduct

Please be respectful and constructive in all interactions. We're all here to make this project better!

## Questions?

If you have questions about contributing, feel free to:
- Open an issue for discussion
- Reach out to the maintainers
- Check existing issues and pull requests for similar discussions

Thank you for contributing to XperienceCommunity.ApplicationInsights! 🎉
