namespace WebServer.Domain.Abstractions.Errors;

public class Error : IEquatable<Error>
{
    public static readonly Error None = null;
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    public static readonly Error Unauthorized = new("Error.Unauthorized", "Unauthorized access.");
    public static readonly Error NotFound = new("Error.NotFound", "The requested resource was not found.");
    public static readonly Error Forbidden = new("Error.Forbidden", "Access to this resource is forbidden.");
    public static readonly Error BadRequest = new("Error.BadRequest", "The request was invalid or cannot be served.");
    public static readonly Error InvalidClientIp = new("Error.InvalidClientIp", "Client IP address is not available.");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => Code;
}
