using AspNetMvcLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcLogin.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Kategori> Kategori => Set<Kategori>();
    public DbSet<Group> Group => Set<Group>();
    public DbSet<UserGroup> UserGroups => Set<UserGroup>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductStocks> ProductStocks => Set<ProductStocks>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Username)
            .IsUnique();

        modelBuilder.Entity<Group>();

        modelBuilder.Entity<Kategori>();

        modelBuilder.Entity<UserGroup>()
            .HasOne(x => x.User)
            .WithOne(x => x.UserGroup)
            .HasForeignKey<UserGroup>(x => x.UserId);

        modelBuilder.Entity<UserGroup>()
            .HasOne(x => x.Group)
            .WithOne(x => x.UserGroup)
            .HasForeignKey<UserGroup>(x => x.GroupId);

        modelBuilder.Entity<Product>()
            .HasIndex(x => x.Kategori_id);

        modelBuilder.Entity<Product>()
            .HasOne<Kategori>()
            .WithMany()
            .HasForeignKey(x => x.Kategori_id);

        modelBuilder.Entity<ProductStocks>()
            .HasIndex(x => x.ProductId);

        modelBuilder.Entity<ProductStocks>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }

}
