# Toarnbeike.Options.Extensions 

This namespace provides extension methods for working with `Option` types, enabling functional programming patterns such as mapping, binding and side effects. 

These extensions are inspired by functional programming paradigms and are designed to work seamlessly with the immutable, allocation-free `Option` types provided by the `Toarnbeike.Options` library. 

--- 

## Overview 

| Method			             | Returns       | Description								     |
|--------------------------------|---------------|-----------------------------------------------|
| [`AsNullable()`](#asnullable)	 | `T?`          | Convert to nullable						     |
| [`AsOption()`](#asoption)		 | `Option<T>`   | Convert from nullable						 |
| [`Map(...)`](#map)			 | `Option<U>`   | Transforms the inner value 			         |
| [`Bind(...)`](#bind)			 | `Option<U>`   | Chain operations returning `Option<T>`        |
| [`Check(...)`](#check)		 | `Option<T>`   | Filter by predicate					         |
| [`Match(...)`](#match)	     | `U`           | Pattern match: Some/ None                     |
| [`Reduce(...)`](#reduce)	     | `T`           | Fallback to a value if empty      		     |
| [`Tap(...)`](#tap)			 | `Option<T>`   | Execute side-effect on value				     |
| [`TapIfNone()`](#tapifnone)	 | `Option<T>`   | Execute side-effect when empty                |

### Choosing the right method

- Use [`AsNullable`](#asnullable) when you want to use an Option in a context that does not allow Options, such as EF Core.
- Use [`AsOption`](#asoption) if you received a nullable value from somewhere.
- Use [`Map`](#map) when transforming the value of an Option, while keeping the result as an Option.
- Use [`Bind`](#bind) when transforming the value of an Option, to a result that can also be an Option itself.
- Use [`Check`](#check) to verify if a condition is true for the given value, and return Option.None if it is not.
- Use [`Reduce`](#reduce) to finish using the Option and return the value, or an alternative if the value was `None`.
- Use [`Tap`](#tap) for side effects on the value, while retaining the option for further chaining.
- Use [`TapIfNone`](#tapifnone) for side effects if the value is `None`, while retaining the option for further chaining.
 
--- 

## AsNullable 

The `AsNullable` extension method allows you to convert the `Option` to a C# `T?`. 
This is used for contexts where `Option` is not allowed, such as e.g. EF Core.
`AsNullable` is a terminating operation: it return an `T?` that does not allow further chaining.

```csharp
Option<User> user = GetById(123);
User? nullableUser = user.AsNullable();
```

Due to C#'s implementation of nullable, separate extensions are required for value types, these are `AsNullableValue()`:

```csharp
Option<int> input = GetUserInput();
int? nullableInput = input.AsNullableValue();
```

Overloads are available for async `Task<Option<T>>` versions. 

--- 

## AsOption

The `AsOption` extension method converts an existing nullable value to an `Option`.
`AsOption` is a creating operation: it returns an `Option` that allows for further chaining.

```csharp
string? nullableFirstName = GetFirstNameFromDatabaseForId(123);
Option<string> firstName = nullableFirstName.AsOption(); 
```

An overload is available for constructing an `Task<Option<T>>` from a `Task<T?>`.

--- 

## Map 

The `Map` extension method allows you to modify the value inside the `Option` by providing a mapping function from the original to the new value. 
It returns a new `Option` with either the transformed value, or `None` if the original value was `None`.
`Map` is a chaining operation: it return an `Option` that allows for further chaining.

```csharp
Option<User> user = GetById(123);
Option<string> firstName = user.Map(value => value.FirstName);
```

Overloads are available for async mapping and `Task<Option<T>>` versions. 

--- 

## Bind 

The `Bind` extension method allows you to modify the value inside the `Option` by providing a binding function from the original to a new Option.
This means that the result of the binding function can also be `None`, even if the original value was present.
If the original value was `None`, the result is still `None`.
`Bind` is a chaining operation: it return an `Option` that allows for further chaining.

```csharp
Option<User> user = GetById(123);
Option<string> firstName = user.Bind(value => string.IsNullOrEmpty(value.FirstName) ? Option.None : value.FirstName);
```

Overloads are available for async binding and `Task<Option<T>>` versions. 

--- 

## Check 

The `Check` extension method allows you to verify a condition on the value inside the `Option` and changing the value to `None` if the predicate fails.
If the original value was `None`, the result is still `None`.
`Check` is a chaining operation: it return an `Option` that allows for further chaining.

```csharp
Option<User> user = GetById(123);
Option<User> activeUser = user.Check(value => value.Status == Status.Active);
```

Overloads are available for async checking and `Task<Option<T>>` versions. 

--- 

## Match 

The `Match` extension method allows you to create a result from either the `Some` branch or the `None` branch.
`Match` is a terminating operation: it returns a result that does not allow further chaining.

```csharp
Option<User> user = GetById(123);
string result = user.Match(
  some => CreateUserLabel(user),
  () => "User not found"
);
```

Overloads are available for async matching and `Task<Option<T>>` versions. 

--- 

## Reduce

The `Reduce` extension method allows you to substitute a value if none is present. Contrary to `OrElse`, this terminates the `Option`.
`Reduce` is a terminating operation: it returns a result that does not allow further chaining.

```csharp
Option<User> user = GetById(123);
string name = user.Map(value => $"{value.FirstName} {value.LastName}").Reduce("User not found");
```

Overloads are available for an async reduce `Task` and `Task<Option<T>>` versions. 

--- 

## Tap

The `Tap` extension method allows to perform a side effect on the `Option<T>`, while returning the original `Option`.
`Tap` is a chaining operation: it return an `Option` that allows for further chaining.

```csharp
Option<User> user = GetById(123)
	.Tap(value => logger.LogInformation($"Found user with first name {value.FirstName}));
```

Overloads are available for an async side effects and `Task<Option<T>>` versions. 

---

## TapIfNone

The `TapIfNone` extension method allows to perform a side effect on the `Option<T>` if it is in the `None` state, while returning the original `Option`.
`TapIfNone` is a chaining operation: it return an `Option` that allows for further chaining.

```csharp
Option<User> user = GetById(123)
	.TapIfNone(() => logger.LogWarning("User with Id 123 not found."));
```

Overloads are available for an async side effects and `Task<Option<T>>` versions. 

---