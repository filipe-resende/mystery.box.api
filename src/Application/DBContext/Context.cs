using Domain.Entities;
using Infraestructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Application.DBContext;
public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new SteamCardMap());
        modelBuilder.ApplyConfiguration(new SteamCardCategoryMap());

    }

    //public DbSet<User> Users { get; set; }
    //public DbSet<SteamCard> SteamCards { get; set; }
    //public DbSet<SteamCardCategory> SteamCardCategorys { get; set; }

}

