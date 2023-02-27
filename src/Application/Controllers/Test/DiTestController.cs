using application.Models.Test.DiTest;
using Infrastructure.Service.Di;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers;

/// <summary>
/// DI 서비스 수명 테스트 API 컨트롤러
/// </summary>
[ApiController]
[Route("test/di")]
public class DiTestController : ControllerBase
{
    private readonly SingletonService _singleton;
    private readonly ScopedService _scoped1;
    private readonly ScopedService _scoped2;
    private readonly TransientService _transient1;
    private readonly TransientService _transient2;
    
    public DiTestController(SingletonService singleton, ScopedService scoped1, ScopedService scoped2, TransientService transient1, TransientService transient2)
    {
        _singleton = singleton;
        _scoped1 = scoped1;
        _scoped2 = scoped2;
        _transient1 = transient1;
        _transient2 = transient2;
    }
    
    // GET
    [HttpGet]
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