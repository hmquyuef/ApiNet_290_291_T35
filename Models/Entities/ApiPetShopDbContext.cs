using Microsoft.EntityFrameworkCore;

namespace ApiNet_290_291_T35.Models.Entities
{
    public class ApiPetShopDbContext  : DbContext
    {
        public ApiPetShopDbContext() { }

        public ApiPetShopDbContext(DbContextOptions<ApiPetShopDbContext> options) : 
            base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
