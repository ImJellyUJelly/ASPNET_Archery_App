using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPNET_Archery_Application.Startup))]
namespace ASPNET_Archery_Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
