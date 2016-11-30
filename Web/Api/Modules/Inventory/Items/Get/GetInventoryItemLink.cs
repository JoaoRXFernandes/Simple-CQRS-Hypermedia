using System;
using Web.Api.Infrastructure.Requests.Links;

namespace Web.Api.Modules.Inventory.Items.Get
{
    public class GetInventoryItemLink : ApiLink
    {
        public Guid Id { get; set; }

        public GetInventoryItemLink() : base("Item", "/api/inventory/{id}", new[] { "inventory-item", "detail" })
        {
        }

        public GetInventoryItemLink(Guid id) : this() 
        {
            Id = id;
        }
    }
}