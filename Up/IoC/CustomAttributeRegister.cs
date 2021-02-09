using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Up.Extensions;

namespace Up.IoC
{
    public static class CustomAttributeRegister
    {
        public static void AddCustomAttributes(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Read_HocPhi>();
            services.AddScoped<Read_NgayHoc>();
            services.AddScoped<Read_KhoaHoc>();
            services.AddScoped<Read_GioHoc>();
            services.AddScoped<Read_TaiLieu>();
            services.AddScoped<Read_LopHoc>();
            services.AddScoped<Read_HocVien>();
            services.AddScoped<Read_QuanHe>();
            services.AddScoped<Read_ViTriCongViec>();
            services.AddScoped<Read_CheDoHopTac>();
            services.AddScoped<Read_NgayLamViec>();
            services.AddScoped<Read_NhanVien>();
            services.AddScoped<Read_ChiPhiCoDinh>();
            services.AddScoped<Read_No>();
            services.AddScoped<Read_TinhHocPhi>();
            services.AddScoped<Read_TinhLuong>();
            services.AddScoped<Read_DiemDanh>();
            services.AddScoped<Read_DiemDanh_Export>();
        }
    }
}
