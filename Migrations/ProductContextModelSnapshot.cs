﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Novi.Models;

namespace Novi.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Novi.Models.Image", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int")
                        .HasColumnName("ID_I");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Data");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.HasKey("ID");

                    b.ToTable("Slika");
                });

            modelBuilder.Entity("Novi.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Slika");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Username");

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Novi.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Category");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Opis");

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int")
                        .HasColumnName("Ocena");

                    b.Property<string>("Naziv")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Naziv");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Cena");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Novi.Models.Image", b =>
                {
                    b.HasOne("Novi.Models.Product", "Product")
                        .WithMany("Picture")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Novi.Models.Product", b =>
                {
                    b.HasOne("Novi.Models.Korisnik", null)
                        .WithMany("Products")
                        .HasForeignKey("KorisnikID");
                });

            modelBuilder.Entity("Novi.Models.Korisnik", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Novi.Models.Product", b =>
                {
                    b.Navigation("Picture");
                });
#pragma warning restore 612, 618
        }
    }
}
