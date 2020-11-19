using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Peoples.Dal.Startup))]
namespace Peoples.Dal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
