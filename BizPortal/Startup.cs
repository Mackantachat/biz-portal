using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BizPortal.Startup))]
namespace BizPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

