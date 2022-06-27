using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("async")]
public class AsyncController : ControllerBase
{
    public AsyncController()
    {
        
    }

    [HttpGet]
    public async Task<ActionResult> GetMethod()
    {
        for (int i = 0; i < 10000000000; i++)
        {
            Console.WriteLine($"test-{i}");
        }
        
        return Ok(null);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetailMethod([FromRoute] ulong id)
    {
        return Ok(null);
    }

    [HttpPost]
    public async Task<IActionResult> PostMethod()
    {
        return Ok(null);
    }

    [HttpPut]
    public async Task<IActionResult> PutMethod()
    {
        return Ok(null);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMethod()
    {
        return Ok(null);
    }
}