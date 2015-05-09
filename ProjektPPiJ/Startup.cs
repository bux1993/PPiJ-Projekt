using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektPPiJ.Startup))]
namespace ProjektPPiJ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
