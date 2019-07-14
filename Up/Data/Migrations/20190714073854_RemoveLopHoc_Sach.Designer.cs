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
    [Migration("20190714073854_RemoveLopHoc_Sach")]
    partial class RemoveLopHoc_Sach
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
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
                            Id = "ea19b33d-a2ef-4919-b91f-efda405ac726",
                            ConcurrencyStamp = "662855fc-aeef-4bb7-bf86-fdbdb3c4ea58",
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

                    b.Property<Guid>("LoaiGiaoVienId");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<double>("TeachingRate");

                    b.Property<double>("TutoringRate");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("GiaoVienId");

                    b.HasIndex("LoaiGiaoVienId");

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

                    b.Property<bool>("IsAppend");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("NgaySinh");

                    b.Property<string>("ParentFacebookAccount");

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

            modelBuilder.Entity("Up.Data.Entities.LoaiGiaoVien", b =>
                {
                    b.Property<Guid>("LoaiGiaoVienId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

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

                    b.Property<Guid>("GiaoVienId");

                    b.Property<Guid>("GioHocId");

                    b.Property<Guid>("HocPhiId");

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

                    b.HasIndex("GiaoVienId");

                    b.HasIndex("GioHocId");

                    b.HasIndex("HocPhiId");

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

            modelBuilder.Entity("Up.Data.Entities.NhanVienKhac", b =>
                {
                    b.Property<Guid>("NhanVienKhacId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BasicSalary");

                    b.Property<string>("CMND");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DiaChi");

                    b.Property<string>("FacebookAccount");

                    b.Property<bool>("IsDisabled");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("NhanVienKhacId");

                    b.ToTable("NhanVienKhacs");
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

            modelBuilder.Entity("Up.Data.Entities.ThongKe_ChiPhi", b =>
                {
                    b.Property<Guid>("ThongKe_ChiPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("ChiPhi");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime>("NgayChiPhi");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("ThongKe_ChiPhiId");

                    b.ToTable("ThongKe_ChiPhis");
                });

            modelBuilder.Entity("Up.Data.Entities.ThongKe_DoanhThuHocPhi", b =>
                {
                    b.Property<Guid>("ThongKe_DoanhThuHocPhiId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<double>("HocPhi");

                    b.Property<Guid>("HocVienId");

                    b.Property<Guid>("LopHocId");

                    b.Property<DateTime>("NgayDong");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("ThongKe_DoanhThuHocPhiId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("LopHocId");

                    b.ToTable("ThongKe_DoanhThuHocPhis");
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
                    b.HasOne("Up.Data.Entities.LoaiGiaoVien", "LoaiGiaoVien")
                        .WithMany("GiaoViens")
                        .HasForeignKey("LoaiGiaoVienId")
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
                    b.HasOne("Up.Data.Entities.GiaoVien", "GiaoVien")
                        .WithMany("LopHocs")
                        .HasForeignKey("GiaoVienId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.GioHoc", "GioHoc")
                        .WithMany("LopHocs")
                        .HasForeignKey("GioHocId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Up.Data.Entities.HocPhi", "HocPhi")
                        .WithMany("LopHocs")
                        .HasForeignKey("HocPhiId")
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
#pragma warning restore 612, 618
        }
    }
}
