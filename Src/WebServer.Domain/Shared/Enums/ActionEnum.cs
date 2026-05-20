namespace WebServer.Domain.Shared.Enums;

[Flags]
public enum ActionEnum : short
{
    Read = 1 << 0,      // 1
    Create = 1 << 1,    // 2
    Update = 1 << 2,    // 4
    Delete = 1 << 3,    // 8
    Export = 1 << 4,    // 16
    Import = 1 << 5,    // 32
    All = Read | Create | Update | Delete | Export | Import // 63
}
