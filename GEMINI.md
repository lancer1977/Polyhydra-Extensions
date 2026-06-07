# Core.Extensions Project Manifest

This file provides persistent context for Gemini CLI to ensure rapid project re-entry and alignment with architectural standards.

## 🏗️ Architectural Overview

- **Pattern:** Comprehensive .NET Extension Library.
- **Scope**: Async, Collections, Dice Expressions, Enums, Guids, Strings, and Time.
- **Testing:** NUnit + Moq.

## 🛠️ State (April 2026)

### Key Features
- **DiceExpression**: Robust parser and evaluator for RPG-style dice strings (e.g., "1d20+5").
- **AsyncExtensions**: Helpers for Task management and synchronization.
- **CollectionExtensions**: Fluent operations for enumerable and collection types.

### Test Coverage
- **Status**: Substantial.
- **Project**: `PolyhydraGames.Extensions.Tests` (NUnit).
- **Focus**: Dice expression accuracy, collection manipulation, and string/GUID formatting.

## 📋 Coding Standards
- **Simplicity**: Extensions should be "pure" functions where possible, avoiding side effects.
- **Naming**: Consistent with .NET BCL patterns.
- **Documentation**: XML comments are mandatory for all public extension methods.
