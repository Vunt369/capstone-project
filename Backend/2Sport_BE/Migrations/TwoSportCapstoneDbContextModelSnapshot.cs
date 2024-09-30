﻿
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _2Sport_BE.Repository.Data;

#nullable disable

namespace _2Sport_BE.Migrations
{
    [DbContext(typeof(TwoSportCapstoneDbContext))]
    partial class TwoSportCapstoneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("CheckedBy")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Reason");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Status");

                    b.HasKey("AttendanceId");

                    b.HasIndex("BranchId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ManagerEmployeeId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BlogName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("BlogName");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar")
                        .HasColumnName("Content");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("BranchName");

                    b.Property<string>("Hotline")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar")
                        .HasColumnName("Hotline");

                    b.Property<string>("ImgAvatarName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("ImgAvatarName");

                    b.Property<string>("ImgAvatarPath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("ImgAvatarPath");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Location");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("BrandName");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("Logo");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.BrandCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("BrandId");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BrandCategories");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CartId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("CartId");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal")
                        .HasColumnName("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("CategoryName");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Description");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SportId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.CustomerDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LoyaltyPoints")
                        .HasColumnType("int");

                    b.Property<string>("MembershipLevel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("MembershipLevel");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Address");

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("ImgAvatarPath");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar")
                        .HasColumnName("FullName");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Gender");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("HashPassword");

                    b.Property<string>("PasswordResetToken")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("PhoneNumber");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Username");

                    b.HasKey("EmployeeId");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.EmployeeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BranchId")
                        .HasColumnType("int")
                        .HasColumnName("BranchId");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeId");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Position");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int")
                        .HasColumnName("SupervisorId");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("EmployeeDetails");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ErrorLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InnerException")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ErrorLogs");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ImagesVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlogId")
                        .HasColumnType("int")
                        .HasColumnName("BlogId");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("ImageUrl");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("VideoUrl");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("ProductId");

                    b.ToTable("ImagesVideos");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ImportHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Content");

                    b.Property<DateTime?>("ImportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LotCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("LotCode");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("SupplierId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("ImportHistories");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlogId")
                        .HasColumnType("int")
                        .HasColumnName("BlogId");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("IntoMoney")
                        .HasColumnType("decimal")
                        .HasColumnName("IntoMoney");

                    b.Property<string>("OrderCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("OrderCode");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodId");

                    b.Property<DateTime?>("ReceivedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ShipmentDetailId")
                        .HasColumnType("int")
                        .HasColumnName("ShipmentDetailId");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal")
                        .HasColumnName("TotalPrice");

                    b.Property<decimal?>("TranSportFee")
                        .HasColumnType("decimal")
                        .HasColumnName("TranSportFee");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("ShipmentDetailId");

                    b.HasIndex("UserId");

                b.ToTable("Orders");
            });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderId");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal")
                        .HasColumnName("Price");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("PaymentMethodName");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("BrandId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<string>("Color")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Color");

                    b.Property<int?>("Condition")
                        .HasColumnType("int")
                        .HasColumnName("Condition");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("ImgAvatarName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("ImgAvatarName");

                    b.Property<string>("ImgAvatarPath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("ImgAvatarPath");

                    b.Property<bool>("IsRent")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ListedPrice")
                        .HasColumnType("decimal")
                        .HasColumnName("ListedPrice");

                    b.Property<string>("Offers")
                        .HasColumnType("nvarchar")
                        .HasColumnName("Offers");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal")
                        .HasColumnName("Price");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("ProductCode");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("ProductName");

                    b.Property<decimal?>("RentPrice")
                        .HasColumnType("decimal")
                        .HasColumnName("RentPrice");

                    b.Property<string>("Size")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Size");

                    b.Property<int>("SportId")
                        .HasColumnType("int")
                        .HasColumnName("SportId");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SportId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeId");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("JwtId");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("Token");

                    b.Property<bool?>("Used")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<string>("ReviewContent")
                        .IsRequired()
                        .HasColumnType("nvarchar")
                        .HasColumnName("ReviewContent");

                    b.Property<decimal?>("Star")
                        .HasColumnType("decimal")
                        .HasColumnName("Star");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Description");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("RoleName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ShipmentDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Address");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("FullName");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar")
                        .HasColumnName("PhoneNumber");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShipmentDetails");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Location");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar")
                        .HasColumnName("SupplierName");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacebookId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoogleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BranchId")
                        .HasColumnType("int")
                        .HasColumnName("BranchId");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("ProductId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Attendance", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Branch", "Branch")
                        .WithMany("Attendances")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2Sport_BE.Repository.Models.Employee", "Employee")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("_2Sport_BE.Repository.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Employee");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Blog", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.BrandCategory", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Brand", "Brand")
                        .WithMany("BrandCategories")
                        .HasForeignKey("BrandId");

                    b.HasOne("_2Sport_BE.Repository.Models.Category", "Category")
                        .WithMany("BrandCategories")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Cart", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.CartItem", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId");

                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Category", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.CustomerDetail", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Employee", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.EmployeeDetail", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Branch", "Branch")
                        .WithMany("EmployeeDetails")
                        .HasForeignKey("BranchId");

                    b.HasOne("_2Sport_BE.Repository.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("_2Sport_BE.Repository.Models.Employee", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Branch");

                    b.Navigation("Employee");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ImagesVideo", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId");

                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("ImagesVideos")
                        .HasForeignKey("ProductId");

                    b.Navigation("Blog");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ImportHistory", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("ImportHistories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2Sport_BE.Repository.Models.Supplier", "Supplier")
                        .WithMany("ImportHistories")
                        .HasForeignKey("SupplierId");

                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Like", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Blog", "Blog")
                        .WithMany("Likes")
                        .HasForeignKey("BlogId");

                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("Likes")
                        .HasForeignKey("ProductId");

                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId");

                    b.Navigation("Blog");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Order", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("_2Sport_BE.Repository.Models.ShipmentDetail", "ShipmentDetail")
                        .WithMany("Orders")
                        .HasForeignKey("ShipmentDetailId");

                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("ShipmentDetail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.OrderDetail", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId");

                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Product", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2Sport_BE.Repository.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2Sport_BE.Repository.Models.Sport", "Sport")
                        .WithMany("Products")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.RefreshToken", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Employee", "Employee")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId");

                    b.Navigation("Employee");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Review", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId");

                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ShipmentDetail", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.User", "User")
                        .WithMany("ShipmentDetails")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.User", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Warehouse", b =>
                {
                    b.HasOne("_2Sport_BE.Repository.Models.Branch", "Branch")
                        .WithMany("Warehouses")
                        .HasForeignKey("BranchId");

                    b.HasOne("_2Sport_BE.Repository.Models.Product", "Product")
                        .WithMany("Warehouses")
                        .HasForeignKey("ProductId");

                    b.Navigation("Branch");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Blog", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Branch", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("EmployeeDetails");

                    b.Navigation("Warehouses");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Brand", b =>
                {
                    b.Navigation("BrandCategories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Category", b =>
                {
                    b.Navigation("BrandCategories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Employee", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.PaymentMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("ImagesVideos");

                    b.Navigation("ImportHistories");

                    b.Navigation("Likes");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");

                    b.Navigation("Warehouses");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.ShipmentDetail", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Sport", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.Supplier", b =>
                {
                    b.Navigation("ImportHistories");
                });

            modelBuilder.Entity("_2Sport_BE.Repository.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Likes");

                    b.Navigation("Orders");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Reviews");

                    b.Navigation("ShipmentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
