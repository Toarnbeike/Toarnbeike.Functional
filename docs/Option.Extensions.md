# Option extension methods

The option extension methods are defined in the `Toarnbeike.Options.Extensions` namespace.

## Overview

The following extension methods are defined:

| Method                        | Description                                  |
|-------------------------------|----------------------------------------------|
| [`AsNullable()`](#asnullable) | Convert to nullable                          |
| [`AsOption()`](#asoption)     | Convert from nullable                        |
| [`Map(...)`](#map)            | Transforms the inner value                   |
| [`Bind(...)`](#bind)          | Chain operations returning `Option<T>`       |
| [`Check(...)`](#check)        | Filter by predicate                          |
| [`Match(...)`](#match)        | Pattern match: Some/ None                    |
| [`Reduce(...)`](#reduce)      | Fallback to a value if empty                 |
| [`OrElse(...)`](#orelse)      | Return current option or fallback if `None`  |
| [`Tap(...)`](#tap)            | Execute side-effect on value                 |
| [`TapIfNone()`](#tapifnone)   | Execute side-effect when empty               |

---

### AsNullable
Convert an option back to a nullable value.
Mostly used for ORM mapping like EF Core that expect nullable values.

```csharp
public T? AsNullable<T>() where T : class {}
public T? AsNullableValue<T>() where T : struct {}
```
AsNullable supports async overloads for `Task<Option<TValue>>`.

---

### AsOption
Convert a nullable value to an option.
If the value is null, the resulting option will be `None`.

```csharp
public Option<T> AsOption<T>(this T? value) {}
```

---

### Map
Projects the value of an `Option<TIn>` to a new `Option<TOut>` by applying the provided mapping function.
If the original `Option<TIn>` is `None`, the resulting `Option<TOut>` is also `None`.
If the original `Option<TIn>` is `Some(TIn)`, the mapping function is applied to the value.

```csharp
public Option<TOut> Map<TOut>(Func<TIn, TOut?> map)
```

Map supports async overloads, both for the `map` function and a `Task<Option<TIn>>`.

---

### Bind
Projects the value of an `Option<TIn>` to a new `Option<TOut>` by applying the provided binding function.
If the original `Option<TIn>` is `None`, the resulting `Option<TOut>` is also `None`.
If the original `Option<TIn>` is `Some(TIn)`, the binding function is applied to the value.

```csharp
public Option<TOut> Bind<TOut>(Func<TIn, Option<TOut>> bind) {}
```

Bind supports async overloads, both for the `bind` function and a `Task<Option<TIn>>`.

---

### Check
Apply a predicate to the option and only retain the value if the current value matches the predicate.
If the original `Option<TValue>` is `None`, the resulting `Option<TOut>` is also `None`.
If the original `Option<TValue>` is `Some(TValue)`, the predicate is applied, and the value is retained if it matches the predicate.

```csharp
public Option<TValue> Check(Func<TValue, bool> predicate) {}
```

Check supports async overloads, both for the `predicate` function and a `Task<Option<TValue>>`.

---

### Match

Match the method to generate the `TOut` depending on whether this option has a value or not.
If the original `Option<TValue>` is a `None`, then none value is returned,
If the original `Option<TValue>` is a `Some(TValue)`, then the some mapping is invoked.

```csharp
public TOut Match<TOut>(Func<TIn, TOut> whenSome, Func<TOut> whenNone) {}
```

Match supports async overloads, both for the `whenSome` and `whenNone` (as `Task`) and a `Task<Option<TValue>>`.

---

### OrElse

Replace a `None` value with a fallback value.

```csharp
public Option<TValue> OrElse(TValue alternative) {}
public Option<TValue> OrElse(Func<TValue> alternative) {}
```

OrElse supports async overloads, both for the `alternative` (as `Task`) and a `Task<Option<TValue>>`.

---

### Reduce

Replace a `None` value with a fallback value and return the result.

```csharp
public TValue Reduce(TValue alternative) {}
public TValue Reduce(Func<TValue> alternative) {}
```

Reduce supports async overloads, both for the `alternative` (as `Task`) and a `Task<Option<TValue>>`.

---

### Tap

Executes the specified action if the current `Option<TValue>` is `Some(TValue)`.
Returns the current `Option<TValue>` for further chaining.

```csharp
public Option<TValue> Tap(Action<TValue> onSome) {}
```

Tap supports async overloads, both for the `onSome` (as `Task`) and a `Task<Option<TValue>>`.

---

### TapNone

Executes the specified action if the current `Option<TValue>` is `None`.
Returns the current `Option<TValue>` for further chaining.

```csharp
public Option<TValue> TapNone(Action onNone) {}
```

Tap supports async overloads, both for the `onNone` (as `Task`) and a `Task<Option<TValue>>`.

---