using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheatreCompany.Startup))]
namespace TheatreCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
