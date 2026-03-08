# Core.Extensions

**Location:** `~/code/Core.Extensions`

**Purpose:** Utility extensions library — common helpers used across the Polyhydra ecosystem.

**Assembly:** `PolyhydraGames.Extensions`

**NuGet Dependency:** `Microsoft.Extensions.Logging.Abstractions` (8.0.1)

**Target Frameworks:** net8.0, net9.0

## Key Modules

| Module | Purpose |
|--------|---------|
| `EnumExtensions` | `ToEnum<T>` conversions, int/string → enum |
| `StringExtension` | String manipulation helpers |
| `IocExtensions` | DI helpers (`ThatImplement<T>`, `ThatHaveAttribute<T>`, `CreateDefault`) |
| `TypeExtensions` | Reflection utilities |
| `DateExtensions` | Date/time helpers |
| `TaskExtensions` | Async helpers |
| `EnumerableExtensions` | LINQ enhancements |
| `CryptoExtensions` | Hashing, encryption |
| `Dice/` | Dice rolling expression parser |

## Usage Pattern

```csharp
using PolyhydraGames.Extensions;

// Enum conversion
var myEnum = "value".ToEnum<MyEnum>();
var fromInt = 5.ToEnum<MyEnum>();

// DI helpers
var services = someAssembly.GetTypes().ThatImplement<IService>();
var handlers = types.ThatHaveAttribute<CommandHandlerAttribute>();

// Dice rolling
var result = DiceExpression.Parse("2d6+3").Roll();
```

## Dependencies From

This library is referenced by nearly everything — it's the base utility layer.

Common consumers:
- All `PolyhydraGames.*` APIs
- Core libraries
- Spotabot

## Status

✅ **Working** — Stable, no known issues.
