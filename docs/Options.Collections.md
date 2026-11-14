# Toarnbeike.Options.Collections

This namespace provides extension methods for working with collections in combination with `Option<T>`. 
They allow safely extracting values from sequences and converting collection-based results into `Option` representations without relying on `null` or exceptions.

These extensions are designed to complement the core `Option<T>` API and follow the same functional principles: explicit absence, immutability, and predictable control flow.

## Overview

### IEnumerable<Option<T>>`

| Method							    | Returns           | Description								  |
|---------------------------------------|-------------------|---------------------------------------------|
| [`Values()`](#values)					| `IEnumerable<T>`  | Get all non None values.                    |
| [`WhereValues(...)`](#wherevalues)	| `IEnumerable<T>`  | Get all non None values with predicate      |
| [`SelectValues(...)`](#selectvalues)	| `IEnumerable<U>`  | Get all non None values and apply selector  |
| [`CountValues()`](#countvalues)		| `int`             | Get count of non None values                |
| [`AnyValues()`](#anyvalues)			| `bool`            | Check if collection has any values          |
| [`AllValues()`](#allvalues)			| `bool`            | Check if collection contains only values    |
| [`FirstOrNone()`](#firstornone)		| `Option<T>`       | Get first non None value in collection      |
| [`LastOrNone()`](#lastornone)			| `Option<T>`       | Get last non None value in collection       |
| [`SingleOrNone()`](#singgleornone)    | `Option<T>`       | Get the only non None instance			  |

The last three methods, [`FirstOrNone()`](#firstornone), [`LastOrNone()`](#lastornone) and [`SingleOrNone()`](#singgleornone) are also available for regular `IEnumerable<T>` collections.
There these methods filter away any `null` values.

---

## Values

The `Values` extension removes all `None` instances from the collection, and unwraps the contained values from `Some`, returning an `IEnumerable<T>`.

This is useful when a sequence contains optional values and only the actual values are relevant.

```csharp
IEnumerable<Option<int>> measurements = GetAllMeasurements();
IEnumerable<int> succeededMeasurement = measurements.Values();
```

---

### WhereValues

The `WhereValues` extension method combines the `Where` functionality of a regular `IEnumerable<T>` with the `Values` functionality in that it removes `None` instances and unwraps the contained `Some` values.

```csharp
IEnumerable<Option<int>> measurements = GetAllMeasurements();
IEnumerable<int> measurementsAbove20 = measurements.WhereValues(value => value > 20);
```

---

### SelectValues

The `SelectValues` extension method combines the `Select` functionality of a regular `IEnumerable<T>` with the `Values` functionality in that it removes `None` instances and unwraps the contained `Some` values.

```csharp
IEnumerable<Option<int>> measurements = GetAllMeasurements();
IEnumerable<double> measurementsInInches = measurements.SelectValues(value => value * 0.394d);
```

---

### CountValues

The `CountValues` extension method counts the number of `Some` values in the collection.
This method accepts an overload for filtering specific instances, similar to regular `IEnumerable<T>` `Count(Func<T, bool> predicate)` method.

```csharp
IEnumerable<Option<int>> measurements = GetAllMeasurements();
int numberOfMeasurements = measurements.CountValues();
int numberOfMeasurementsAbove20 = measurements.CountValues(value => value > 20);
```

---

### AnyValues

The `AnyValues` extension method checks if any instance of the collection is `Some`.
This method accepts an overload for filtering specific instances, similar to regular `IEnumerable<T>` `Any(Func<T, bool> predicate)` method.

```csharp
IEnumerable<Option<int>> measurements = GetAllMeasurements();
bool anyMeasurements = measurements.AnyValues();
bool anyMeasurementsAbove20 = measurements.AnyValues(value => value > 20);
```

---

### AllValues

The `AllValues` extension method checks if all instance of the collection is `Some`.
This method accepts an overload for filtering specific instances, similar to regular `IEnumerable<T>` `All(Func<T, bool> predicate)` method.

```csharp
IEnumerable<Option<int>> measurements = GetAllMeasurements();
bool allMeasurements = measurements.AllValues();
bool allMeasurementsAbove20 = measurements.AllValues(value => value > 20);
```

---

### FirstOrNone

The `FirstOrNone` extension method returns the first element of a sequence, or `None ` if the sequence contains no elements.
This method is both applicable for regular `IEnumerable<T>`, where it skips any `null`, and for `IEnumerable<Option<T>>`, where it skips any 'None' values.

This method accepts an overload for filtering specific instances, similar to regular `IEnumerable<T>` `FirstOrDefault(Func<T, bool> predicate)` method.

```csharp
IEnumerable<int> nullableMeasurements = GetAllMeasurements();
Option<int> firstSomeMeasurement = nullableMeasurements.FirstOrNone();
Option<int> firstSomeMeasurementAbove20 = nullableMeasurements.FirstOrNone(value => value > 20);

IEnumerable<Option<int>> measurements = GetAllMeasurements();
firstSomeMeasurement = measurements.FirstOrNone();
firstSomeMeasurementAbove20 = measurements.FirstOrNone(value => value > 20);
```

---

### LastOrNone

The `LastOrNone` extension method returns the last element of a sequence, or `None ` if the sequence contains no elements.
This method is both applicable for regular `IEnumerable<T>`, where it skips any `null`, and for `IEnumerable<Option<T>>`, where it skips any 'None' values.

This method accepts an overload for filtering specific instances, similar to regular `IEnumerable<T>` `LastOrDefault(Func<T, bool> predicate)` method.

```csharp
IEnumerable<int> nullableMeasurements = GetAllMeasurements();
Option<int> lastSomeMeasurement = nullableMeasurements.LastOrNone();
Option<int> lastSomeMeasurementAbove20 = nullableMeasurements.LastOrNone(value => value > 20);

IEnumerable<Option<int>> measurements = GetAllMeasurements();
lastSomeMeasurement = measurements.LastOrNone();
lastSomeMeasurementAbove20 = measurements.LastOrNone(value => value > 20);
```

---

### SingleOrNone

The `LastOrNone` extension method returns the only element of a sequence that contains a value, or `None ` if the sequence contains no elements.
This method is both applicable for regular `IEnumerable<T>`, where it skips any `null`, and for `IEnumerable<Option<T>>`, where it skips any 'None' values.

This method accepts an overload for filtering specific instances, similar to regular `IEnumerable<T>` `SingleOrDefault(Func<T, bool> predicate)` method.
Similar to `SingleOrDefault`, it throws an exception when more than one instance is found.

```csharp
IEnumerable<Measurement>> nullableMeasurements = GetAllMeasurements();
Option<Measurement> measurementWithId123 = nullableMeasurements.SingleOrNone(value => value.Id == 123);

IEnumerable<Option<Measurement>> measurements = GetAllMeasurements();
measurementWithId123 = nullableMeasurements.SingleOrNone(value => value.Id == 123);
```

---