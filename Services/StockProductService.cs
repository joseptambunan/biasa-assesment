using AspNetMvcLogin.Models;
using AspNetMvcLogin.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcLogin.Services;

public sealed class StockProductService
{
	private readonly AppDbContext _dbContext;

	public StockProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string UpdateStock(ApiUpdateStock? request){
    	try {
            var requestStock = new ProductStocks
            {
                ProductId = request.product_id,
                stock = request.stock
            };
        
            _dbContext.ProductStocks.Add(requestStock);
            _dbContext.SaveChanges();

            var product = _dbContext.Products.Find(request.product_id);
            product.stock = request.stock;
            _dbContext.SaveChanges();

            return "SUCCESS";

		}catch (Exception ex){
            Console.WriteLine(ex.ToString());
            return "Error";
        }
    }

}