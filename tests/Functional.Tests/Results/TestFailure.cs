namespace Toarnbeike.Results;

public record TestFailure(string Code, string Message) : Failure(Code, Message);