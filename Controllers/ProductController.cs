using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspNetMvcLogin.Models;
using AspNetMvcLogin.Data;
using Microsoft.EntityFrameworkCore;
using AspNetMvcLogin.Services;

namespace AspNetMvcLogin.Controllers;

[Authorize]
[ApiController]
[Route("api/product")]
public sealed class ProductController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly MasterProductService _masterProductService;
    private readonly StockProductService _stockProductService;

    public ProductController(AppDbContext dbContext, MasterProductService masterProductService, StockProductService stockProductService )
    {
        _dbContext = dbContext;
        _masterProductService = masterProductService;
        _stockProductService = stockProductService;
    }

    public IActionResult Index() => View();

    [AllowAnonymous]
    public IActionResult Error() => Problem();

    [HttpGet("/products")]
    public IActionResult ProductListPage() => View("Index");

    [HttpGet("/products/{productId:int}")]
    public IActionResult ProductDetailsPage(int productId) => View("Details", productId);

    [HttpGet("/categories")]
    public IActionResult CategoriesPage() => View("Categories");

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("kategori/submit")]
    public IActionResult SubmitKategori([FromBody] ApiKategoriRequest? request)
    {

        var kategori = _masterProductService.MasterKategori(request);
        if ( kategori != "Error"){
            return Ok(new
            {
                code = 200,
                message = "Kategori berhasil disimpan.",
                data = kategori
            });
        }

        return StatusCode(500, new
        {
            code = 500,
            message = "Gagal menyimpan kategori ke database"
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("kategori")]
    public IActionResult GetAllKategori()
    {
        var kategori = _masterProductService.GetAllKategori();
        return Ok(new
        {
            code = 200,
            message = "Kategori berhasil disimpan.",
            data = kategori
        });

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("submit")]
    public IActionResult SubmitProduct([FromBody] ApiProductRequest? request)
    {
        var product = _masterProductService.SubmitProduct(request);
        return Ok(new
        {
            code = 200,
            message = "Product berhasil disimpan.",
            data = product
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("all")]
    public IActionResult GetAllProduct([FromQuery] ApiGetAll? request)
    {
        var product = _masterProductService.GetProductAll(request.page);
        return Ok(new
        {
            code = 200,
            message = "Product berhasil disimpan.",
            data = product
        });

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{product_code}")]
    public IActionResult GetProductById(string product_code)
    {
        var product = _masterProductService.GetProductById(product_code);
        if ( product is null ){
            return NotFound(new{ 
                message = "Data tidak ditemukan" 
            });
        }

        return Ok(new
        {
            code = 200,
            message = "Product berhasil disimpan.",
            data = product
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("update")]
    public IActionResult UpdateProductById([FromBody] ApiProductRequest? request)
    {
        var product = _masterProductService.UpdateProduct(request);
        if ( product is null ){
            return NotFound(new{ 
                message = "Data tidak ditemukan" 
            });
        }
        
        return Ok(new
        {
            code = 200,
            message = "Product berhasil disimpan.",
            data = product
        });
    }

    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("search")]
    public IActionResult SearchProduct([FromBody] ApiSearchRequest? request)
    {

        var product = _masterProductService.SearchProduct(request.kategori, request.product, request.page);
        return Ok(new
        {
            code = 200,
            message = "Product berhasil disimpan.",
            data = product
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("stock")]
    public IActionResult SubmitStock([FromBody] ApiUpdateStock? request)
    {

        var stock = _stockProductService.UpdateStock(request);
        if ( stock != "Error"){
            return Ok(new
            {
                code = 200,
                message = "Kategori berhasil disimpan.",
                data = stock
            });
        }

        return StatusCode(500, new
        {
            code = 500,
            message = "Gagal menyimpan stock ke database"
        });
    }
}
