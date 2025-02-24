﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VseTShirts.DB;

#nullable disable

namespace VseTShirts.DB.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241101154638_addCollectionNameTOProduct")]
    partial class addCollectionNameTOProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VseTShirts.DB.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.CartPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartPositions");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.ComparedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("ComparedProducts");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.FavoriteProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("FavoriteProducts");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CollectionId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOfCollection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("92bced76-82ba-4f44-af74-70eb7b31a6f9"),
                            Category = "Category 1",
                            Color = "Red",
                            Description = "Description 1",
                            Name = "Product 1",
                            NameOfCollection = "Коллекция1",
                            Price = 100m,
                            Quantity = 10,
                            Sex = "Male",
                            Size = "S"
                        },
                        new
                        {
                            Id = new Guid("ba7aec10-45d0-49ad-8ee6-ddbe95371796"),
                            Category = "Category 1",
                            Color = "Red",
                            Description = "Description 1",
                            Name = "Product 1",
                            NameOfCollection = "Коллекция1",
                            Price = 100m,
                            Quantity = 10,
                            Sex = "Male",
                            Size = "S"
                        });
                });

            modelBuilder.Entity("VseTShirts.DB.Models.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c96dc613-1372-4746-87d7-47fed78a990b"),
                            ProductId = new Guid("92bced76-82ba-4f44-af74-70eb7b31a6f9"),
                            URL = "/Images/Products/Image1.jpg"
                        },
                        new
                        {
                            Id = new Guid("68bfe1d6-a659-4407-aa2a-d38b10af42b1"),
                            ProductId = new Guid("ba7aec10-45d0-49ad-8ee6-ddbe95371796"),
                            URL = "/Images/Products/Image2.jpg"
                        });
                });

            modelBuilder.Entity("VseTShirts.DB.Models.CartPosition", b =>
                {
                    b.HasOne("VseTShirts.DB.Models.Cart", null)
                        .WithMany("Positions")
                        .HasForeignKey("CartId");

                    b.HasOne("VseTShirts.DB.Models.Order", null)
                        .WithMany("Positions")
                        .HasForeignKey("OrderId");

                    b.HasOne("VseTShirts.DB.Models.Product", "Product")
                        .WithMany("CartPositions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.ComparedProduct", b =>
                {
                    b.HasOne("VseTShirts.DB.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.FavoriteProduct", b =>
                {
                    b.HasOne("VseTShirts.DB.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Product", b =>
                {
                    b.HasOne("VseTShirts.DB.Models.Collection", null)
                        .WithMany("Products")
                        .HasForeignKey("CollectionId");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.ProductImage", b =>
                {
                    b.HasOne("VseTShirts.DB.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Cart", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Collection", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Order", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("VseTShirts.DB.Models.Product", b =>
                {
                    b.Navigation("CartPositions");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
