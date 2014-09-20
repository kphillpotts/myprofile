using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackPOS.Startup))]
namespace StackPOS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
