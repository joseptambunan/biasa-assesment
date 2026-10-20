using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcLogin.Controllers;


public sealed class HomeController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public HomeController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public IActionResult Index() => Home();

    [AllowAnonymous]
    public IActionResult Error() => Problem();

    [HttpGet]
    [Route("")]
    public IActionResult Home()
    {
        var filePath = Path.Combine(_environment.WebRootPath, "index.html");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File wwwroot/index.html tidak ditemukan.");
        }

        return PhysicalFile(filePath, "text/html; charset=utf-8");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        var filePath = Path.Combine(_environment.WebRootPath, "login.html");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File wwwroot/index.html tidak ditemukan.");
        }

        return PhysicalFile(filePath, "text/html; charset=utf-8");
    }
}
