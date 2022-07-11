﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Novi.Models;

namespace DiplomskiServer.Migrations
{
    [DbContext(typeof(CategoryContext))]
    [Migration("20220709182700_V12")]
    partial class V12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Novi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_category")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PictureId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Novi.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_group")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.Property<int?>("id_category")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PictureId");

                    b.HasIndex("id_category");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Novi.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_image")
                        .UseIdentityColumn();

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("data");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<int?>("id_product")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_product");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Novi.Models.NumberOfViewe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_viewe")
                        .UseIdentityColumn();

                    b.Property<int>("IdUser")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<int?>("id_product")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_product");

                    b.ToTable("NumberOfViewe");
                });

            modelBuilder.Entity("Novi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_product")
                        .UseIdentityColumn();

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("details");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<int>("NumberOfLike")
                        .HasColumnType("int")
                        .HasColumnName("number_of_like");

                    b.Property<int>("NumberOfWish")
                        .HasColumnType("int")
                        .HasColumnName("number_of_wish");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.Property<int?>("id_group")
                        .HasColumnType("int");

                    b.Property<int?>("id_user")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_group");

                    b.HasIndex("id_user");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Novi.Models.ProductInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_product_information")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<int?>("id_group")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_group");

                    b.ToTable("ProductInformation");
                });

            modelBuilder.Entity("Novi.Models.ProductInformationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_product_information_data")
                        .UseIdentityColumn();

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("data");

                    b.Property<int>("IdInfo")
                        .HasColumnType("int")
                        .HasColumnName("id_product_information_save");

                    b.Property<int?>("id_product")
                        .HasColumnType("int");

                    b.Property<int?>("id_product_information")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_product");

                    b.HasIndex("id_product_information");

                    b.ToTable("ProductInformationData");
                });

            modelBuilder.Entity("Novi.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_review")
                        .UseIdentityColumn();

                    b.Property<string>("Coment")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("coment");

                    b.Property<int>("Mark")
                        .HasColumnType("int")
                        .HasColumnName("mark");

                    b.Property<int?>("id_product")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_product");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("Novi.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_user")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit")
                        .HasColumnName("is_admin");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("picture");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("username");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Novi.Models.UserInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_user_information")
                        .UseIdentityColumn();

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("data");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<int?>("id_user")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_user");

                    b.ToTable("UserInformation");
                });

            modelBuilder.Entity("Novi.Models.Category", b =>
                {
                    b.HasOne("Novi.Models.Image", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("Novi.Models.Group", b =>
                {
                    b.HasOne("Novi.Models.Image", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.HasOne("Novi.Models.Category", "Category")
                        .WithMany("Groups")
                        .HasForeignKey("id_category");

                    b.Navigation("Category");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("Novi.Models.Image", b =>
                {
                    b.HasOne("Novi.Models.Product", "Product")
                        .WithMany("Picture")
                        .HasForeignKey("id_product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Novi.Models.NumberOfViewe", b =>
                {
                    b.HasOne("Novi.Models.Product", "Product")
                        .WithMany("NumberOfViewers")
                        .HasForeignKey("id_product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Novi.Models.Product", b =>
                {
                    b.HasOne("Novi.Models.Group", "Group")
                        .WithMany("Products")
                        .HasForeignKey("id_group");

                    b.HasOne("Novi.Models.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("id_user");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Novi.Models.ProductInformation", b =>
                {
                    b.HasOne("Novi.Models.Group", "Groups")
                        .WithMany("ProductInformation")
                        .HasForeignKey("id_group");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Novi.Models.ProductInformationData", b =>
                {
                    b.HasOne("Novi.Models.Product", "Product")
                        .WithMany("Data")
                        .HasForeignKey("id_product");

                    b.HasOne("Novi.Models.ProductInformation", "ProductInformation")
                        .WithMany()
                        .HasForeignKey("id_product_information");

                    b.Navigation("Product");

                    b.Navigation("ProductInformation");
                });

            modelBuilder.Entity("Novi.Models.Review", b =>
                {
                    b.HasOne("Novi.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("id_product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Novi.Models.UserInformation", b =>
                {
                    b.HasOne("Novi.Models.User", "User")
                        .WithMany("UserInformation")
                        .HasForeignKey("id_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Novi.Models.Category", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Novi.Models.Group", b =>
                {
                    b.Navigation("ProductInformation");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Novi.Models.Product", b =>
                {
                    b.Navigation("Data");

                    b.Navigation("NumberOfViewers");

                    b.Navigation("Picture");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Novi.Models.User", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("UserInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
