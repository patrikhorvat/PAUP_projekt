using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PAUP_projekt.Startup))]
namespace PAUP_projekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
