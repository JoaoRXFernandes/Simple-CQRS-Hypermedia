using System;
using Web.Api.Infrastructure.Requests.Actions;

namespace Web.Api.Modules.Inventory.Remove
{
    public class RemoveInventoryItemAction : ApiAction
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public RemoveInventoryItemAction() : base("Delete", "DELETE", "/api/inventory/{id}/{version}")
        {
        }

        public RemoveInventoryItemAction(Guid id, int version) : base("Delete", "DELETE", "/api/inventory/{id}/{version}")
        {
            Id = id;
            Version = version;
        }
    }
}