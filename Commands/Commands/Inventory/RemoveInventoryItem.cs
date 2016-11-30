using System;
using Infrastructure.CQRS.Commands;

namespace Commands.Inventory
{
    public class RemoveInventoryItem : Command {
        public Guid InventoryItemId { get; set; }
        public int OriginalVersion { get; set; }
    }
}