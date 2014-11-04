using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test_converter.Startup))]
namespace test_converter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
