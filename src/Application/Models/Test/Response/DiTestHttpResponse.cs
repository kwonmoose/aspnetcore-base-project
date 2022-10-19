namespace Microsoft.Extensions.DependencyInjection.Models.Test.Response;

public class DiTestHttpResponse
{
    public string SingletonGuid { get; set; }
    public string ScopedGuid1 { get; set; }
    public string ScopedGuid2 { get; set; }
    public string TransientGuid1 { get; set; }
    public string TransientGuid2 { get; set; }
}