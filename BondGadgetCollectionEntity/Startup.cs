using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BondGadgetCollectionEntity.Startup))]
namespace BondGadgetCollectionEntity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
