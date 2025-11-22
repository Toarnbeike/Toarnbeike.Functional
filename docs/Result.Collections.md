# Result collection extension methods

The result collection extension methods are defined in the `Toarnbeike.Results.Collections` namespace.

## Overview

The following collection extension methods are defined:

| Method                         | Applies To                                  | Returns                                   | Description                                                                                      |
|--------------------------------|---------------------------------------------|-------------------------------------------|--------------------------------------------------------------------------------------------------|
| [`Sequence(...)`](#sequence)   | `IEnumerable<Result<T>>`                    | `Result<IEnumerable<T>>`                  | Fail-fast: first failure encountered or a collection of all success values.                      |
| [`Traverse(...)`](#traverse)   | `IEnumerable<T>`                            | `Result<IEnumerable<T>>`                  | Map each item using a function returning `Result<T>`; fail-fast aggregation.                     |
|                                | `IEnumerable<Task<T>>`                      | `Task<Result<IEnumerable<T>>>`            | Async input items variant.                                                                       |
| [`Aggregate(...)`](#aggregate) | `IEnumerable<Result>`                       | `Result`                                   | Combine results; collect all failures into `AggregateFailure`.                                   |
|                                | `IEnumerable<Result<T>>`                    | `Result<IEnumerable<T>>`                  | Collect successes and aggregate failures.                                                        |
| [`Partition()`](#partition)    | `IEnumerable<Result<T>>`                    | `(IEnumerable<T>, IEnumerable<Failure>)`  | Split into successes and failures.                                                                |
| [`SuccessValues()`](#successvalues) | `IEnumerable<Result<T>>`                | `IEnumerable<T>`                          | Extract only successful values.                                                                  |
| [`Failures()`](#failures)      | `IEnumerable<Result>`/`IEnumerable<Result<T>>` | `IEnumerable<Failure>`                  | Extract only failures.                                                                           |

Async variants are available for `IEnumerable<Task<Result>>` and `IEnumerable<Task<Result<T>>>`.

---

## Sequence

Convert a sequence of results into a single result. If any item is a failure, return that failure; otherwise return all values.

```csharp
public Result<IEnumerable<TValue>> Sequence<TValue>(this IEnumerable<Result<TValue>> results)
public Task<Result<IEnumerable<TValue>>> Sequence<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
```

---

## Traverse

Apply a function per element that returns a result. Short-circuits on first failure.

```csharp
// For IEnumerable<TValue>
public Result<IEnumerable<TValue>> Traverse<TValue>(Func<TValue, Result<TValue>> bind)
public Task<Result<IEnumerable<TValue>>> TraverseAsync<TValue>(Func<TValue, Task<Result<TValue>>> bindAsync)

// For IEnumerable<Task<TValue>>
public Task<Result<IEnumerable<TValue>>> Traverse<TValue>(Func<TValue, Result<TValue>> bind)
public Task<Result<IEnumerable<TValue>>> TraverseAsync<TValue>(Func<TValue, Task<Result<TValue>>> bindAsync)
```

---

## Aggregate

Aggregate over a sequence of results, collecting all failures into a single `AggregateFailure` while preserving all successes.

```csharp
public Result Aggregate(this IEnumerable<Result> results)
public Result<IEnumerable<TValue>> Aggregate<TValue>(this IEnumerable<Result<TValue>> results)
public Task<Result> Aggregate(this IEnumerable<Task<Result>> resultTasks)
public Task<Result<IEnumerable<TValue>>> Aggregate<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
```

---

## Partition

Split results into successes and failures.

```csharp
public (IEnumerable<TValue> Successes, IEnumerable<Failure> Failures) Partition<TValue>(this IEnumerable<Result<TValue>> results)
public Task<(IEnumerable<TValue> Successes, IEnumerable<Failure> Failures)> Partition<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
```

---

## SuccessValues

Extract only the values from successful results.

```csharp
public IEnumerable<TValue> SuccessValues<TValue>(this IEnumerable<Result<TValue>> results)
public Task<IEnumerable<TValue>> SuccessValues<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
```

---

## Failures

Extract only failures from a sequence of results.

```csharp
public IEnumerable<Failure> Failures(this IEnumerable<Result> results)
public IEnumerable<Failure> Failures<TValue>(this IEnumerable<Result<TValue>> results)
public Task<IEnumerable<Failure>> Failures(this IEnumerable<Task<Result>> resultTasks)
public Task<IEnumerable<Failure>> Failures<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
```

---
