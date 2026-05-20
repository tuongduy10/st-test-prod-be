namespace WebServer.Infrastructure.Security.Authorizations;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        try
        {
            var claims = httpContext.User.Claims;
            var isOwner = claims.Where(_ => _.Type == "roles").Select(_ => _.Value).ToList().Any(_ => _ == "owner");
            if (isOwner)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            var claimPermissions = claims.FirstOrDefault(_ => _.Type == "permissions")?.Value;
            if (string.IsNullOrEmpty(claimPermissions))
                context.Fail();

            // Parse the permissions claim
            Dictionary<string, Dictionary<string, PermissionData>>? userPermissions = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, PermissionData>>>(claimPermissions);

            var userPermission = (ActionEnum)userPermissions[requirement.Service][requirement.Module].Action;
            if ((userPermission & requirement.Action) == requirement.Action)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
        catch
        {
            context.Fail();
            return Task.CompletedTask;
        }
    }

    public class PermissionData
    {
        [JsonPropertyName("permissions")]
        public short Action { get; set; }

        [JsonPropertyName("data_access")]
        public string DataAccess { get; set; } = "own";
    }
}
