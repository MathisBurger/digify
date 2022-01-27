using digify.Models;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

[ApiController]
public class ServerInformationController : ControllerBase
{
    [HttpGet("/api/server-information")]
    public ActionResult<ServerInformation> Index()
    {
        return new ServerInformation("v1");
    }
}