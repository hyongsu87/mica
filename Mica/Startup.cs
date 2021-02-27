using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mica.Startup))]
namespace Mica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
