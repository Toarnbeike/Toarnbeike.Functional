# Either collection extension methods

The either collection extension methods are defined in the `Toarnbeike.Eithers.Collection` namespace.
These methods are, in typical FP fashion, using the right path as the success path, to enable chaining of operations.

## Overview

The following collection extension methods are defined:

| Method           | Applies To                           | Returns                                     | Description                                                                               |
|------------------|--------------------------------------|---------------------------------------------|-------------------------------------------------------------------------------------------|
| `Traverse(...)`  | `IEnumerable<TValue>`                | `Either<TLeft, IEnumerable<TRight>>`        | Applies a function per element, returning a collection of `TRight`, or the first `TLeft`. |
| `AnyLeft(...)`   | `IEnumerable<Either<TLeft, TRight>>` | `bool`                                      | Returns true if any of the Eithers are in the left state.                                 |
| `AnyRight(...)`  | `IEnumerable<Either<TLeft, TRight>>` | `bool`                                      | Returns true if any of the Eithers are in the right state.                                |
| `CountLeft(...)` | `IEnumerable<Either<TLeft, TRight>>` | `int`                                       | The number of the Eithers in the left state.                                              |
| `CountLeft(...)` | `IEnumerable<Either<TLeft, TRight>>` | `int`                                       | The number of the Eithers in the right state.                                             |
| `Lefts(...)`     | `IEnumerable<Either<TLeft, TRight>>` | `IEnumerable<TLeft>`                        | Lazely collect all the Eithers in the left state.                                         |
| `Rights(...)`    | `IEnumerable<Either<TLeft, TRight>>` | `IEnumerable<TRight>`                       | Lazely collect all the Eithers in the right state.                                        |
| `Sequence(...)`  | `IEnumerable<Either<TLeft, TRight>>` | `Either<TLeft, IEnumerable<TRight>>`        | Flattens a collection of Eithers into a collection of `TRight`, or the first `TLeft`.     |
| `Partition(...)` | `IEnumerable<Either<TLeft, TRight>>` | `(IEnumerable<TRight>, IEnumerable<TLeft>)` | Splits the collection into two collections by type.                                       |

---

Specific descriptions of each of these methods will follow.