using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PAUP_FS.Startup))]
namespace PAUP_FS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
