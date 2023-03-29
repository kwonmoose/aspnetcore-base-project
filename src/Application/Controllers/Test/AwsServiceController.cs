using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace application.Controllers;

/// <summary>
/// AWS Service 테스트 API 컨트롤러
/// </summary>
[ApiController]
[Route("test/aws-service")]
public class AwsServiceController : ControllerBase
{
    // Log
    private readonly ILogger<AwsServiceController> _logger;

    private readonly ConnectionStrings _connectionStrings;

    public AwsServiceController(ILogger<AwsServiceController> logger, IOptions<ConnectionStrings> connectionStrings)
    {
        _logger = logger;

        _connectionStrings = connectionStrings.Value;
    }
    
    [HttpGet("secrets-manager")]
    public async Task<IActionResult> GetSecretsManager()
    {
        return Ok(JsonSerializer.Serialize(_connectionStrings));
    }
}