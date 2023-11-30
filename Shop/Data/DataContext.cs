using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Bargain> Bargains { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Seller> Sellers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Seller>("Seller");
        

        }

        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
