using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AddItemToDB.Startup))]
namespace AddItemToDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
