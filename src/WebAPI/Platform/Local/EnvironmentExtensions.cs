namespace Apb.WebAPI.Platform.Local;

internal static class EnvironmentExtensions
{
    internal static bool IsLocalEnv(this IWebHostEnvironment environment) =>
        environment.IsEnvironment("Local");
}
