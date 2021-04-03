using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Up.Repositoties;

namespace Up.IoC
{
    public static class DataAccessRegister
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHocVienRepository, HocVienRepository>();
            services.AddScoped<INgayHocRepository, NgayHocRepository>();
            services.AddScoped<ILopHocRepository, LopHocRepository>();
            services.AddScoped<ISachRepository, SachRepository>();
            services.AddScoped<IKhoaHocRepository, KhoaHocRepository>();
            services.AddScoped<IHocPhiRepository, HocPhiRepository>();
            services.AddScoped<IGioHocRepository, GioHocRepository>();
            services.AddScoped<IQuanHeRepository, QuanHeRepository>();
            services.AddScoped<ILoaiGiaoVienRepository, LoaiGiaoVienRepository>();
            services.AddScoped<INgayLamViecRepository, NgayLamViecRepository>();
            services.AddScoped<IGiaoVienRepository, GiaoVienRepository>();
            services.AddScoped<ILoaiCheDoRepository, LoaiCheDoRepository>();
            services.AddScoped<IChiPhiCoDinhRepository, ChiPhiCoDinhRepository>();
            services.AddScoped<IChiPhiKhacRepository, ChiPhiKhacRepository>();
            services.AddScoped<IChiPhiRepository, ChiPhiRepository>();
            services.AddScoped<IDiemDanhRepository, DiemDanhRepository>();
            services.AddScoped<IThongKeRepository, ThongKeRepository>();
            services.AddScoped<INoRepository, NoRepository>();
            services.AddScoped<IThongKe_DoanhThuHocPhiRepository, ThongKe_DoanhThuHocPhiRepository>();
            services.AddScoped<IHocPhiTronGoiRepository, HocPhiTronGoiRepository>();
            services.AddScoped<IBienLaiRepository, BienLaiRepository>();
            services.AddScoped<IThuThachRepository, ThuThachRepository>();
        }
    }
}
