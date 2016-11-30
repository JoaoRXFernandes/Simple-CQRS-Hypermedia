using System;
using Web.Api.Infrastructure.Requests.Actions;

namespace Web.Api.Modules.Inventory.Items.Rename
{
    public class RenameInventoryItemAction : ApiAction
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string NewName { get; set; }

        public RenameInventoryItemAction() : base("Rename", "POST", "/api/inventory/{id}/name")
        {
        }

        public RenameInventoryItemAction(Guid id, int version, string name) : base("Rename", "POST", "/api/inventory/{id}/name")
        {
            Id = id;
            Version = version;
            NewName = name;
        }
    }
}