namespace application.Models.Test.NullableTest;

/// <summary>
/// Http Body 기반 Nullable 테스트 Http 요청 클래스
/// </summary>
public class HttpBodyNullableTestHttpRequest
{
    /// <summary>
    /// Null을 허용하지 않는 string 변수
    /// </summary>
    public string String { get; set; }
    
    /// <summary>
    /// Null을 허용하는 string 변수
    /// </summary>
    public string? NullableString { get; set; }

    /// <summary>
    /// Null을 허용하지 않는 string array 변수
    /// </summary>
    public string[] StringArray { get; set; }

    /// <summary>
    /// Null을 허용하는 string array 변수
    /// </summary>
    public string[]? NullableStringArray { get; set; }

    /// <summary>
    /// Null을 허용하지 않는 string list 변수
    /// </summary>
    public List<string> StringList { get; set; }

    /// <summary>
    /// Null을 허용하는 string list 변수
    /// </summary>
    public List<string>? NullableStringList { get; set; }
}