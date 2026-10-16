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
}