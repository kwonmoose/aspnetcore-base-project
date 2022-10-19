using System.Net.Mime;
using Infrastructure.Service.Di;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Models.Test.Response;

namespace application.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("test")]
public class TestController : ControllerBase
{
    private readonly SingletonService _singleton;
    private readonly ScopedService _scoped1;
    private readonly ScopedService _scoped2;
    private readonly TransientService _transient1;
    private readonly TransientService _transient2;
    
    public TestController(SingletonService singleton, ScopedService scoped1, ScopedService scoped2, TransientService transient1, TransientService transient2)
    {
        _singleton = singleton;
        _scoped1 = scoped1;
        _scoped2 = scoped2;
        _transient1 = transient1;
        _transient2 = transient2;
    }
    
    // GET
    [HttpGet("di")]
    public IActionResult DiTest()
    {
        var responseModel = new DiTestHttpResponse();
        responseModel.SingletonGuid = _singleton.GetGuid();
        responseModel.ScopedGuid1 = _scoped1.GetGuid();
        responseModel.ScopedGuid2 = _scoped2.GetGuid();
        responseModel.TransientGuid1 = _transient1.GetGuid();
        responseModel.TransientGuid2 = _transient2.GetGuid();
        
        return Ok(responseModel);
    }
}