using Microsoft.EntityFrameworkCore;

namespace mvc_aspnet_test.Infrastructure
{
    public class CmsShoppingCartContext : DbContext
    {
        public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options)
            : base(options)
        { 
        }
    }
}
