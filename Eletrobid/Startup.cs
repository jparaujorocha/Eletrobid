using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eletrobid.Startup))]
namespace Eletrobid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
