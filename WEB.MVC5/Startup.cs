using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEB.MVC5.Startup))]
namespace WEB.MVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
