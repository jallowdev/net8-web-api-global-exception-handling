using global_exception_handling.exceptions;
using Microsoft.AspNetCore.Mvc;

namespace global_exception_handling.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExceptionController : Controller
{
    [Route("bad-request")]
    [HttpGet]
    public IActionResult BadRequest()
    {
        throw new BadHttpRequestException("The firstName is not blank.");
    }
    
    [Route("not-found")]
    [HttpGet]
    public IActionResult NotFound()
    {
        throw new NotFoundException("User not found");
    }
    
    [Route("internal")]
    [HttpGet]
    public IActionResult Internal()
    {
        throw new Exception("Service Not found");
    }
}