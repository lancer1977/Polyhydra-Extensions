# Polyhydra-Extensions

Polyhydra-Extensions is a **NuGet library** providing a collection of **C# extension methods** to simplify common programming tasks, including text manipulation, collections, enums, and more.

[![NuGet Badge](https://img.shields.io/nuget/v/Polyhydra-Extensions.svg)](https://www.nuget.org/packages/PolyhydraGames.Extensions)

## Features

- 📝 **Text Extensions**: Simplifies string manipulations and validations.
- 📦 **Collection Extensions**: Enhances operations on collections like lists and dictionaries.
- 🎭 **Enum Extensions**: Facilitates conversions and operations on enumeration types.
- 🔧 **Utility Methods**: Adds helper functions for various programming needs.

## Installation

Install the package from NuGet:

```sh
 dotnet add package PolyhydraGames.Extensions
```

## Usage Example

### Enum Extension Example
```csharp
using Polyhydra.Extensions;

public enum Status
{
    Active,
    Inactive,
    Pending
}

public void Example()
{
    Status currentStatus = Status.Active;
    string statusName = currentStatus.ToString(); // Enum to string conversion
    // Additional extension methods can be utilized here
}
```

## Roadmap
- [ ] Add more extension methods
- [ ] Improve documentation and examples
- [ ] Provide benchmark comparisons for performance impact

## Contributing

Contributions are welcome! If you'd like to contribute:

1. **Fork** the repository.
2. **Create** a new branch.
3. **Implement** your feature or fix.
4. **Submit** a pull request.

## License

📜 This project is licensed under the **MIT License**.

## Links
- [GitHub Repository](https://github.com/lancer1977/Polyhydra-Extensions)
- [NuGet Package](https://www.nuget.org/packages/Polyhydra-Extensions/)

