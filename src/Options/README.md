![CI](https://github.com/Toarnbeike/Toarnbeike.Functional/actions/workflows/build.yaml/badge.svg)
[![.NET 11](https://img.shields.io/badge/.NET-11.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Options

This package provides a **lightweight and expressive [Option (Maybe) monad](https://en.wikipedia.org/wiki/Option_type)** for explicit handling of missing values in .NET,
building on the C# 11 feature of Unions.  

An `Option<TValue>` is a C#11 `public union Option<TValue>(TValue, None)` representing a value that can either be there, or be absent, without relying on nulls. 
It improves code clarity and safety by making the **absence of a value explicit**, replacing null checks and exceptions with a functional approach.

## Features

- **Explicit missing value handling** Avoid using nulls for missing values; make absent a meaningful feature.
- **Fluent extension methods:** Compose operations with `Bind`, `Map`, `Tap`, `Reduce`, and more.
- **Fluent collection extensions:** Handle collections with missing values using `WhereValues`, `SelectValues`, `FirstOrNone` and more.
* **Seamless async support:** Works naturally with Task and async pipelines.
- **Functional programming inspired:** Inspired by FP principles for predictable, readable, and maintainable error handling.
 
## Quick start

```csharp
using Toarnbeike.Options;			
using Toarnbeike.Options.Extensions;

Option<string> message = "Hello World!";
Option<string> missing = Option.None;

// Transform the value
var messageLength = message.Map(value => value.Length);                     // Option<int> with value 12
var newMissing = missing.Map(_ => "Not executed");                          // Option<string>, still None.

// Consume the value
Console.WriteLine($"Message had {messageLength.Reduce(0)} characters");    // Message had 12 characters
Console.WriteLine($"Missing was replaced by: {newMissing.Reduce("Hello")); // Missing was replaced by: Hello
```

Key properties of working with Unions:
- `null` values and nullable notation is replaced with `Option`.
- Values are never consumed directly, but always using `Reduce()` or `Match()` to provide an alternative for a missing value.
- Mapping, binding, etc. modifies the value in an `Option` state, but missing values remain `Option.None`.

For a full developer guide, see the [documentation](https://github.com/Toarnbeike/Toarnbeike.Functional/).

---