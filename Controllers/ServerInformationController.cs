using digify.Models;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

[Route("[controller]")]
[ApiController]
public class ServerInformationController : ControllerBase
{
    [HttpGet("/api/server-information")]
    public IActionResult ServerInformation()
    {
        return Ok(new ServerInformation("v1"));
    }
}