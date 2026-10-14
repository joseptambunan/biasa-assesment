using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AspNetMvcLogin.Data;
using AspNetMvcLogin.Models;
using AspNetMvcLogin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcLogin.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthApiController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly JwtTokenService _jwtTokenService;

    public AuthApiController(AppDbContext dbContext, JwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _jwtTokenService = jwtTokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ApiLoginRequest? request)
    {
        if (request is null ||
            string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new
            {
                message = "Username dan password wajib diisi."
            });
        }

        var username = request.Username.Trim();
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(item => item.Username == username);

        if (user is null)
        {
            return Unauthorized(new
            {
                message = "Username atau password tidak sesuai."
            });
        }
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);
        
        if (result == PasswordVerificationResult.Success ||
            result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            var token = _jwtTokenService.CreateToken(
                userId: user.Id.ToString(),
                username: user.Username,
                displayName: user.Username);

            return Ok(new
            {
                accessToken = token,
                tokenType = "Bearer",
                expiresIn = 3600
            });
        }

        return Unauthorized(new
        {
            message = "Username atau password tidak sesuai."
        });
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new CurrentUserResponse(
            User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
            User.Identity?.Name ?? "",
            User.FindFirstValue("displayName") ?? "",
            User.FindFirstValue("roleAccess") ?? "" ));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Logout berhasil." });
    }
}
