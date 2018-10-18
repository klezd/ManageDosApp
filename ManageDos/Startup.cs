using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManageDos.Startup))]
namespace ManageDos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
