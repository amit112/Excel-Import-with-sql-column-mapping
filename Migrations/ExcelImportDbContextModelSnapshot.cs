﻿// <auto-generated />
using System;
using ExcelImportWithColSelectionByAmit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExcelImportWithColSelectionByAmit.Migrations
{
    [DbContext(typeof(ExcelImportDbContext))]
    partial class ExcelImportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExcelImportWithColSelectionByAmit.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlternativeId");

                    b.Property<string>("Children");

                    b.Property<string>("DrivingLicense");

                    b.Property<string>("Education");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MaritalStatus");

                    b.Property<string>("Phone");

                    b.Property<string>("Sex");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ExcelImportWithColSelectionByAmit.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankID");

                    b.Property<string>("BankName");

                    b.Property<int?>("BranchID");

                    b.Property<string>("BranchName");

                    b.Property<int?>("CurrencyID");

                    b.Property<string>("CurrencyName");

                    b.Property<string>("Nature");

                    b.Property<int?>("NatureID");

                    b.Property<int?>("StateID");

                    b.Property<string>("StateName");

                    b.Property<string>("VendorName");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });
#pragma warning restore 612, 618
        }
    }
}
