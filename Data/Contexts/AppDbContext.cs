using DocumentOperation.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
                        .HasKey(i => i.InvoiceId);

            modelBuilder.Entity<InvoiceHeader>()
                        .HasKey(ih => ih.InvoiceId);

            modelBuilder.Entity<Invoice>()
                        .HasOne(i => i.InvoiceHeader)
                        .WithOne()
                        .HasForeignKey<InvoiceHeader>(ih => ih.InvoiceId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceDetail>()
                        .HasKey(id => new { id.Id, id.InvoiceId });


            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
