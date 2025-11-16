# Option collection extension methods

The option collection extension methods are defined in the `Toarnbeike.Option.Collection` namespace.

## Overview

The following collection extension methods are defined:

| Method                          | Returns               | Description                      |
|---------------------------------|-----------------------|----------------------------------|
| [`Sequence()`](#sequence)       | `IEnumerable<TValue>` | Filter all non-empty values      |
| [`CountValues()`](#countvalues) | `int`                 | Count all non-empty values       |
| [`AnyValues()`](#anyvalues)     | `bool`                | Check if any value is present    |
| [`AllValues()`](#allvalues)     | `bool`                | Check if all values are present  |
| [`FirstOrNone()`](#firstornone) | `TValue`              | Get the first non-None value     |
| [`LastOrNone()`](#lastornone)   | `TValue`              | Get the last non-None value      |

---

## Sequence

Filter all non-None values from the collection.
Contains overloads to filter by predicate and to select a value.

```csharp
public IEnumerable<TValue> Sequence() {}
public IEnumerable<TValue> Sequence(Func<TValue, bool> predicate) {}
public IEnumerable<TResult> Sequence<TResult>(Func<TValue, TResult> selector) {}
```

---

## CountValues

Count all non-None values in the collection.
Contains an overload to filter by predicate.

```csharp
public int CountValues() {}
public int CountValues(Func<TValue, bool> predicate) {}
```

---

## AnyValues

Check if any value is present in the collection.
Contains an overload to filter by predicate.

```csharp
public bool AnyValues() {}
public bool AnyValues(Func<TValue, bool> predicate> {}
```

---

## AllValues

Check if all values are present in the collection.
Contains an overload to filter by predicate.

```csharp
public bool AllValues() {}
public bool AllValues(Func<TValue, bool> predicate) {}
```

---

## FirstOrNone

Get the first non-None value from the collection.
Return `None` if the collection is empty or contains only `None` values.
Contains an overload to filter by predicate.

```csharp
public Option<TValue> FirstOrNone() {}
public Option<TValue> FirstOrNone(Func<TValue, bool> predicate) {}
```

These methods are also available on `IEnumerable<TValue>`, returning an `None` option if the sequence is empty.

---

## LastOrNone

Get the last non-None value from the collection.
Return `None` if the collection is empty or contains only `None` values.
Contains an overload to filter by predicate.

```csharp
public Option<TValue> LastOrNone() {}
public Option<TValue> LastOrNone(Func<TValue, bool> predicate) {}
```

These methods are also available on `IEnumerable<TValue>`, returning an `None` option if the sequence is empty.

---