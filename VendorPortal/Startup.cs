using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VendorPortal.Startup))]
namespace VendorPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
