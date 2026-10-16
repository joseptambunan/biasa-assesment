using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspNetMvcLogin.Models;
using AspNetMvcLogin.Data;

namespace AspNetMvcLogin.Controllers;

[Authorize]
[ApiController]
[Route("api/product")]
public sealed class ProductController : Controller
{
    private readonly AppDbContext _dbContext;

    public ProductController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index() => View();

    [AllowAnonymous]
    public IActionResult Error() => Problem();

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("kategori/submit")]
    public IActionResult SubmitKategori([FromBody] ApiKategoriRequest? request)
    {
        try {
            if (request is null)
            {
                return BadRequest(new
                {
                    message = "Request body wajib diisi."
                });
            }

            var requestKategori = new Kategori
            {
                Name = request.Name
            };

            
            _dbContext.Kategori.Add(requestKategori);
            _dbContext.SaveChanges();

            return Ok(new
            {
                message = "Kategori berhasil disimpan.",
                data = requestKategori
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
}
