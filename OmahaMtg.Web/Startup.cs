using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OmahaMtg.Web.Startup))]
namespace OmahaMtg.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new OmahaMtg.Startup.Startup().ConfigureAuth(app);
        }
    }
}
