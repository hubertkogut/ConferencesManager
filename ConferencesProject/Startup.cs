using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConferencesProject.Startup))]
namespace ConferencesProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
