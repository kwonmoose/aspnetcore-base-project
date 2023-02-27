namespace Domain.Enums;

public static class ServerEnvironmentExtensionMethod
{
    public static bool IsLocal(this ServerEnvironment source)
    {
        return source == ServerEnvironment.Local;
    }
    
    public static bool IsDevelopment(this ServerEnvironment source)
    {
        return source == ServerEnvironment.Development;
    }
    
    public static bool IsStaging(this ServerEnvironment source)
    {
        return source == ServerEnvironment.Staging;
    }
    
    public static bool IsProduction(this ServerEnvironment source)
    {
        return source == ServerEnvironment.Production;
    }
}