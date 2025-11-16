# Either

`Either<TLeft, TRight>` represent a value of one of two possible types (a disjoint union).
Either is semantically neutral, meaning that it can be used to represent a value that is either a left or a right value.
Extension methods, in common FP style, treat the right path as the success path, and the left path as the failure path.
This enables binding and mapping operations to be chained together.
For explicit success and failure handling however, the `Result<TValue` is preferred.

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
An instance of an `Either` can either contain a left value or a right value.
The left value, represented by `TLeft`, is often used as the error type, whereas the right value, represented by `TRight`, is often used as the success type.

```csharp
var left  = Either<string, int>.Left("error message");
var right = Either<string, int>.Right(42);
```

Instances can be created using the implicit conversion operator for the `TRight`, and an explicit conversion operator for the `TLeft`.
This distinction is made to avoid issues regarding ambiguous conversions in case of symmetric types (e.g. `Either<int, int>`)

```csharp
Either<string, int> left = (Either<string, int>)"error message";
Either<string, int> right = 42;
```

Use `IsLeft` and `IsRight` to check the type of the value.
`[MaybeNullWhen(false)]` annotations are used to ensure no null-forgiving operators are required.

```csharp
if (either.IsLeft(out var error))
{
    Log(error);
}

if (either.IsRight(out var r))
{
    Console.WriteLine($"Right: {r}");
}
```
---

## Extensions

The `Toarnbeike.Eithers.Extensions` namespace contains rich extension methods for `Either<TLeft, TRight>`:

| Method         | Applies To | Returns                   | Description                                         |
|----------------|------------|---------------------------|-----------------------------------------------------|
| `Bind(...)`    | `TRight`   | `Either<TLeft, TRight2>`  | Chains operations returning `Either<TLeft, TOut>`   |
| `Map(...)`     | `TRight`   | `Either<TLeft, TRight2>`  | Maps the value to a new value of type `TOut`        |
| `Map(...)`     | Both       | `Either<TLeft2, TRight2>` | Maps both operants at the same time                 |
| `MapLeft(...)` | `TLeft`    | `Either<TLeft2, TRight>`  | Maps the left value to a new value of type `TLeft2` |
| `Match(...)`   | Both       | `TOut`                    | Applies a function to both sides to return a `TOut` |
| `Match(...)`   | Both       | void                      | Applies an action to both sides, returning void.    |
| `Swap(...)`    | Both       | `Either<TRight,TLeft>`    | Swaps the left and right values.                    |
| `Tap(...)`     | `TRight`   | `Either<TLeft, TRight>`   | Execute side-effect on Right side.                  |
| `TapLeft(...)` | `TLeft`    | `Either<TLeft, TRight>`   | Execute side-effect on Left side.                   |

For more details regarding these methods, see the [Extensions details](Either.Extensions.md) readme.

---

## Collections

The `Toarnbeike.Eithers.Collections` namespace contains extension methods for working with collections of Eithers:

| Method           | Applies To                           | Returns                                     | Description                                                                               |
|------------------|--------------------------------------|---------------------------------------------|-------------------------------------------------------------------------------------------|
| `Traverse(...)`  | `IEnumerable<TValue>`                | `Either<TLeft, IEnumerable<TRight>>`        | Applies a function per element, returning a collection of `TRight`, or the first `TLeft`. |
| `AnyLeft(...)`   | `IEnumerable<Either<TLeft, TRight>>` | `bool`                                      | Returns true if any of the Eithers are in the left state.                                 |
| `AnyRight(...)`  | `IEnumerable<Either<TLeft, TRight>>` | `bool`                                      | Returns true if any of the Eithers are in the right state.                                |
| `CountLeft(...)` | `IEnumerable<Either<TLeft, TRight>>` | `int`                                       | The number of the Eithers in the left state.                                              |
| `CountLeft(...)` | `IEnumerable<Either<TLeft, TRight>>` | `int`                                       | The number of the Eithers in the right state.                                             |
| `Lefts(...)`     | `IEnumerable<Either<TLeft, TRight>>` | `IEnumerable<TLeft>`                        | Lazely collect all the Eithers in the left state.                                         |
| `Rights(...)`    | `IEnumerable<Either<TLeft, TRight>>` | `IEnumerable<TRight>`                       | Lazely collect all the Eithers in the right state.                                        |
| `Sequence(...)`  | `IEnumerable<Either<TLeft, TRight>>` | `Either<TLeft, IEnumerable<TRight>>`        | Flattens a collection of Eithers into a collection of `TRight`, or the first `TLeft`.     |
| `Partition(...)` | `IEnumerable<Either<TLeft, TRight>>` | `(IEnumerable<TRight>, IEnumerable<TLeft>)` | Splits the collection into two collections by type.                                       |

For more details regarding these methods, see the [Collections details](Either.Collections.md) readme (incomplete).

---

## LINQ support

The `Toarnbeike.Eithers.Linq` namespace contains extension methods for working with LINQ queries:
- Select = Map
- SelectMany = Bind

```csharp
Either<string, int> e1 = 2;
Either<string, int> e2 = 3;

var result =
    from x in e1
    from y in e2
    select x + y;

// Right(5)
```

### Why use LINQ Query Syntax?
While method chaining works well for most scenarios, C# LINQ query syntax can make some workflows more expressive and readable, especially when you want to:

- Name intermediate results using `let`
- Compose complex Either pipelines declaratively
- Avoid deeply nested lambdas in `Bind` and `Map`

---

## Test extensions

The `Toarnbeike.Eithers.TestExtensions` namespace contains extension methods for asserting on Eithers.
These extensions are compatible with any test framework. See [TestExtensions](TestExtensions.md) for more details.

---

## Integrations

Upon expanding the library, integrations with other parts of the library will be added here.

---

## Best practices

- Use `Left` for errors, and `Right` for successful results.
- Keep `Left` as light-weight as possible, preferably a simple `string` or `int`.
- Use `Match` as primary deconstring-method.
- Use `Select()` and `SelectMany()` only for chaining operations (Linq).

---