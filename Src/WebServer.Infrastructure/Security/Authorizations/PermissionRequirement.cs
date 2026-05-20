namespace WebServer.Infrastructure.Security.Authorizations;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Service { get; } = default!;
    public string Module { get; } = default!;
    public ActionEnum Action { get; }

    public PermissionRequirement(string service, string module, ActionEnum action)
    {
        Service = service;
        Module = module;
        Action = action;
    }
}
