using AspNetMvcLogin.Models;
using AspNetMvcLogin.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcLogin.Services;

public sealed class MasterProductService
{
	private readonly AppDbContext _dbContext;

	public MasterProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public string MasterKategori(ApiKategoriRequest? request){
		try {
            var requestKategori = new Kategori
            {
                Name = request.Name
            };
        
            _dbContext.Kategori.Add(requestKategori);
            _dbContext.SaveChanges();

            return requestKategori.Name;

		}catch (Exception ex){
            Console.WriteLine(ex.ToString());
            return "Error";
        }
	}

    public List<Kategori> GetAllKategori(){
        try{
            return _dbContext.Kategori
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToList();
        }catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public string SubmitProduct(ApiProductRequest? request){
        try {
            
            var requestProduct = new Product
            {
                Name = request.Name,
                Kategori_id = request.kategori_id,
                Price = request.price
            };

            
            _dbContext.Products.Add(requestProduct);
            _dbContext.SaveChanges();

            return requestProduct.Name;
        }catch (Exception  ex){
            Console.WriteLine(ex.ToString());
            return "Error";
        }
    }

    public List<Product> GetProductAll(){
        try {
            var product = _dbContext.Products
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToList();

            return product;

        }catch (Exception  ex){
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public Product GetProductById(int product_id){
        try {
            var product = _dbContext.Products
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == product_id);

            return product;

        }catch (Exception  ex){
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}