using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOAN_WEB_1.Startup))]
namespace DOAN_WEB_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
