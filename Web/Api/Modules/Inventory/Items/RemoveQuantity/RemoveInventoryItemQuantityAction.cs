using System;
using Web.Api.Infrastructure.Requests.Actions;

namespace Web.Api.Modules.Inventory.Items.RemoveQuantity
{
    public class RemoveInventoryItemQuantityAction : ApiAction
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public int Quantity { get; set; }

        public RemoveInventoryItemQuantityAction() : base("Remove Quantity", "POST", "/api/inventory/{id}/quantity/remove")
        {
        }

        public RemoveInventoryItemQuantityAction(Guid id, int version) : base("Remove Quantity", "POST", "/api/inventory/{id}/quantity/remove")
        {
            Id = id;
            Version = version;
        }
    }
}