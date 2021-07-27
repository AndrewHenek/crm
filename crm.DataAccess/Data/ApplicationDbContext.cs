using crm.Models;
using Microsoft.EntityFrameworkCore;

namespace crm.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
    }
}
