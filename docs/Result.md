# Result

`Result` and `Result<TValue>` model the outcome of an operation that can either succeed or fail.

- `Result` represents success/failure without a value.
- `Result<TValue>` represents success/failure with a value when successful.

The failure path carries a `Failure` object with a code and message, enabling rich error handling without throwing exceptions.

---

## Contents
1. [Basic usage](#basic-usage)
2. [Extensions](#extensions)
3. [Collections](#collections)
4. [LINQ support](#linq-support)
5. [Test extensions](#test-extensions)
6. [Integrations](#integrations)
7. [Best practices](#best-practices)

---

## Basic usage

Create results explicitly using the static factories:

```csharp
// Non-generic result
var ok  = Result.Success();
var err = Result.Failure(new Failure("not_found", "Resource was not found"));

// Generic result
var ok42   = Result.Success(42);
var errStr = Result<string>.Failure(new Failure("parse_error", "Could not parse"));
```

`Result<T>` supports implicit conversions for convenience:

```csharp
Result<int> okImplicit = 42;                       // Success(42)
Result<int> errImplicit = new Failure("x", "y"); // Failure(x: y)
```

Inspect a result using `TryGetValue` / `TryGetFailure`:

```csharp
if (ok42.TryGetValue(out var value))
{
    Console.WriteLine($"Success: {value}");
}

if (err.TryGetFailure(out var failure))
{
    LogWarning($"Failure: {failure.Code} - {failure.Message}");
}
```

Execute potentially-throwing code safely with `Try` helpers:

```csharp
var load = Result.Try(() => File.ReadAllText(path));           // Result<string>
var run  = Result.Try(() => DoWork());                          // Result
var task = await Result.TryAsync(async () => await DoWorkAsync()); // Result
```

Exceptions are captured as `ExceptionFailure` with the exception instance available.

---

## Extensions

The `Toarnbeike.Results.Extensions` namespace contains rich extension methods for `Result` and `Result<TValue>`:

| Method           | Applies To                  | Returns                  | Description                                                                 |
|------------------|-----------------------------|--------------------------|-----------------------------------------------------------------------------|
| `Bind(...)`      | `Result`, `Result<T>`       | `Result<TOut>`           | Chains operations that return a `Result`, short-circuiting on failure.      |
| `Map(...)`       | `Result<T>`                 | `Result<TOut>`           | Transforms the success value.                                               |
| `Check(...)`     | `Result<T>`                 | `Result<T>`              | Keeps success only if predicate holds; otherwise replaces with a failure.   |
| `Match(...)`     | `Result`, `Result<T>`       | `TOut`                   | Converts a result by providing functions for success and failure.           |
| `Tap(...)`       | `Result`, `Result<T>`       | same as input            | Executes side-effects on success, does not change the result.               |
| `TapFailure(...)`| `Result`, `Result<T>`       | same as input            | Executes side-effects on failure, does not change the result.               |
| `WithValue(...)` | `Result`                    | `Result<T>`              | Supplies a value for a successful non-generic result.                       |
| `Zip(...)`       | `Result<T1>`                | `Result<(T1,T2)>/TResult`| Combine two successful results into one (tuple or projected).               |
| `ToOption()`     | `Result<T>`                 | `Option<T>`              | Convert to `Option<T>` (drops failure info).                                 |

Many of these include async overloads and `Task<Result<...>>` variants. For details, see the [Extensions details](Result.Extensions.md) readme.

---

## Collections

The `Toarnbeike.Results.Collections` namespace contains helpers for working with collections of results:

| Method            | Applies To                             | Returns                      | Description                                                                           |
|-------------------|----------------------------------------|------------------------------|---------------------------------------------------------------------------------------|
| `Sequence(...)`   | `IEnumerable<Result<T>>`               | `Result<IEnumerable<T>>`     | Fail-fast: first failure or all success values collected.                             |
| `Traverse(...)`   | `IEnumerable<T>`                       | `Result<IEnumerable<T>>`     | Apply function per element returning `Result<T>`; fail-fast aggregation.              |
| `Aggregate(...)`  | `IEnumerable<Result>` / `Result<T>`    | `Result` / `Result<IEnumerable<T>>` | Aggregate failures into `AggregateFailure`; collect successes.                 |
| `Partition()`     | `IEnumerable<Result<T>>`               | `(IEnumerable<T>, IEnumerable<Failure>)` | Split into successes and failures.                                        |
| `SuccessValues()` | `IEnumerable<Result<T>>`               | `IEnumerable<T>`             | Extract only successful values.                                                       |
| `Failures()`      | `IEnumerable<Result>` / `Result<T>`    | `IEnumerable<Failure>`       | Extract only failures.                                                                |

Async variants exist for enumerables of `Task<Result>`/`Task<Result<T>>`. For more details, see the [Collections details](Result.Collections.md) readme.

---

## LINQ support

The `Toarnbeike.Results.Linq` namespace provides LINQ support:
- Select = Map
- SelectMany = Bind
- Where = Check (creates a standard `Failure` when predicate fails)

```csharp
Result<int> r1 = 2;
Result<int> r2 = 3;

var sum =
    from x in r1
    from y in r2
    where x > 0 && y > 0
    select x + y;

// Success(5)
```

---

## Test extensions

The `Toarnbeike.Results.TestExtensions` namespace contains extensions for asserting on Results in tests (framework-agnostic). See [TestExtensions](TestExtensions.md) for more details.

---

## Integrations

Upon expanding the library, integrations with other parts of the library will be added here.

---

## Best practices

- Prefer returning `Result`/`Result<T>` over throwing exceptions for expected error conditions.
- Use `Failure` codes consistently to enable reliable matching and diagnostics.
- Use `Bind`/`Map` to build readable pipelines; use `Match` at boundaries to convert to plain values.
- Use `Try` helpers to wrap exception-throwing code when needed.
- Use `ToOption()` when you do not care about the failure information.

---
