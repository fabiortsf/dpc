using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stock.Web.Startup))]
namespace Stock.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
