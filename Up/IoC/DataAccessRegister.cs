﻿using Microsoft.Extensions.Configuration;
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
        }
    }
}
