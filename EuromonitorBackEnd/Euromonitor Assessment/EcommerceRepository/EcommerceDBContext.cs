using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceRepository
{
    public class EcommerceDBContext: IdentityDbContext
    {
        public EcommerceDBContext()
        {

        }
        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Book> books { get; set; }
    }
}
