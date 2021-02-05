using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Up.Areas.Identity.IdentityHostingStartup))]
namespace Up.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}