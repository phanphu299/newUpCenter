﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Up.Repositoties;

namespace Up.IoT
{
    public static class DataAccessRegister
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHocVienRepository, HocVienRepository>();
            services.AddScoped<INgayHocRepository, NgayHocRepository>();
        }
    }
}