﻿// <auto-generated />
using System;
using DocumentOperation.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocumentOperation.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230718082358_init-9")]
    partial class init9
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DocumentOperation.Data.Entities.InvoiceDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("DocumentOperation.Data.Entities.InvoiceDetailDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("InvoiceDataModelId")
                        .HasColumnType("int");

                    b.Property<int>("InvoiceDetailId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UnitCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceDataModelId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("DocumentOperation.Data.Entities.InvoiceHeaderDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReceiverTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("InvoiceHeaders");
                });

            modelBuilder.Entity("DocumentOperation.Data.Entities.InvoiceDetailDataModel", b =>
                {
                    b.HasOne("DocumentOperation.Data.Entities.InvoiceDataModel", null)
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceDataModelId");
                });

            modelBuilder.Entity("DocumentOperation.Data.Entities.InvoiceHeaderDataModel", b =>
                {
                    b.HasOne("DocumentOperation.Data.Entities.InvoiceDataModel", null)
                        .WithOne("InvoiceHeader")
                        .HasForeignKey("DocumentOperation.Data.Entities.InvoiceHeaderDataModel", "InvoiceId")
                        .HasPrincipalKey("DocumentOperation.Data.Entities.InvoiceDataModel", "InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocumentOperation.Data.Entities.InvoiceDataModel", b =>
                {
                    b.Navigation("InvoiceDetails");

                    b.Navigation("InvoiceHeader")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}