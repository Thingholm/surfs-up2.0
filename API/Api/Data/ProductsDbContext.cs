namespace Api.Data;

using Api.Models;
using Microsoft.EntityFrameworkCore;

public class ProductsDbContext : DbContext
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardEquipment> BoardEquipments { get; set; }
    public DbSet<BoardType> BoardTypes { get; set; }
    public DbSet<Equipment> Equipments { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    
    }
}