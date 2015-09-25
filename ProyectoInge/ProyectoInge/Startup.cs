using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoInge.Startup))]
namespace ProyectoInge
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
