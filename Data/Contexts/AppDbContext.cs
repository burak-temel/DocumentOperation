using DocumentOperation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DocumentOperation.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<InvoiceDataModel> Invoices { get; set; }
        public DbSet<InvoiceHeaderDataModel> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetailDataModel> InvoiceDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships and additional configurations for the entities
            modelBuilder.Entity<InvoiceDataModel>()
                        .HasKey(i => i.Id);
            modelBuilder.Entity<InvoiceDataModel>()
                        .HasIndex(i => i.InvoiceId)
                        .IsUnique();

            modelBuilder.Entity<InvoiceHeaderDataModel>()
                        .HasKey(ih => ih.Id);
            modelBuilder.Entity<InvoiceHeaderDataModel>()
                        .HasIndex(i => i.InvoiceId)
                        .IsUnique();

            modelBuilder.Entity<InvoiceDataModel>()
                .HasOne(i => i.InvoiceHeader)
                .WithOne()
                .HasForeignKey<InvoiceHeaderDataModel>(ih => ih.InvoiceId)
                .HasPrincipalKey<InvoiceDataModel>(i => i.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceDetailDataModel>()
                        .Property(i => i.UnitPrice)
                        .HasColumnType("decimal(18, 2)");



            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey != null)
                {
                    foreach (var property in primaryKey.Properties)
                    {
                        property.ValueGenerated = ValueGenerated.OnAdd;
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
