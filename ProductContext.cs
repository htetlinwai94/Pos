using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace mysqlefcore
{
  public class PosContext : DbContext
  {
    
    public DbSet<Product> Product { get; set; }

    public DbSet<Group> groups { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Inventory> inventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL("server=localhost;database=Pos2;user=root;password=root");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Group>(entity =>
      {
        entity.HasKey(e => e.GCode);
        entity.Property(e => e.GName).IsRequired();
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.HasKey(e => e.PCode);
        entity.Property(e => e.Name).IsRequired();
        entity.HasOne(d => d.Group)
          .WithMany(p => p.products);
      });
      modelBuilder.Entity<User>(entity =>
      {
        entity.HasKey(e => e.UserID);
        entity.Property(e => e.Name).IsRequired();
      });
      modelBuilder.Entity<Inventory>(entity =>
      {
        entity.HasKey(e => e.ID);
        entity.Property(e => e.Quantity).IsRequired();
      });

    }
  }
}