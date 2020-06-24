﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Up.Data;

namespace Up.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200624050404_UpdateTableChiPhiThongKe")]
    partial class UpdateTableChiPhiThongKe
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "c688e63f-027d-4389-8610-198afc919c14",
                            ConcurrencyStamp = "4db8ad81-fdbe-41a4-8400-0741dfb5b285",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Up.Data.Entities.ChiPhiCoDinh", b =>
                {
                    b.Property<Guid>("ChiPhiCoDinhId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<double>("Gia");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("ChiPhiCoDinhId");

                    b.ToTable("ChiPhiCoDinhs");
                });

            modelBuilder.Entity("Up.Data.Entities.ChiPhiKhac", b =>
                {
                    b.Property<Guid>("ChiPhiKhacId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<double>("Gia");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<DateTime>("NgayChiPhi");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("ChiPhiKhacId");

                    b.ToTable("ChiPhiKhacs");
                });

            modelBuilder.Entity("Up.Data.Entities.GiaoVien", b =>
                {
                    b.Property<Guid>("GiaoVienId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BasicSalary");

                    b.Property<string>("CMND");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DiaChi");

                    b.Property<string>("FacebookAccount");

                    b.Property<string>("InitialName");

                    b.Property<bool>("IsDisabled");

                    b.Property<double>("MucHoaHong");

                    b.Property<string>("Name");

                    b.Property<string>("NganHang");

                    b.Property<DateTime>("NgayBatDau");

                    b.Property<DateTime?>("NgayKetThuc");

                    b.Property<Guid>("NgayLamViecId");

                    b.Property<string>("Phone");

                    b.Property<double>("TeachingRate");

                    b.Property<double>("TutoringRate");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("GiaoVienId");

                    b.HasIndex("NgayLamViecId");

                    b.ToTable("GiaoViens");
                });

            modelBuilder.Entity("Up.Data.Entities.GioHoc", b =>
                {
                    b.Property<Guid>("GioHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("From");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("To");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("GioHocId");

                    b.ToTable("GioHocs");
                });

            modelBuilder.Entity("Up.Data.Entities.HocPhi", b =>
                {
                    b.Property<Guid>("HocPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("GhiChu");

                    b.Property<double>("Gia");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("NgayApDung");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("HocPhiId");

                    b.ToTable("HocPhis");
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien", b =>
                {
                    b.Property<Guid>("HocVienId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EnglishName");

                    b.Property<string>("FacebookAccount");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("NgaySinh");

                    b.Property<string>("OtherPhone");

                    b.Property<string>("ParentFullName");

                    b.Property<string>("ParentPhone");

                    b.Property<string>("Phone");

                    b.Property<Guid?>("QuanHeId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("HocVienId");

                    b.HasIndex("QuanHeId");

                    b.ToTable("HocViens");
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien_LopHoc", b =>
                {
                    b.Property<Guid>("HocVien_LopHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("HocVienId");

                    b.Property<Guid>("LopHocId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("HocVien_LopHocId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("LopHocId");

                    b.ToTable("HocVien_LopHocs");
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien_NgayHoc", b =>
                {
                    b.Property<Guid>("HocVien_NgayHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("HocVienId");

                    b.Property<Guid>("LopHocId");

                    b.Property<DateTime>("NgayBatDau");

                    b.Property<DateTime?>("NgayKetThuc");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("HocVien_NgayHocId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("LopHocId");

                    b.ToTable("HocVien_NgayHocs");
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien_No", b =>
                {
                    b.Property<Guid>("HocVien_NoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("HocVienId");

                    b.Property<bool>("IsDisabled");

                    b.Property<Guid>("LopHocId");

                    b.Property<DateTime>("NgayNo");

                    b.Property<double>("TienNo");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("HocVien_NoId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("LopHocId");

                    b.ToTable("HocVien_Nos");
                });

            modelBuilder.Entity("Up.Data.Entities.KhoaHoc", b =>
                {
                    b.Property<Guid>("KhoaHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("KhoaHocId");

                    b.ToTable("KhoaHocs");
                });

            modelBuilder.Entity("Up.Data.Entities.LoaiCheDo", b =>
                {
                    b.Property<Guid>("LoaiCheDoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("LoaiCheDoId");

                    b.ToTable("LoaiCheDos");
                });

            modelBuilder.Entity("Up.Data.Entities.LoaiGiaoVien", b =>
                {
                    b.Property<Guid>("LoaiGiaoVienId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<byte?>("Order");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("LoaiGiaoVienId");

                    b.ToTable("LoaiGiaoViens");
                });

            modelBuilder.Entity("Up.Data.Entities.LopHoc", b =>
                {
                    b.Property<Guid>("LopHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("GioHocId");

                    b.Property<bool>("IsCanceled");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsGraduated");

                    b.Property<Guid>("KhoaHocId");

                    b.Property<string>("Name");

                    b.Property<Guid>("NgayHocId");

                    b.Property<DateTime?>("NgayKetThuc");

                    b.Property<DateTime>("NgayKhaiGiang");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("LopHocId");

                    b.HasIndex("GioHocId");

                    b.HasIndex("KhoaHocId");

                    b.HasIndex("NgayHocId");

                    b.ToTable("LopHocs");
                });

            modelBuilder.Entity("Up.Data.Entities.LopHoc_DiemDanh", b =>
                {
                    b.Property<Guid>("LopHoc_DiemDanhId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("HocVienId");

                    b.Property<bool?>("IsDuocNghi");

                    b.Property<bool>("IsOff");

                    b.Property<Guid>("LopHocId");

                    b.Property<DateTime>("NgayDiemDanh");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("LopHoc_DiemDanhId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("LopHocId");

                    b.ToTable("LopHoc_DiemDanhs");
                });

            modelBuilder.Entity("Up.Data.Entities.LopHoc_HocPhi", b =>
                {
                    b.Property<Guid>("LopHoc_HocPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("HocPhiId");

                    b.Property<Guid>("LopHocId");

                    b.Property<int>("Nam");

                    b.Property<int>("Thang");

                    b.HasKey("LopHoc_HocPhiId");

                    b.HasIndex("HocPhiId");

                    b.HasIndex("LopHocId");

                    b.ToTable("LopHoc_HocPhis");
                });

            modelBuilder.Entity("Up.Data.Entities.NgayHoc", b =>
                {
                    b.Property<Guid>("NgayHocId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("NgayHocId");

                    b.ToTable("NgayHocs");
                });

            modelBuilder.Entity("Up.Data.Entities.NgayLamViec", b =>
                {
                    b.Property<Guid>("NgayLamViecId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("NgayLamViecId");

                    b.ToTable("NgayLamViecs");
                });

            modelBuilder.Entity("Up.Data.Entities.NhanVien_ViTri", b =>
                {
                    b.Property<Guid>("NhanVien_ViTriId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CheDoId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("NhanVienId");

                    b.Property<Guid>("ViTriId");

                    b.HasKey("NhanVien_ViTriId");

                    b.HasIndex("CheDoId");

                    b.HasIndex("NhanVienId");

                    b.HasIndex("ViTriId");

                    b.ToTable("NhanVien_ViTris");
                });

            modelBuilder.Entity("Up.Data.Entities.QuanHe", b =>
                {
                    b.Property<Guid>("QuanHeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("QuanHeId");

                    b.ToTable("QuanHes");
                });

            modelBuilder.Entity("Up.Data.Entities.Quyen", b =>
                {
                    b.Property<int>("QuyenId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("QuyenId");

                    b.ToTable("Quyens");
                });

            modelBuilder.Entity("Up.Data.Entities.Quyen_Role", b =>
                {
                    b.Property<Guid>("Quyen_RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuyenId");

                    b.Property<string>("RoleId");

                    b.HasKey("Quyen_RoleId");

                    b.HasIndex("QuyenId");

                    b.ToTable("Quyen_Roles");
                });

            modelBuilder.Entity("Up.Data.Entities.Sach", b =>
                {
                    b.Property<Guid>("SachId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<double>("Gia");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("SachId");

                    b.ToTable("Sachs");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKeGiaoVienTheoThang", b =>
                {
                    b.Property<Guid>("ThongKeGiaoVienTheoThangId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<byte>("LoaiGiaoVien");

                    b.Property<int>("SoLuong");

                    b.HasKey("ThongKeGiaoVienTheoThangId");

                    b.ToTable("ThongKeGiaoVienTheoThangs");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKeHocVienTheoThang", b =>
                {
                    b.Property<Guid>("ThongKeHocVienTheoThangId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<byte>("LoaiHocVien");

                    b.Property<int>("SoLuong");

                    b.HasKey("ThongKeHocVienTheoThangId");

                    b.ToTable("ThongKeHocVienTheoThangs");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_ChiPhi", b =>
                {
                    b.Property<Guid>("ThongKe_ChiPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Bonus");

                    b.Property<double>("ChiPhi");

                    b.Property<Guid?>("ChiPhiCoDinhId");

                    b.Property<Guid?>("ChiPhiKhacId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("DaLuu");

                    b.Property<double>("DailySalary");

                    b.Property<bool>("IsDisabled");

                    b.Property<double>("Minus");

                    b.Property<double>("MucHoaHong");

                    b.Property<DateTime>("NgayChiPhi");

                    b.Property<string>("NgayLamViec");

                    b.Property<Guid?>("NhanVienId");

                    b.Property<double>("Salary_Expense");

                    b.Property<double>("SoGioDay");

                    b.Property<double>("SoGioKem");

                    b.Property<double>("SoHocVien");

                    b.Property<int>("SoNgayLam");

                    b.Property<int>("SoNgayLamVoSau");

                    b.Property<double>("SoNgayNghi");

                    b.Property<double>("TeachingRate");

                    b.Property<double>("TutoringRate");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("ThongKe_ChiPhiId");

                    b.HasIndex("ChiPhiCoDinhId");

                    b.HasIndex("ChiPhiKhacId");

                    b.HasIndex("NhanVienId");

                    b.ToTable("ThongKe_ChiPhis");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_DoanhThuHocPhi", b =>
                {
                    b.Property<Guid>("ThongKe_DoanhThuHocPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Bonus");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("DaDong");

                    b.Property<bool>("DaNo");

                    b.Property<string>("GhiChu");

                    b.Property<double>("HocPhi");

                    b.Property<Guid>("HocVienId");

                    b.Property<int>("KhuyenMai");

                    b.Property<Guid>("LopHocId");

                    b.Property<double>("Minus");

                    b.Property<DateTime>("NgayDong");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("ThongKe_DoanhThuHocPhiId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("LopHocId");

                    b.ToTable("ThongKe_DoanhThuHocPhis");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_DoanhThuHocPhi_TaiLieu", b =>
                {
                    b.Property<Guid>("ThongKe_DoanhThuHocPhi_TaiLieuId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("SachId");

                    b.Property<Guid>("ThongKe_DoanhThuHocPhiId");

                    b.HasKey("ThongKe_DoanhThuHocPhi_TaiLieuId");

                    b.HasIndex("SachId");

                    b.HasIndex("ThongKe_DoanhThuHocPhiId");

                    b.ToTable("ThongKe_DoanhThuHocPhi_TaiLieus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.GiaoVien", b =>
                {
                    b.HasOne("Up.Data.Entities.NgayLamViec", "NgayLamViec")
                        .WithMany("NhanViens")
                        .HasForeignKey("NgayLamViecId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien", b =>
                {
                    b.HasOne("Up.Data.Entities.QuanHe", "QuanHe")
                        .WithMany("HocViens")
                        .HasForeignKey("QuanHeId");
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien_LopHoc", b =>
                {
                    b.HasOne("Up.Data.Entities.HocVien", "HocVien")
                        .WithMany("HocVien_LopHocs")
                        .HasForeignKey("HocVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LopHoc", "LopHoc")
                        .WithMany("HocVien_LopHocs")
                        .HasForeignKey("LopHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien_NgayHoc", b =>
                {
                    b.HasOne("Up.Data.Entities.HocVien", "HocVien")
                        .WithMany("HocVien_NgayHocs")
                        .HasForeignKey("HocVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LopHoc", "LopHoc")
                        .WithMany("HocVien_NgayHocs")
                        .HasForeignKey("LopHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.HocVien_No", b =>
                {
                    b.HasOne("Up.Data.Entities.HocVien", "HocVien")
                        .WithMany("HocVien_Nos")
                        .HasForeignKey("HocVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LopHoc", "LopHoc")
                        .WithMany("HocVien_Nos")
                        .HasForeignKey("LopHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.LopHoc", b =>
                {
                    b.HasOne("Up.Data.Entities.GioHoc", "GioHoc")
                        .WithMany("LopHocs")
                        .HasForeignKey("GioHocId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.KhoaHoc", "KhoaHoc")
                        .WithMany("LopHocs")
                        .HasForeignKey("KhoaHocId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.NgayHoc", "NgayHoc")
                        .WithMany("LopHocs")
                        .HasForeignKey("NgayHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.LopHoc_DiemDanh", b =>
                {
                    b.HasOne("Up.Data.Entities.HocVien", "HocVien")
                        .WithMany("LopHoc_DiemDanhs")
                        .HasForeignKey("HocVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LopHoc", "LopHoc")
                        .WithMany("LopHoc_DiemDanhs")
                        .HasForeignKey("LopHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.LopHoc_HocPhi", b =>
                {
                    b.HasOne("Up.Data.Entities.HocPhi", "HocPhi")
                        .WithMany("LopHoc_HocPhis")
                        .HasForeignKey("HocPhiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LopHoc", "LopHoc")
                        .WithMany("LopHoc_HocPhis")
                        .HasForeignKey("LopHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.NhanVien_ViTri", b =>
                {
                    b.HasOne("Up.Data.Entities.LoaiCheDo", "CheDo")
                        .WithMany("NhanVien_ViTris")
                        .HasForeignKey("CheDoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.GiaoVien", "NhanVien")
                        .WithMany("NhanVien_ViTris")
                        .HasForeignKey("NhanVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LoaiGiaoVien", "ViTri")
                        .WithMany("NhanVien_ViTris")
                        .HasForeignKey("ViTriId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.Quyen_Role", b =>
                {
                    b.HasOne("Up.Data.Entities.Quyen", "Quyen")
                        .WithMany("Quyen_Roles")
                        .HasForeignKey("QuyenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_ChiPhi", b =>
                {
                    b.HasOne("Up.Data.Entities.ChiPhiCoDinh", "ChiPhiCoDinh")
                        .WithMany("ThongKe_ChiPhis")
                        .HasForeignKey("ChiPhiCoDinhId");

                    b.HasOne("Up.Data.Entities.ChiPhiKhac", "ChiPhiKhac")
                        .WithMany("ThongKe_ChiPhis")
                        .HasForeignKey("ChiPhiKhacId");

                    b.HasOne("Up.Data.Entities.GiaoVien", "NhanVien")
                        .WithMany("ThongKe_ChiPhis")
                        .HasForeignKey("NhanVienId");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_DoanhThuHocPhi", b =>
                {
                    b.HasOne("Up.Data.Entities.HocVien", "HocVien")
                        .WithMany("ThongKe_DoanhThuHocPhis")
                        .HasForeignKey("HocVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.LopHoc", "LopHoc")
                        .WithMany("ThongKe_DoanhThuHocPhis")
                        .HasForeignKey("LopHocId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_DoanhThuHocPhi_TaiLieu", b =>
                {
                    b.HasOne("Up.Data.Entities.Sach", "Sach")
                        .WithMany("ThongKe_DoanhThuHocPhi_TaiLieus")
                        .HasForeignKey("SachId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.ThongKe_DoanhThuHocPhi", "ThongKe_DoanhThuHocPhi")
                        .WithMany("ThongKe_DoanhThuHocPhi_TaiLieus")
                        .HasForeignKey("ThongKe_DoanhThuHocPhiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
