﻿// <auto-generated />
using System;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManager.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(LibraryManagerDbContext))]
    partial class LibraryManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CurrentLoanId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentLoanId")
                        .IsUnique()
                        .HasFilter("[CurrentLoanId] IS NOT NULL");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeadLineDevolutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReturnedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdBook");

                    b.HasIndex("IdUser");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Loan", "CurrentLoan")
                        .WithOne()
                        .HasForeignKey("LibraryManager.Domain.Entities.Book", "CurrentLoanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CurrentLoan");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Loan", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LibraryManager.Domain.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
