# Option

`Option<TValue>` represent a value that can either be a value `Some<TValue>` or nothing `None`.
It improves code clarity and safety by making the absence of a value explicit, replacing null checks and exceptions with a functional approach.

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
An option can represent either a value (`Some`) or no value (`None`):

```csharp
var option1 = Option<int>.Some(1);
var option2 = Option.Some(1);        // Type inferred
var option3 = Option<int>.None();

Option<int> option4 = 1;             // Implicit conversion
Option<int> option5 = Option.None;
```

Use `TryGetValue` to safely retreive the value from an option:
```csharp
if (option1.TryGetValue(out var value))
{
    Console.WriteLine(value); // Outputs: 1
}
```

---

## Extensions
The `Toarnbeike.Options.Extensions` namespace contains extension methods for `Option<TValue>`:

| Method            | Description                                  |
|-------------------|----------------------------------------------|
| `AsNullable(...)` | Convert to nullable                          |
| `AsOption(...)`   | Convert from nullable                        |
| `Map(...)`        | Transforms the inner value                   |
| `Bind(...)`       | Chain operations returning `Option<T>`       |
| `Check(...)`      | Filter by predicate                          |
| `Match(...)`      | Pattern match: Some/ None                    |
| `Reduce(...)`     | Fallback to a value if empty                 |
| `OrElse(...)`     | Return current option or fallback if `None`  |
| `Tap(...)`        | Execute side-effect on value                 |
| `TapIfNone()`     | Execute side-effect when empty               |

For more details regarding these methods, see the [Extensions details](Option.Extensions.md) readme.

---

## Collections
The `Toarnbeike.Options.Collections` namespace contains extension methods for `IEnumerable<Option<TValue>>`:

| Method          | Returns               | Description                      |
|-----------------|-----------------------|----------------------------------|
| `Sequence()`    | `IEnumerable<TValue>` | Filter all non-empty values      |
| `CountValues()` | `int`                 | Count all non-empty values       |
| `AnyValues()`   | `bool`                | Check if any value is present    |
| `AllValues()`   | `bool`                | Check if all values are present  |
| `FirstOrNone()` | `TValue`              | Get the first non-None value     |
| `LastOrNone()`  | `TValue`              | Get the last non-None value      |

For more details regarding these methods, see the [Collections details](Option.Collections.md) readme.

---

## LINQ support

The `Toarnbeike.Option.Linq` namespace contains extension methods for working with LINQ queries:
- Select = Map
- SelectMany = Bind

```csharp
Option<string> GetName(int id) =>
    id == 1 ? "Alice" : Option.None;

Option<int> GetAge(string name) =>
    name == "Alice" ? 30 : Option.None;

var result =
    from id in SomeInt(1)
    from name in GetName(id)
    from age in GetAge(name)
    select $"{name} is {age}";

result.ShouldBeSomeWithValue("Alice is 30");
```

---

## Test extensions

The `Toarnbeike.Options.TestExtensions` namespace contains extension methods for asserting on Options.
These extensions are compatible with any test framework. See [TestExtensions](TestExtensions.md) for more details.

---

## Integrations

Upon expanding the library, integrations with other parts of the library will be added here.

---

## Best practices

- Use `Option` instead of `Nullable<T>` where possible.
- Use `Result` when additional context regarding the failure can be provided.
- Use `Option.Try` methods when the failure can be expected.

---