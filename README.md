![CI](https://github.com/Toarnbeike/Toarnbeike.Functional/actions/workflows/build.yaml/badge.svg)
[![Code Coverage](https://toarnbeike.github.io/Toarnbeike.Results/badge_shieldsio_linecoverage_brightgreen.svg)](https://github.com/Toarnbeike/Toarnbeike.Functional/blob/gh-pages/SummaryGithub.md)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Functional

Lightweight functional primitives for modern C#: `Option<T>`, `Result`/`Result<T>`, and `Either<TLeft, TRight>` with first‑class support for extensions, collections, and LINQ.

The library focuses on:
- Clear, explicit modeling of absence and failure (no exceptions for expected control flow)
- Composable pipelines with short‑circuiting semantics
- Async‑friendly APIs (`Task<Result<...>>` and async methods)
- Test‑friendly helpers to assert on functional types

Works with C# 14 / .NET 10.0.

---

## Contents
- [What’s included](#whats-included)
- [Quick start](#quick-start)
- [Option overview and docs](#option--overview-and-docs)
- [Result overview and docs](#result--overview-and-docs)
- [Either overview and docs](#either--overview-and-docs)
- [LINQ support](#linq-support)
- [Collections helpers](#collections-helpers)
- [Testing extensions](#testing-extensions)
- [Project structure](#project-structure)
- [License](#license)

---

## What’s included

- Option
  - Core type: `Option<T>` with `Some`/`None`
  - Extensions: map, bind, match, check, reduce, orElse, tap, nullability helpers
  - Collections helpers for working with `IEnumerable<Option<T>>`
- Result
  - Core types: `Result` and `Result<T>` with rich `Failure` information
  - Extensions: bind, map, check, match, tap/tapFailure, zip, withValue, toOption, unsafe helpers
  - Collections helpers: sequence, traverse, aggregate, partition, failures/success values
- Either
  - Core type: `Either<TLeft, TRight>` (neutral); extensions treat Right as the success path
  - Extensions: bind/map (both sides), mapLeft, match, swap, tap/tapLeft

All three come with LINQ query syntax support where it makes sense.

See the in‑repo docs for details and complete API tables.

---

## Quick start

```csharp
using Toarnbeike.Options;
using Toarnbeike.Results;
using Toarnbeike.Results.Extensions;

// Option
Option<int> maybe = 42;
var message = maybe
    .Map(x => x * 2)
    .Match(some => $"Double: {some}", whenNone: () => "No value");

// Result
Result<int> parsed = Result.Try(() => int.Parse("123"));
var text = parsed
    .Check(x => x > 0, () => new Failure("not_positive", "Expected positive"))
    .Map(x => x * 2)
    .Match(
        onSuccess: v => $"Value: {v}",
        onFailure: f => $"Error: {f.Code} - {f.Message}");

// Either (Right is the happy path for extensions)
var either = Either<string, int>.Right(5)
    .Map(x => x + 1)
    .Match(onLeft: l => $"Left: {l}", onRight: r => $"Right: {r}");
```

---

## Option – overview and docs

`Option<T>` represents presence (`Some`) or absence (`None`) of a value.

- Overview: see docs/Option.md
- Extensions reference: docs/Option.Extensions.md
- Collections helpers: docs/Option.Collections.md

Links:
- [Option overview](docs/Option.md)
- [Option extensions](docs/Option.Extensions.md)
- [Option collections](docs/Option.Collections.md)

---

## Result – overview and docs

`Result`/`Result<T>` model the outcome of an operation. On failure, a `Failure` with `Code` and `Message` is carried (no exceptions for expected errors). `Result.Try` helpers capture exceptions as an `ExceptionFailure` when you do have to call throwing APIs.

Highlights:
- Implicit conversions for `Result<T>` from value and from `Failure`
- LINQ support: `Select`/`SelectMany`/`Where` map to `Map`/`Bind`/`Check`
- Async‑friendly: `BindAsync`, `MapAsync`, etc., and `Task<Result<T>>` overloads

Links:
- [Result overview](docs/Result.md)
- [Result extensions](docs/Result.Extensions.md)
- [Result collections](docs/Result.Collections.md)

---

## Either – overview and docs

`Either<TLeft, TRight>` is a disjoint union. Extensions consider the Right side as the success path to enable composition.

Links:
- [Either overview](docs/Either.md)
- [Either extensions](docs/Either.Extensions.md)
- [Either collections](docs/Either.Collections.md)

---

## LINQ support

The library provides LINQ query syntax for `Option`, `Result`, and `Either` where applicable, mapping to the underlying composition functions (e.g., `Select = Map`, `SelectMany = Bind`). This enables clear, declarative pipelines:

```csharp
using Toarnbeike.Results.Linq; // similarly for Options/Eithers

Result<int> a = 2;
Result<int> b = 3;

var sum =
    from x in a
    from y in b
    where x > 0 && y > 0
    select x + y; // Success(5)
```

See the type‑specific docs for details and examples.

---

## Collections helpers

Utilities for working with sequences of functional values are included for `Option` and `Result` (and for `Either`):
- `Sequence` / `Traverse` to convert and evaluate collections in a fail‑fast way
- `Aggregate` to collect successes and compose failures
- `Partition`, `Failures()`, `SuccessValues()` helpers

See the collections docs listed above for each type.

---

## Testing extensions

Each type includes small assertion extensions to make unit tests expressive and framework‑agnostic (e.g., `ShouldBeSuccess()`, `ShouldBeFailureWithCode(...)`, `ShouldBeSomeWithValue(...)`, and more). Use them to keep tests declarative and intention‑revealing.

---

## Project structure

```
src/Functional
  Options/            // Option<T> + extensions, collections, linq, test extensions
  Results/            // Result/Result<T> + extensions, collections, failures, linq, test extensions
  Eithers/            // Either<TLeft, TRight> + extensions, collections, linq, test extensions
docs/
  Option*.md, Result*.md, Either*.md
tests/Functional.Tests
  ...
```

---

## License

This project is licensed under the terms of the MIT License. See the [LICENSE](LICENSE) file for details.

