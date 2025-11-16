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