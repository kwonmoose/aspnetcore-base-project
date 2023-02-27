using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using application.Models.Test.NullableTest;
using Serilog.Context;

namespace application.Controllers;

/// <summary>
/// Nullable 테스트 API 컨트롤러
/// </summary>
[ApiController]
[Route("test/nullable")]
public class NullableTestController : ControllerBase
{
    // Log
    private readonly ILogger<NullableTestController> _logger;

    public NullableTestController(ILogger<NullableTestController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult QueryStringNullableTest([FromQuery] QueryStringNullableTestHttpRequest request)
    {
        using (LogContext.PushProperty("LogProperty", new
               {
                   request = JsonSerializer.Serialize(request)
               }))
        {
            _logger.LogInformation("API QueryString Nullable Test");
        }
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult HttpBodyNullableTest([FromBody] HttpBodyNullableTestHttpRequest request)
    {
        using (LogContext.PushProperty("LogProperty", new
               {
                   request = JsonSerializer.Serialize(request)
               }))
        {
            _logger.LogInformation("API HTTP Body Nullable Test");
        }
        
        return Ok();
    }
}