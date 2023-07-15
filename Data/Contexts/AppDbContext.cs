// AppDbContext.cs

using DocumentOperation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using DocumentOperation.Data.Entities;

namespace DocumentOperation.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships and additional configurations for the entities
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.InvoiceHeader)
                .WithOne()
                .HasForeignKey<InvoiceHeader>(ih => ih.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceDetail>()
                .HasKey(id => new { id.InvoiceId });

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
