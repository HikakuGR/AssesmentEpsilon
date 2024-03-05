using Microsoft.EntityFrameworkCore;
namespace AssesmentEpsilon
{    

    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                    : base(options)
        {
        }
        public DatabaseContext()
        {
                
        }
    }
}
