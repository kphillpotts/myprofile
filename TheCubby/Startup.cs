using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheCubby.Startup))]
namespace TheCubby
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
