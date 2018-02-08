using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMYCWebsite.Startup))]
namespace LMYCWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
