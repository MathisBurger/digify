using digify.Models;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

/// <summary>
/// Handles basic information about the backend and
/// system in general
/// </summary>
[ApiController]
public class ServerInformationController : ControllerBase
{
    /// <summary>
    /// Returns all important server information
    /// </summary>
    [HttpGet("/")]
    public IActionResult ServerInformation()
    {
        return Ok(new ServerInformation("v1"));
    }
}