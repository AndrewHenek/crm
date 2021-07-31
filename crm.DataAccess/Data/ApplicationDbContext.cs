using crm.Models;
using Microsoft.EntityFrameworkCore;

namespace crm.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=db-crm;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .Property(currency => currency.Id)
                .HasColumnName("id");

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.Symbol)
                .HasColumnName("symbol")
                .IsRequired()
                .HasMaxLength(3);

            modelBuilder.Entity<Currency>()
                .HasIndex(currency => currency.Symbol)
                .IsUnique();

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.Rate)
                .HasColumnName("rate");

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.IsSync)
                .HasColumnName("is_sync")
                .HasDefaultValue(false);

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.UpdatedAt)
                .HasColumnName("updated_at");

            modelBuilder.Entity<Currency>()
                .Property(currency => currency.Ghosted)
                .HasColumnName("ghosted")
                .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Currency> Currencies { get; set; }
    }
}
