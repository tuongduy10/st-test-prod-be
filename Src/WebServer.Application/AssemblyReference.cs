[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("WebServer.Application.Tests")]

namespace WebServer.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
