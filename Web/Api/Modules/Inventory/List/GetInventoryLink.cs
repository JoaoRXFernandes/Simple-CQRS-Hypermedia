using Web.Api.Infrastructure.Requests.Links;

namespace Web.Api.Modules.Inventory.List
{
    public class GetInventoryLink : ApiLinkPaged
    {
        public GetInventoryLink() : base("Inventory", "/api/inventory", new[] { "inventory", "collection" })
        {
        }
    }
}