using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SysComedorCADE.Startup))]
namespace SysComedorCADE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
