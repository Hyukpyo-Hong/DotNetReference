using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsyncEmail.Startup))]
namespace AsyncEmail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
