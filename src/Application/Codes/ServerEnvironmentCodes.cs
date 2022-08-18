namespace application.Codes;

/// <summary>
/// 서버 환경 이름 코드 목록
/// </summary>
public static class ServerEnvironmentCodes
{
    private static readonly string _serverEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    /// <summary>
    /// 개발자 개인 PC 환경 
    /// </summary>
    public const string Local = "Local";
        
    /// <summary>
    /// 개발 환경
    /// </summary>
    public const string Development = "Development";
        
    /// <summary>
    /// QA 환경
    /// </summary>
    public const string QA = "QA";
        
    /// <summary>
    /// 스테이징 환경
    /// </summary>
    public const string Staging = "Staging";
        
    /// <summary>
    /// 서비스 운영 환경
    /// </summary>
    public const string Production = "Production";

    public static bool IsLocal() => _serverEnvironment.Equals(Local);
    public static bool IsDevelopment() => _serverEnvironment.Equals(Development);
    public static bool IsQA() => _serverEnvironment.Equals(QA);
    public static bool IsStaging() => _serverEnvironment.Equals(Staging);
    public static bool IsProduction() => _serverEnvironment.Equals(Production);
}