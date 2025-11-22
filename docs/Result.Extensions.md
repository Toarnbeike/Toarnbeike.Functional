# Result extension methods

The result extension methods are defined in the `Toarnbeike.Results.Extensions` namespace.
These methods adopt functional composition patterns, short-circuiting on the first failure.

## Overview

The following extension methods are defined:

| Method                               | Applies To                | Returns                    | Description                                                       |
|--------------------------------------|---------------------------|----------------------------|-------------------------------------------------------------------|
| [`Bind(...)`](#bind)                 | `Result`, `Result<T>`     | `Result<TOut>`             | Chain operations that return a `Result`.                          |
| [`Map(...)`](#map)                   | `Result<T>`               | `Result<TOut>`             | Transform the success value.                                      |
| [`Check(...)`](#check)               | `Result<T>`               | `Result<T>`                | Keep success only if predicate holds; otherwise produce failure.  |
| [`Match(...)`](#match)               | `Result`, `Result<T>`     | `TOut`                     | Convert a result by mapping success and failure separately.       |
| [`Tap(...)`](#tap)                   | `Result`, `Result<T>`     | same as input              | Execute side-effect on success; pass result through unchanged.    |
| [`TapFailure(...)`](#tapfailure)     | `Result`, `Result<T>`     | same as input              | Execute side-effect on failure; pass result through unchanged.    |
| [`WithValue(...)`](#withvalue)       | `Result`                  | `Result<T>`                | Supply a value for a successful non-generic result.               |
| [`Zip(...)`](#zip)                   | `Result<T1>`              | `Result<(T1,T2)>/TResult`  | Combine two successful results into one.                          |
| [`ToOption()`](#tooption)            | `Result<T>`               | `Option<T>`                | Convert to `Option<T>` (drops failure data).                      |
| [`GetValueOrThrow(...)`](#unsafe)    | `Result<T>`               | `T`                        | UNSAFE: Get value or throw if failure.                            |
| [`GetFailureOrThrow(...)`](#unsafe)  | `Result`/`Result<T>`      | `Failure`                  | UNSAFE: Get failure or throw if success.                          |

---

### Bind

Projects a successful result into another result by applying a provided function.
Short-circuits by returning the existing failure when not successful.

```csharp
// Non-generic -> generic
public Result<TOut> Bind<TOut>(Func<Result<TOut>> bind)
public Task<Result<TOut>> BindAsync<TOut>(Func<Task<Result<TOut>>> bindAsync)

// Generic -> generic
public Result<TOut> Bind<TOut>(Func<TIn, Result<TOut>> bind)
public Task<Result<TOut>> BindAsync<TOut>(Func<TIn, Task<Result<TOut>>> bindAsync)

// Task<Result<...>> variants provided as well
```

---

### Map

Transforms the value of a successful `Result<TIn>` using the provided mapping function.
If the original is a failure, the failure is preserved.

```csharp
public Result<TOut> Map<TOut>(Func<TIn, TOut> map)
public Task<Result<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut>> map)

// Task<Result<TIn>> variants provided as well
```

---

### Check

Enforce an additional invariant on a successful result. If the predicate returns `false`,
the result is transformed into a failure provided by `onFailure`.

```csharp
public Result<TValue> Check(Func<TValue, bool> predicate, Func<Failure> onFailure)
public Task<Result<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate, Func<Failure> onFailure)

// Task<Result<TValue>> variants provided as well
```

---

### Match

Convert a result into a value by providing mappings for both success and failure.

```csharp
// Non-generic
public TOut Match<TOut>(Func<TOut> onSuccess, Func<Failure, TOut> onFailure)
public Task<TOut> MatchAsync<TOut>(Func<Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)

// Generic
public TOut Match<TOut>(Func<TValue, TOut> onSuccess, Func<Failure, TOut> onFailure)
public Task<TOut> MatchAsync<TOut>(Func<TValue, Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)

// Task<Result<...>> variants provided as well
```

---

### Tap

Execute a side-effect only when the result is successful. The original result is returned unchanged.

```csharp
// Non-generic
public Result Tap(Action onSuccess)
public Task<Result> TapAsync(Func<Task> onSuccess)

// Generic
public Result<TValue> Tap(Action<TValue> onSuccess)
public Task<Result<TValue>> TapAsync(Func<TValue, Task> onSuccess)

// Task<Result<...>> variants provided as well
```

---

### TapFailure

Execute a side-effect only when the result is a failure. The original result is returned unchanged.

```csharp
// Non-generic
public Result TapFailure(Action<Failure> onFailure)
public Task<Result> TapFailureAsync(Func<Failure, Task> onFailure)

// Generic
public Result<TValue> TapFailure(Action<Failure> onFailure)
public Task<Result<TValue>> TapFailureAsync(Func<Failure, Task> onFailure)

// Task<Result<...>> variants provided as well
```

---

### WithValue

Turn a non-generic success into a typed success by supplying a value (or value factory).
Failures are propagated unchanged.

```csharp
public Result<TValue> WithValue<TValue>(TValue value)
public Result<TValue> WithValue<TValue>(Func<TValue> valueFunc)
public Task<Result<TValue>> WithValueAsync<TValue>(Func<Task<TValue>> valueFunc)

// Task<Result> variants provided as well
```

---

### Zip

Combine two successful results into a single result. Supports both tuple output and a projector
to create a named/typed result. Short-circuits on the first failure.

```csharp
public Result<(T1, T2)> Zip<T2>(Func<T1, Result<T2>> second)
public Result<TResult> Zip<T2, TResult>(Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector)
public Task<Result<(T1, T2)>> ZipAsync<T2>(Func<T1, Task<Result<T2>>> secondTask)
public Task<Result<TResult>> ZipAsync<T2, TResult>(Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector)

// Task<Result<T1>> variants provided as well
```

---

### ToOption

Convert a `Result<T>` to an `Option<T>`. Note: this intentionally drops the failure details.

```csharp
public Option<T> ToOption<T>(this Result<T> result)
public Task<Option<T>> ToOption<T>(this Task<Result<T>> resultTask)
```

---

### Unsafe

Methods under `Toarnbeike.Results.Extensions.Unsafe` that throw on unexpected state.
Use sparingly, typically after filtering in collections.

```csharp
public T GetValueOrThrow<T>(this Result<T> result)
public Task<T> GetValueOrThrow<T>(this Task<Result<T>> resultTask)

public Failure GetFailureOrThrow(this Result result)
public Failure GetFailureOrThrow<T>(this Result<T> result)
public Task<Failure> GetFailureOrThrow(this Task<Result> resultTask)
public Task<Failure> GetFailureOrThrow<T>(this Task<Result<T>> resultTask)
```

---
