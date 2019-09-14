
namespace Up
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Up.Data;
    using Up.Extensions;
    using Up.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Expiration = TimeSpan.FromHours(5);
                });

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IKhoaHocService, KhoaHocService>();
            services.AddScoped<IQuanHeService, QuanHeService>();
            services.AddScoped<INgayHocService, NgayHocService>();
            services.AddScoped<IGioHocService, GioHocService>();
            services.AddScoped<IHocPhiService, HocPhiService>();
            services.AddScoped<ISachService, SachService>();
            services.AddScoped<ILopHocService,LopHocService>();
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
            services.AddScoped<IThongKe_ChiPhiService, ThongKe_ChiPhiService>();
            services.AddScoped<INgayLamViecService, NgayLamViecService>();
            services.AddScoped<IQuyenService, QuyenService>();

            services.AddScoped<Read_HocPhi>();
            services.AddScoped<Read_NgayHoc>();
            services.AddScoped<Read_KhoaHoc>();
            services.AddScoped<Read_GioHoc>();
            services.AddScoped<Read_TaiLieu>();
            services.AddScoped<Read_LopHoc>();
            services.AddScoped<Read_HocVien>();
            services.AddScoped<Read_HocVien_Export>();
            services.AddScoped<Read_QuanHe>();
            services.AddScoped<Read_ViTriCongViec>();
            services.AddScoped<Read_CheDoHopTac>();
            services.AddScoped<Read_NgayLamViec>();
            services.AddScoped<Read_NhanVien>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            ApplicationDbInitializer.SeedUsers(userManager);

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
