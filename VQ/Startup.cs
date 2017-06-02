using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VQ.Startup))]
namespace VQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
