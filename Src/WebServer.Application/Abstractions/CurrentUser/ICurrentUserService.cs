namespace WebServer.Application.Abstractions.CurrentUser;

public interface ICurrentUserService
{
    Guid UserId { get; }
}
