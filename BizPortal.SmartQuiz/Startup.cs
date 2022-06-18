using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BizPortal.SmartQuizWeb.Startup))]
namespace BizPortal.SmartQuizWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
