using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPizza.Startup))]
namespace MVCPizza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
