using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HtmlPropertyHelperDemo.Startup))]
namespace HtmlPropertyHelperDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
