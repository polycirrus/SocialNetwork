using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialNetwork.MvcPresentation.Startup))]
namespace SocialNetwork.MvcPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
