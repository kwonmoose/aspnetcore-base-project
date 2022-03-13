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