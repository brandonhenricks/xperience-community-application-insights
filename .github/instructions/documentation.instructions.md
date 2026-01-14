---
applyTo: ["*.md", "!**/node_modules/**"]
---

# Documentation Guidelines

When creating or updating documentation files (Markdown), follow these guidelines:

## File Types
- **README.md**: User-facing documentation for library consumers
- **CONTRIBUTING.md**: Guidelines for contributors
- **.github/copilot-instructions.md**: AI agent instructions (technical, detailed)
- **.github/instructions/*.instructions.md**: Path-specific AI agent guidance

## General Markdown Standards
1. **Use clear, concise language** - documentation should be easy to understand
2. **Include code examples** - show, don't just tell
3. **Keep examples up-to-date** - verify code samples actually work
4. **Use proper Markdown formatting**:
   - Headers: Use `#`, `##`, `###` hierarchy
   - Code blocks: Always specify language (```csharp, ```bash, etc.)
   - Lists: Use consistent formatting (ordered vs. unordered)
   - Links: Use descriptive text, not "click here"

## README.md Specific Guidelines
- **Target audience**: Library consumers (developers using the NuGet package)
- **Must include**:
  - Project description and purpose
  - Version compatibility matrix
  - Installation instructions (NuGet package install)
  - Basic configuration steps
  - Usage examples
  - Links to license, contributing guide, and issues
- **Update when**:
  - Public API changes (method signatures, service registration)
  - Breaking changes to configuration
  - New features are added
  - Minimum version requirements change

## CONTRIBUTING.md Specific Guidelines
- **Target audience**: Contributors to the repository
- **Must include**:
  - How to report issues
  - How to submit pull requests
  - Development setup instructions
  - Build and test commands
  - Coding standards and conventions
  - Package management details
- **Keep aligned** with `.github/copilot-instructions.md` for technical details

## copilot-instructions.md Specific Guidelines
- **Target audience**: AI coding agents (technical and detailed)
- **Must include**:
  - Project architecture overview
  - Build/test commands that actually work
  - Explicit coding conventions with examples
  - Boundaries and restrictions (what NOT to do)
  - Repository structure
  - Decision-making guidance (when to use X vs Y)
- **Use directive language**: "Always", "Never", "Must", "Should"
- **Provide code examples** for patterns to follow
- **Be specific and actionable** - avoid vague guidance

## Path-Specific Instructions (.instructions.md)
- **Use YAML frontmatter** with `applyTo` glob patterns
- **Focus on specific file types or areas** (e.g., Extensions, tests)
- **Include concrete examples** showing the exact pattern to follow
- **Keep scope narrow** - don't duplicate copilot-instructions.md content

## Examples to Follow

### Good code block:
```csharp
// Good example with language specified
services.AddApplicationInsightsTelemetry();
services.AddXperienceApplicationInsights();
```

### Bad code block:
```
// Bad - no language specified, harder to read
services.AddApplicationInsightsTelemetry();
```

### Good command documentation:
```bash
# Restore dependencies
dotnet restore

# Build for all target frameworks
dotnet build
```

### Bad command documentation:
Build the project with dotnet build.

## Validation Steps
Before committing documentation changes:
1. **Preview Markdown rendering** - ensure formatting is correct
2. **Test code samples** - verify commands and code actually work
3. **Check links** - ensure all URLs and file references are valid
4. **Verify consistency** - documentation should align across files
5. **Build the project** - ensure technical details are accurate

## Common Mistakes to Avoid
- Don't include outdated code samples
- Don't use broken links
- Don't make promises the code doesn't fulfill
- Don't forget to update version numbers
- Don't skip code language specifiers in code blocks
- Don't duplicate large blocks of content across multiple docs (DRY principle)
