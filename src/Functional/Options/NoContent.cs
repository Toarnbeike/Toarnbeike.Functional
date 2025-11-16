using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Options;

/// <summary>
/// Marker type representing the absence of a value in <see cref="Option{TValue}"/>.
/// </summary>
/// <remarks>
/// Used internally to support conversion from <see cref="Option.None"/> to <see cref="Option{TValue}.None()"/>.
/// Not intended for use outside the <see cref="Toarnbeike.Options"/> namespace.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
[ExcludeFromCodeCoverage(Justification = "Not publicly used.")]
// ReSharper disable once ClassNeverInstantiated.Global
public sealed record NoContent;