using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Up.Services;

namespace Up.IoC
{
    public static class LogicsRegister
    {
        public static void AddLogics(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IKhoaHocService, KhoaHocService>();
            services.AddScoped<IQuanHeService, QuanHeService>();
            services.AddScoped<INgayHocService, NgayHocService>();
            services.AddScoped<IGioHocService, GioHocService>();
            services.AddScoped<IHocPhiService, HocPhiService>();
            services.AddScoped<ISachService, SachService>();
            services.AddScoped<ILopHocService, LopHocService>();
            services.AddScoped<IHocVienService, HocVienService>();
            services.AddScoped<IGiaoVienService, GiaoVienService>();
            services.AddScoped<IDiemDanhService, DiemDanhService>();
            services.AddScoped<ILoaiGiaoVienService, LoaiGiaoVienService>();
            services.AddScoped<ILoaiCheDoService, LoaiCheDoService>();
            services.AddScoped<IThongKeService, ThongKeService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IChiPhiCoDinhService, ChiPhiCoDinhService>();
            services.AddScoped<INoService, NoService>();
            services.AddScoped<IThongKe_DoanhThuHocPhiService, ThongKe_DoanhThuHocPhiService>();
            services.AddScoped<IChiPhiService, ChiPhiService>();
            services.AddScoped<INgayLamViecService, NgayLamViecService>();
            services.AddScoped<IQuyenService, QuyenService>();
            services.AddScoped<IChiPhiKhacService, ChiPhiKhacService>();
            services.AddScoped<IHocPhiTronGoiService, HocPhiTronGoiService>();

            services.AddScoped<Converters.Converter>();
            services.AddScoped<Converters.EntityConverter>();
        }
    }
}
