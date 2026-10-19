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

    public List<Product> GetProductAll(int pageNumber = 1){
        try {

            const int pageSize = 1;
            var product = _dbContext.Products
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
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

    public Product UpdateProduct(ApiProductRequest? request){
        try{
            var requestProduct = new Product
            {
                Name = request.Name,
                Kategori_id = request.kategori_id,
                Price = request.price
            };
            var product = _dbContext.Products.Find(request.product_id);
            product.Name = request.Name;
            product.Kategori_id = request.kategori_id;
            product.Price = request.price;
            _dbContext.SaveChanges();

            return requestProduct;
        
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public List<Product> SearchProduct(int? kategori_id, string product_name,int pageNumber = 1){
        try {
            const int pageSize = 10;
            if ( kategori_id.HasValue){
                var product = _dbContext.Products
                .AsNoTracking()
                .Where(x =>
                    x.Kategori_id == kategori_id.Value ||
                    EF.Functions.Like(x.Name, $"%{product_name}%")
                )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
                return product;

            }else{
                var product = _dbContext.Products
                .AsNoTracking()
                .Where(x =>
                    EF.Functions.Like(x.Name, $"%{product_name}%")
                )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                return product;    
            }
            

        }catch (Exception  ex){
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}