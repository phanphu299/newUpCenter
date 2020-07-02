namespace Up.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Up.Data.Entities;

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<QuanHe> QuanHes { get; set; }
        public DbSet<NgayHoc> NgayHocs { get; set; }
        public DbSet<GioHoc> GioHocs { get; set; }
        public DbSet<HocPhi> HocPhis { get; set; }
        public DbSet<Sach> Sachs { get; set; }
        public DbSet<HocVien_LopHoc> HocVien_LopHocs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        //GIAO VIEN LA NHAN VIEN
        public DbSet<GiaoVien> GiaoViens { get; set; }
        //GIAO VIEN LA NHAN VIEN
        public DbSet<LoaiGiaoVien> LoaiGiaoViens { get; set; }
        public DbSet<LoaiCheDo> LoaiCheDos { get; set; }
        public DbSet<LopHoc_DiemDanh> LopHoc_DiemDanhs { get; set; }
        public DbSet<HocVien_NgayHoc> HocVien_NgayHocs { get; set; }
        public DbSet<ChiPhiCoDinh> ChiPhiCoDinhs { get; set; }
        public DbSet<HocVien_No> HocVien_Nos { get; set; }
        public DbSet<ThongKe_DoanhThuHocPhi> ThongKe_DoanhThuHocPhis { get; set; }
        public DbSet<ThongKe_ChiPhi> ThongKe_ChiPhis { get; set; }
        public DbSet<ThongKe_DoanhThuHocPhi_TaiLieu> ThongKe_DoanhThuHocPhi_TaiLieus { get; set; }
        public DbSet<NhanVien_ViTri> NhanVien_ViTris { get; set; }
        public DbSet<NgayLamViec> NgayLamViecs { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<Quyen_Role> Quyen_Roles { get; set; }
        public DbSet<LopHoc_HocPhi> LopHoc_HocPhis { get; set; }
        public DbSet<ThongKeGiaoVienTheoThang> ThongKeGiaoVienTheoThangs { get; set; }
        public DbSet<ThongKeHocVienTheoThang> ThongKeHocVienTheoThangs { get; set; }
        public DbSet<ChiPhiKhac> ChiPhiKhacs { get; set; }
        public DbSet<HocPhiTronGoi> HocPhiTronGois { get; set; }
        public DbSet<HocPhiTronGoi_LopHoc> HocPhiTronGoi_LopHocs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //	...				

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.Admin, NormalizedName = Constants.Admin.ToUpper() });

            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.Chart_DoanhThu, NormalizedName = Constants.Chart_DoanhThu.ToUpper() });
            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.Chart_GiaoVien, NormalizedName = Constants.Chart_GiaoVien.ToUpper() });
            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.Chart_HocVien, NormalizedName = Constants.Chart_HocVien.ToUpper() });
            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.Chart_No, NormalizedName = Constants.Chart_No.ToUpper() });

            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.TK_ChiPhi, NormalizedName = Constants.TK_ChiPhi.ToUpper() });
            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.TK_DoanhThu, NormalizedName = Constants.TK_DoanhThu.ToUpper() });
            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.TK_GiaoVien, NormalizedName = Constants.TK_GiaoVien.ToUpper() });
            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.TK_HocVien, NormalizedName = Constants.TK_HocVien.ToUpper() });

            // LOPHOC RELATIONSHIPS
            builder.Entity<LopHoc>()
                .HasOne(p => p.KhoaHoc)
                .WithMany(b => b.LopHocs)
                .HasForeignKey(p => p.KhoaHocId);

            builder.Entity<LopHoc>()
                .HasOne(p => p.NgayHoc)
                .WithMany(b => b.LopHocs)
                .HasForeignKey(p => p.NgayHocId);

            builder.Entity<LopHoc>()
                .HasOne(p => p.GioHoc)
                .WithMany(b => b.LopHocs)
                .HasForeignKey(p => p.GioHocId);

            // HOCVIEN RELATIONSHIPS
            builder.Entity<HocVien>()
                .HasOne(p => p.QuanHe)
                .WithMany(b => b.HocViens)
                .HasForeignKey(p => p.QuanHeId);
        }
    }
}
