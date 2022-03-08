using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InteriorPlatform.Web.Areas.Identity.IdentityHostingStartup))]

namespace InteriorPlatform.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}