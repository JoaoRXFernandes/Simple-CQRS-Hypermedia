using System;
using Infrastructure.CQRS.Commands;

namespace Commands.Inventory
{
    public class RenameInventoryItem : Command {
        public Guid InventoryItemId { get; set; }
        public int OriginalVersion { get; set; }
        public string NewName { get; set; }
    }
}