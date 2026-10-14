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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Username)
            .IsUnique();
    }

    public DbSet<UserGroup> UserGroup => Set<UserGroup>();
    public DbSet<Kategori> Kategori => Set<Kategori>();
}
