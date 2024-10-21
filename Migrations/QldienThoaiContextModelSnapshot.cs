﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLDienThoai.Models;

#nullable disable

namespace QLDienThoai.Migrations
{
    [DbContext(typeof(QldienThoaiContext))]
    partial class QldienThoaiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QLDienThoai.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Slug")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BrandId")
                        .HasName("PK__Brands__DAD4F05E90ADE922");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("QLDienThoai.Models.Category", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int")
                        .HasColumnName("CategoriesID");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Slug")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoriesId")
                        .HasName("PK__Categori__EFF907B09A8AD06F");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("QLDienThoai.Models.DanhGia", b =>
                {
                    b.Property<int>("IdDanhGia")
                        .HasColumnType("int")
                        .HasColumnName("ID_DanhGia");

                    b.Property<string>("Comment")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("IdSanPham")
                        .HasColumnType("int")
                        .HasColumnName("ID_SanPham");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Star")
                        .HasColumnType("int");

                    b.HasKey("IdDanhGia")
                        .HasName("PK__DanhGia__6C898AE12411B9D5");

                    b.HasIndex("IdSanPham");

                    b.ToTable("DanhGia");
                });

            modelBuilder.Entity("QLDienThoai.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BCF7EAABD66");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("QLDienThoai.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId")
                        .HasName("PK__OrderDet__D3B9D36C34CF5106");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("QLDienThoai.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__8AFACE1AC644E28F");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("QLDienThoai.Models.RoleClaim", b =>
                {
                    b.Property<int>("RoleClaimId")
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RoleClaimId")
                        .HasName("PK__RoleClai__BB90E9563F926E20");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("QLDienThoai.Models.SanPham", b =>
                {
                    b.Property<int>("IdBanPham")
                        .HasColumnType("int")
                        .HasColumnName("ID_Ban_Pham");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("int")
                        .HasColumnName("CategoriesID");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Images")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("IdBanPham")
                        .HasName("PK__SanPham__EC21BD235D20C44B");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("QLDienThoai.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Occupation")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CC4CED85607A");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QLDienThoai.Models.UserClaim", b =>
                {
                    b.Property<int>("ClaimId")
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ClaimId")
                        .HasName("PK__UserClai__EF2E139B44E11E5F");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("QLDienThoai.Models.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LoginProvider", "UserId")
                        .HasName("PK__UserLogi__028A9453A679B493");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("QLDienThoai.Models.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("PK__UserToke__8CC498412461FDBC");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId")
                        .HasName("PK__UserRole__AF2760AD994C81E4");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("QLDienThoai.Models.DanhGia", b =>
                {
                    b.HasOne("QLDienThoai.Models.SanPham", "IdSanPhamNavigation")
                        .WithMany("DanhGia")
                        .HasForeignKey("IdSanPham")
                        .HasConstraintName("FK__DanhGia__ID_SanP__6477ECF3");

                    b.Navigation("IdSanPhamNavigation");
                });

            modelBuilder.Entity("QLDienThoai.Models.Order", b =>
                {
                    b.HasOne("QLDienThoai.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Orders__UserId__6754599E");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QLDienThoai.Models.OrderDetail", b =>
                {
                    b.HasOne("QLDienThoai.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderDeta__Order__6A30C649");

                    b.HasOne("QLDienThoai.Models.SanPham", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__OrderDeta__Produ__6B24EA82");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("QLDienThoai.Models.RoleClaim", b =>
                {
                    b.HasOne("QLDienThoai.Models.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__RoleClaim__RoleI__5441852A");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("QLDienThoai.Models.SanPham", b =>
                {
                    b.HasOne("QLDienThoai.Models.Brand", "Brand")
                        .WithMany("SanPhams")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK__SanPham__BrandId__619B8048");

                    b.HasOne("QLDienThoai.Models.Category", "Categories")
                        .WithMany("SanPhams")
                        .HasForeignKey("CategoriesId")
                        .HasConstraintName("FK__SanPham__Categor__60A75C0F");

                    b.Navigation("Brand");

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("QLDienThoai.Models.UserClaim", b =>
                {
                    b.HasOne("QLDienThoai.Models.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserClaim__UserI__5165187F");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QLDienThoai.Models.UserLogin", b =>
                {
                    b.HasOne("QLDienThoai.Models.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__UserLogin__UserI__571DF1D5");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QLDienThoai.Models.UserToken", b =>
                {
                    b.HasOne("QLDienThoai.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__UserToken__UserI__59FA5E80");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("QLDienThoai.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__UserRoles__RoleI__4E88ABD4");

                    b.HasOne("QLDienThoai.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__UserRoles__UserI__4D94879B");
                });

            modelBuilder.Entity("QLDienThoai.Models.Brand", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("QLDienThoai.Models.Category", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("QLDienThoai.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("QLDienThoai.Models.Role", b =>
                {
                    b.Navigation("RoleClaims");
                });

            modelBuilder.Entity("QLDienThoai.Models.SanPham", b =>
                {
                    b.Navigation("DanhGia");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("QLDienThoai.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserClaims");

                    b.Navigation("UserLogins");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
