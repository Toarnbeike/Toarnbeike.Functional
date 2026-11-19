# Toarnbeike.Options

This package provides a **lightweight and expressive [Option (Maybe) monad](https://en.wikipedia.org/wiki/Option_type)** for explicit handling of missing values in .NET.  

It introduces **`Option<TValue>`** type, inspired by functional programming and discriminated unions, 
while remaining idiomatic to the .NET ecosystem.

An `Option<TValue>` represents a value that can either be there, or be absent, without reling on nulls. 
It improves code clarity and safety by making the **absence of a value explicit**, replacing null checks and exceptions with a functional approach.

## Features

- **Explicit missing value handling** Avoid using nulls for missing values; make absent a meaningful feature.
- **Fluent extension methods:** Compose operations with `Bind`, `Map`, `Match`, `Reduce`, and more.
- **Fluent collection extensions:** Handle collections with missing values using `WhereValues`, `SelectValues`, `FirstOrNone` and more.
- **Seamless async support:** Works naturally with Task and async pipelines.
- **Functional programming inspired:** Inspired by FP principles for predictable, readable, and maintainable error handling.
 
---

## Contents
1. [Core concepts](#core-concepts)
1. [Extensions](#extensions)
1. [Collections](#collections)
1. [Linq query syntax](#linq-query-syntax)
1. [Test extensions](#test-extensions)
1. [Conclusion](#conclusion)

---

## Core Concepts

### What is an `Option<TValue>`

An option can represent either a value (`Some`) with an attached value of type `TValue`, or no value (`None`). 
At any point, the option is either Some, with value, or None, without value.

Any `TValue` can implicitly be converted to `Option<TValue>`; the option is a container (monad) that contains the value,
and supplies functional methods to transform it's value.

### What is `None`

None is a state of the `Option<TValue>` that indicated that no value is present.
If the option is in the `None` state, a value can be supplied using the `Reduce` function, with reduces the `Option<TValue>` to `TValue` by providing an alternative value.

### Construction

An `Option<TValue>` can either be created through the static factory methods on `Option<TValue>`, or using implicit conversions.
In the example below, some1 and some2 are equivalent, as are none1 and none2.

``` csharp
Option<int> some1 = 42;
var some2 = Option<int>.Some(42);

Option<int> none1 = Option.None;
var none2 = Option<int>.None;
```

### Transformations

Options can be transformed on value using the many provided extension methods.
For an overview of the available methods, see [Extension methods](#extensions).
For a detailed description of each of the methods, see [Extension docs](Options.Extensions.md).

### Consumption

Options can be consumed either by
- using the `Reduce` method. This provides an alternative value when the option is `None`.
- since `Option<T>` is a union in either the TValue state or the None state, the union can also be consumed using a regular switch expression.

```csharp
string output = option.Reduce("fallback"); // if option whas None, the value fallback is used.
return option switch
{
    TValue value => $"Option had value: {value}",
    None => "Option was None"
};
```

---

## Extensions
The `Toarnbeike.Options.Extensions` namespace includes rich extensions for `Option<T>`:

### Available Extensions
| Method			| Returns       | Description								    |
|-------------------|---------------|-----------------------------------------------|
| `AsNullable()`	| `T?`          | Convert to nullable						    |
| `AsOption()`		| `Option<T>`   | Convert from nullable						    |
| `Map(...)`		| `Option<U>`   | Transforms the inner value 			        |
| `Bind(...)`		| `Option<U>`   | Chain operations returning `Option<T>`        |
| `Check(...)`		| `Option<T>`   | Filter by predicate, make `None` if false     |
| `Reduce(...)`		| `T`           | Fallback to a value if empty      		    |
| `Tap(...)`		| `Option<T>`   | Execute side-effect on value				    |
| `TapIfNone()`		| `Option<T>`   | Execute side-effect when empty                |

All methods support `async` variants and operate seamlessly with `Task<Option<TValue>>`.

For information per method, see the [Extensions docs](docs/Extensions.md).

---

## Collections
The `Toarnbeike.Options.Collections` namespace contains extension methods to work with `IEnumerable<Option<T>>`:

| Method			  | Returns           | Description								    |
|---------------------|-------------------|---------------------------------------------|
| `Values()`          | `IEnumerable<T>`  | Get all non None values.                    |
| `WhereValues(...)`  | `IEnumerable<T>`  | Get all non None values with predicate      |
| `SelectValues(...)` | `IEnumerable<U>`  | Get all non None values and apply selector  |
| `CountValues()`     | `int`             | Get count of non None values                |
| `AnyValues()`       | `bool`            | Check if collection has any values          |
| `AllValues()`       | `bool`            | Check if collection contains only values    |
| `FirstOrNone()`     | `Option<T>`       | Get first non None value in collection      |
| `LastOrNone()`      | `Option<T>`       | Get last non None value in collection       |

Many of these methods come with predicate overloads to add additional filters, similar to their Linq equivalences.

For information per method, see the [Collections docs](Options.Collections.md).

### Collections of T

In addition to extensions on `IEnumerable<Option<T>>`, the `Toarnbeike.Options.Collections` namespace also contains extension methods that work on `IEnumerable<T>`:

| Method			| Returns       | Description								            |
|-------------------|---------------|-------------------------------------------------------|
| `FirstOrNone()`   | `Option<T>`   | Get the first not null instance or return Option.None |
| `LastOrNone()`    | `Option<T>`   | Get the last not null instance or return Option.None  |
| `SingleOrNone()`  | `Option<T>`   | Get the only instance or return Option.None           |

All these methods also come with predicate overloads to add additional filters.
These methods are also described in more detail in the [Collections docs](Options.Collections.md).

---

## Linq query syntax

Toarnbeike.Results supports C# LINQ query syntax for composing `Option<TValue>` pipelines using `from`, `select`, `let`, and `where`.

This provides an alternative, declarative way to compose `Bind`, `Map`, and `Check` operations while preserving the same missing value propagation semantics.

`None` values are automatically propagated and short-circuit execution of the query.

See the [LINQ extensions docs](Options.Linq.md) for details and examples.

---

## Conclusion

> Nulls are dangerous and exceptions are control-flow.
> Options make illegal states unrepresentable.

---