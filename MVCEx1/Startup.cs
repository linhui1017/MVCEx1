using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCEx1.Startup))]
namespace MVCEx1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
