using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(salesjs.Backend.Startup))]
namespace salesjs.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
