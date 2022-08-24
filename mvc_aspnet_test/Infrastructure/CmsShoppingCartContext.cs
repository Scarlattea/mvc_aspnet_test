using Microsoft.EntityFrameworkCore;
using mvc_aspnet_test.Models;

namespace mvc_aspnet_test.Infrastructure
{
    public class CmsShoppingCartContext : DbContext
    {
        public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options)
            : base(options)
        { 
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products  { get; set; }
    }
}
 