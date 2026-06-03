# Core.Extensions

[![Build Status](https://img.shields.io/github/actions/workflow/user/lancer1977/Polyhydra-Extensions/.github/workflows/ci.yml/badge.svg)](https://github.com/lancer1977/Polyhydra-Extensions/actions)
[![NuGet](https://img.shields.io/nuget/v/PolyhydraGames.Extensions.svg)](https://www.nuget.org/packages/PolyhydraGames.Extensions/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Tags

- dotnet
- core
- core-extensions
- docs
- testing
- extensions

## Related Repos

- [`Core`](../Core/)
- [`Core.Models`](../Core.Models/)
- [`Core.Interfaces`](../Core.Interfaces/)
- [`Core.Serialization`](../Core.Serialization/)
- [`Core.SQL`](../Core.SQL/)

## 🚀 Overview
A comprehensive .NET extension library providing common utilities, methods, and helper types for PolyhydraGames projects. It covers areas like Async operations, Collections, Dice Expressions, Enums, GUIDs, Strings, and Time manipulation.

## ✨ Key Features
*   **Extension Methods**: Enhances existing .NET types with fluent and helpful operations.
*   **Dice Expression Parser**: Robust evaluation for RPG-style dice strings (e.g., "1d20+5").
*   **Async Helpers**: Utilities for efficient Task management and synchronization.
*   **Collection Utilities**: Fluent operations for Enumerable and Collection types.
*   **String & GUID Formatting**: Consistent handling of formatting for these types.

## 🏗️ Architecture
A comprehensive .NET Extension Library. Extensions are designed to be "pure" functions where possible, avoiding side effects. Naming conventions are consistent with .NET BCL patterns, and XML comments are mandatory for public extension methods. The project structure includes an `.sln` file but lacks individual `.csproj` files, suggesting reliance on global build properties.

### 🛠️ Technology Stack
*   **Language**: C#
*   **Framework**: .NET 8+
*   **Testing**: NUnit + Moq

## 🚦 Getting Started

### Prerequisites
*   .NET 8 SDK (or compatible version)
*   An IDE that supports C# development.

### Installation
```bash
# Clone the repository
git clone git@github.com:lancer1977/Polyhydra-Extensions.git
cd Polyhydra-Extensions

# Restore NuGet packages (likely managed via global build properties and solution file)
dotnet restore
```

## 📖 Usage & Education
```csharp
using PolyhydraGames.Extensions; // Import the namespace

// Example usage of an extension method
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var sum = numbers.Sum(); // Standard Sum
var average = numbers.Average(); // Standard Average

// Example of a potential extension (conceptual)
// var reversedNumbers = numbers.Reverse();
```
Refer to the detailed documentation for specific extension methods and utilities.

## 🌐 Deployment & Hosting
*   **Repo**: [Polyhydra-Extensions](https://github.com/lancer1977/Polyhydra-Extensions)
*   **Hosting Platform**: GitHub.
*   **NuGet**: [PolyhydraGames.Extensions](https://www.nuget.org/packages/PolyhydraGames.Extensions/)

## 📦 Packages & Dependencies
*   **NuGet**: `PolyhydraGames.Extensions` (Package name inferred)
*   **Local Projects**: `Core.Interfaces`, `Core.Serialization` (Likely dependencies)

## 🔗 Related Projects
*   Other `Core.*` libraries within the Polyhydra Games ecosystem.

## 📚 Documentation & Resources
*   **Features**: [Docs/Features](./docs/features/README.md)
*   **CI/CD**: [GitHub Actions](https://github.com/lancer1977/Polyhydra-Extensions/actions)

---
*This README was generated based on project metadata and description.*
