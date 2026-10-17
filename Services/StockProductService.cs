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

    public string UpdateStock

}