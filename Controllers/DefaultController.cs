using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;
[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult EmptyRoute() => new RedirectResult("/dashboard");

    [HttpGet("{*url}")]
    public IActionResult DefaultRedirect([FromRoute] string url)
    {
        if (System.IO.File.Exists("./wwwroot/" + url))
        {
            return File("~/" + url, "text/html");
        }

        return NotFound();
    }
    
}