using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RadhekunkInfra.Startup))]
namespace RadhekunkInfra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
