using System;
using Web.Api.Infrastructure.Requests.Actions;

namespace Web.Api.Modules.Inventory.Items.AddQuantity
{
    public class AddInventoryItemQuantityAction : ApiAction
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public int Quantity { get; set; }

        public AddInventoryItemQuantityAction() : base("Add Quantity", "POST", "/api/inventory/{id}/quantity/add")
        {
        }

        public AddInventoryItemQuantityAction(Guid id, int version) : base("Add Quantity", "POST", "/api/inventory/{id}/quantity/add")
        {
            Id = id;
            Version = version;
        }
    }
}