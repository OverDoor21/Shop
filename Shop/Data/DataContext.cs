using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Bargain> Bargains { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
