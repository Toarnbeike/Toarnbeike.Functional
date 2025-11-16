# Either extension methods

The either extension methods are defined in the `Toarnbeike.Eithers.Extensions` namespace.
These methods are, in typical FP fashion, using the right path as the success path, to enable chaining of operations.

## Overview

The following extension methods are defined:

| Method                            | Applies To | Returns                   | Description                                         |
|-----------------------------------|------------|---------------------------|-----------------------------------------------------|
| [`Bind(...)`](#bind)              | `TRight`   | `Either<TLeft, TRight2>`  | Chains operations returning `Either<TLeft, TOut>`   |
| [`Map(...)`](#map)                | `TRight`   | `Either<TLeft, TRight2>`  | Maps the value to a new value of type `TOut`        |
| [`Map(...)`](#map-bi-directional) | Both       | `Either<TLeft2, TRight2>` | Maps both operants at the same time                 |
| [`MapLeft(...)`](#mapleft)        | `TLeft`    | `Either<TLeft2, TRight>`  | Maps the left value to a new value of type `TLeft2` |
| [`Match(...)`](#match)            | Both       | `TOut`                    | Applies a function to both sides to return a `TOut` |
| [`Match(...)`](#match-actions)    | Both       | void                      | Applies an action to both sides, returning void.    |
| [`Swap(...)`](#swap)              | Both       | `Either<TRight,TLeft>`    | Swaps the left and right values.                    |
| [`Tap(...)`](#tap)                | `TRight`   | `Either<TLeft, TRight>`   | Execute side-effect on Right side.                  |
| [`TapLeft(...)`](#tapleft)        | `TLeft`    | `Either<TLeft, TRight>`   | Execute side-effect on Left side.                   |

---

### Bind
Projects the value of an `Either<TLeft, TRight>` to a new `Either<TLeft, TRight2>` by applying the provided binding function to the right value.
If the original `Either<TLeft, TRight>` is a `Right`, then the binding function is applied to the right value,
If the original `Either<TLeft, TRight>` is a `Left`, the original `TLeft` is returned.

```csharp
public Either<TLeft, TRight2> Bind<TRight2>(Func<TRight, Either<TLeft, TRight2>> bind) {}
```

Bind supports async overloads, both for the `bind` function and a `Task<Either<TLeft,TRight>>`.

---

### Map

Projects the right value of an `Either{TLeft, TRight>` to new values using the provided mapping function.
If the original `Either<TLeft, TRight>` is a `Right`, then the mapping function is applied to the right value,
If the original `Either<TLeft, TRight>` is a `Left`, the original `TLeft` is returned.

```csharp
public Either<TLeft, TRight2> Map<TRight2>(Func<TRight, TRight2> map) {}
```

Map supports async overloads, both for the `map` function and a `Task<Either<TLeft,TRight>>`.

---

### Map Bi-directional

Combines the [`Map`](#map) and [`MapLeft`](#mapleft) methods into a single method.

```csharp
public Either<TLeft2, TRight2> Map<TLeft2, TRight2>(
    Func<TLeft, TLeft2> mapL,
    Func<TRight, TRight2> mapR) {}
```

---

### MapLeft

Projects the left value of an `Either{TLeft, TRight>` to new values using the provided mapping function.
If the original `Either<TLeft, TRight>` is a `Left`, then the mapping function is applied to the left value,
If the original `Either<TLeft, TRight>` is a `Right`, the original `TRight` is returned.

```csharp
public Either<TLeft2, TRight> Map<TLeft2>(Func<TLeft, TLeft2> map) {}
```

MapLeft supports async overloads, both for the `map` function and a `Task<Either<TLeft,TRight>>`.

---

### Match

Converts the current `Either<TLeft, TRight}>` to an `TOut` by invoking the appropriate mappping depending on whether it contains a left or right value.
If the original `Either<TLeft, TRight>` is a `Left`, then the left mapping is invoked,
If the original `Either<TLeft, TRight>` is a `Right`, then the right mapping is invoked.

```csharp
public TOut Match<TOut>(Func<TLeft, TOut> onLeft, Func<TRight, TOut> onRight) {}
```

Match supports async overloads, both for the `onLeft` and `onRight` (as `Task`) and a `Task<Either<TLeft,TRight>>`.

---

### Match actions

Matches the current `Either<TLeft, TRight}>` by invoking the appropriate action depending on whether it contains a left or right value.
If the original `Either<TLeft, TRight>` is a `Left`, then the left action is invoked,
If the original `Either<TLeft, TRight>` is a `Right`, then the right action is invoked.

```csharp
public void Match(Action<TLeft> onLeft, Action<TRight> onRight) {}
```

Match supports async overloads, both for the `onLeft` and `onRight` (as `Task`) and a `Task<Either<TLeft, TRight>>`.

---

### Swap

Swaps the left and right values of an `Either<TLeft,TRight>`.

```csharp
public Either<TRight, TLeft> Swap() {}
```

Swap supports an async overload for a `Task<Either<TLeft, TRight>>`.

---

### Tap

Executes the specified action if the current `Either<TLeft, TRight}>` is a `Right`.
Returns the current `Either<TLeft,TRight>` for further chaining.

```csharp
public Either<TLeft, TRight> Tap(Action<TRight> onRight) {}
```

Tap supports async overloads, both for the `onRight` (as `Task`) and a `Task<Either<TLeft,TRight>>`.

---

### TapLeft

Executes the specified action if the current `Either<TLeft, TRight}>` is a `Left`.
Returns the current `Either<TLeft,TRight>` for further chaining.

```csharp
public Either<TLeft, TRight> TapLeft(Action<TRight> onLeft) {}
```

Tap supports async overloads, both for the `onLeft` (as `Task`) and a `Task<Either<TLeft,TRight>>`.

---