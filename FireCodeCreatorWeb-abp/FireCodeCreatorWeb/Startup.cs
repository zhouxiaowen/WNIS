using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FireCodeCreatorWeb.Startup))]
namespace FireCodeCreatorWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
