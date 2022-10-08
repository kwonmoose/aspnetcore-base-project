using System.Diagnostics;
using System.Net.Mime;
using application.Models.Async.Request;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("async")]
public class AsyncController : ControllerBase
{
    private readonly ILogger<AsyncController> _logger;
    
    public AsyncController(ILogger<AsyncController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetMethod([FromBody] GetMethodHttpRequest model)
    {
        var sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < 100000000000; i++)
        {
            _logger.LogDebug($"test-{i}");
        }
        sw.Stop();
        
        _logger.LogDebug($"StopWatch - {sw.ElapsedMilliseconds}ms");
        
        return Ok("Get Async");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetailMethod([FromRoute] ulong id)
    {
        return Ok(id);
    }

    [HttpPost]
    public async Task<IActionResult> PostMethod()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutMethod()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMethod()
    {
        return Ok();
    }
}