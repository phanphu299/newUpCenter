using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Up.Data.Entities;

namespace Up.Data
{
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
        public DbSet<LopHoc_Sach> LopHoc_Sachs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<HocVien> HocViens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //	...				

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Constants.Admin, NormalizedName = Constants.Admin.ToUpper() });

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

            builder.Entity<LopHoc>()
                .HasOne(p => p.HocPhi)
                .WithMany(b => b.LopHocs)
                .HasForeignKey(p => p.HocPhiId);

            // HOCVIEN RELATIONSHIPS
            builder.Entity<HocVien>()
                .HasOne(p => p.QuanHe)
                .WithMany(b => b.HocViens)
                .HasForeignKey(p => p.QuanHeId);
        }
    }
}
