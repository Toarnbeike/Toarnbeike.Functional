# Toarnbeike.Functional
Functional library for c#. Either, Result, Option and Guard monads

## Either

`Either<TLeft, TRight>` represent a value of one of two possible types (a disjoint union).
Either is semantically neutral, meaning that it can be used to represent a value that is either a left or a right value.
Extension methods, in common FP style, treat the right path as the success path, and the left path as the failure path.
This enables binding and mapping operations to be chained together.
For explicit success and failure handling however, the `Result<TValue` is preferred.

See [Either](docs/Either.md) for more details.

## Option

`Option<TValue>` represent a value that can either be a value `Some<TValue>` or nothing `None`.
It improves code clarity and safety by making the absence of a value explicit, replacing null checks and exceptions with a functional approach.

See [Option](docs/Option.md) for more details.

