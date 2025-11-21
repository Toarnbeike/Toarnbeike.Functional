namespace Toarnbeike;

/// <summary>
/// Exception thrown when an assertion in the functional testing framework fails.
/// </summary>
public class FunctionalAssertException(string message) : Exception(message);