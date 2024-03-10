﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheListener.Data;

#nullable disable

namespace TheListener.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240309184418_ali")]
    partial class ali
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TheListener.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Germany"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "French"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "English"
                        });
                });

            modelBuilder.Entity("TheListener.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "This is Jana and welcome to Storynory. I’m back with another cheeky Monkey story.",
                            ImageUrl = "sssssssssssss",
                            Name = "Listen German"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Katie is learning Latin - the language of the ancient Romans",
                            ImageUrl = "sssssssssssss",
                            Name = "Listen German 2"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Description = "Jana talks with our friends Matthew and Elaine Sweetapple, the creators of Lost on Infinity",
                            ImageUrl = "sssssssssssss",
                            Name = "Listen French"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = "This year Chinese and many Asian nations are celebrating the year of the Dragon.",
                            ImageUrl = "sssssssssssss",
                            Name = "Listen French 2"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            Description = "Parents and older kids check out Relaxivity and our Cleopatra story.",
                            ImageUrl = "sssssssssssss",
                            Name = "Listen English"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 3,
                            Description = "Jana, and I’m here with a story about an old friend of ours. His name is Birdy.",
                            ImageUrl = "sssssssssssss",
                            Name = "Listen English 2"
                        });
                });

            modelBuilder.Entity("TheListener.Models.Product", b =>
                {
                    b.HasOne("TheListener.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
