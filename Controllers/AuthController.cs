using digify.Models;
using digify.Models.Requests;
using digify.Modules;
using Microsoft.AspNetCore.Mvc;
using digify.Shared;
using Microsoft.EntityFrameworkCore;


namespace digify.Controllers;


[ApiController]
public class AuthController : ControllerBase
{
    private readonly DatabaseContext Db;
    private readonly IPasswordHasher Hasher;
    private readonly IAuthorization auth;
    private readonly bool useSecureCookies;

    public AuthController(DatabaseContext _db, IPasswordHasher _hasher, IAuthorization _auth, IConfiguration _config)
    {
        Db = _db;
        Hasher = _hasher;
        auth = _auth;
        useSecureCookies = _config.GetValue("Authorization:UseSecureCookies", true);
    }
    [HttpPost("/auth/login")]
    public async Task<ActionResult> Login([FromBody] AuthCredentials creds)
    {
        var user = await Db.Users.Where(u => u.Username == creds.LoginName).FirstOrDefaultAsync();
        if (user == null)
        {
            return Unauthorized();
        }

        
        if (!Hasher.CompareHashAndPassword(user.Password, creds.Password)) return Unauthorized();
        var sessionJwt = auth.GetAuthToken(new AuthClaims(userId: user.Id));
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            MaxAge = Constants.SESSION_EXPIRATION,
            Secure = true
        };
        Response.Cookies.Append(Constants.SESSION_COOKIE_NAME, sessionJwt, cookieOptions);
        return Ok();
    } 
}