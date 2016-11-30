using Nancy;
using Web.Api.Infrastructure.Siren.Links;
using Web.Api.Infrastructure.Siren.Siren;
using Web.Api.Modules.Inventory.List;

namespace Web.Api.Modules.Root.Get
{
    public class Root : Entity
    {
        public Root(NancyContext context) : base(context.Request.Url.ToString(), "root")
        {
            Links = new LinksFactory(context)
                            .With(new GetInventoryLink())
                            .Build();
        }
    }
}