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

    [HttpGet]
    [Route("kategori")]
    public IActionResult Kategori()
    {
        var filePath = Path.Combine(_environment.WebRootPath, "categories.html");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File wwwroot/index.html tidak ditemukan.");
        }

        return PhysicalFile(filePath, "text/html; charset=utf-8");
    }

    [HttpGet]
    [Route("product")]
    public IActionResult Product()
    {
        var filePath = Path.Combine(_environment.WebRootPath, "products.html");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File wwwroot/index.html tidak ditemukan.");
        }

        return PhysicalFile(filePath, "text/html; charset=utf-8");
    }

    [HttpGet]
    [Route("product/{product_code}")]
    public IActionResult ProductDetail(string product_code)
    {
        var filePath = Path.Combine(_environment.WebRootPath, "product-detail.html");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File wwwroot/index.html tidak ditemukan.");
        }

        return PhysicalFile(filePath, "text/html; charset=utf-8");
    }

    [HttpGet]
    [Route("product/add")]
    public IActionResult AddProduct()
    {
        var filePath = Path.Combine(_environment.WebRootPath, "product-add.html");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File wwwroot/index.html tidak ditemukan.");
        }

        return PhysicalFile(filePath, "text/html; charset=utf-8");
    }
}
