namespace WebServer.Domain.Abstractions.Behaviors;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError",
        "A validation problem occurred.");
    Error[] Errors { get; }
}
