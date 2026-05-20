namespace WebServer.Domain.Shared.Constants;

public sealed class Module
{
    public static readonly Module Auth = new("auth");
    public static readonly Module Role = new("role");
    public static readonly Module User = new("user");
    public static readonly Module Permission = new("permission");
    public static readonly Module Course = new("course");
    public string Value { get; }
    private Module(string value) => Value = value;
    public static readonly IReadOnlyList<Module> All =
    [
        Auth,
        Role,
        User,
        Permission,
        Course,
    ];
    public static readonly IReadOnlyList<string> AllValues = All.Select(m => m.Value).ToList();
}
