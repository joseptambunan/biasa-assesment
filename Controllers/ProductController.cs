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

    public ProductController(AppDbContext dbContext, MasterProductService masterProductService)
    {
        _dbContext = dbContext;
        _masterProductService = masterProductService;
    }

    public IActionResult Index() => View();

    [AllowAnonymous]
    public IActionResult Error() => Problem();

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
        try {
            if (request is null)
            {
                return BadRequest(new
                {
                    message = "Request body wajib diisi."
                });
            }

            var requestProduct = new Product
            {
                Name = request.Name,
                Kategori_id = request.kategori_id
            };

            
            _dbContext.Products.Add(requestProduct);
            _dbContext.SaveChanges();

            return Ok(new
            {
                message = "Kategori berhasil disimpan.",
                data = requestProduct
            });
        }catch (Exception  ex){
            Console.WriteLine(ex.ToString());

            return StatusCode(500, new
            {
                message = "Gagal menyimpan kategori ke database"
            });
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{product_id:int}")]
    public IActionResult GetProductById(int product_id)
    {
        try {
            var product = _dbContext.Products
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == product_id);

            if (product is null)
            {
                return NotFound(new
                {
                    StatusCode = 200,
                    message = "Product tidak ditemukan."
                });
            }

            return Ok(product);

        }catch (Exception  ex){
            Console.WriteLine(ex.ToString());

            return StatusCode(500, new
            {
                message = "Gagal menampilkan database"
            });
        }
    }
}
