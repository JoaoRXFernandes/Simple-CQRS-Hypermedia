using System;
using Infrastructure.CQRS.Commands;

namespace Commands.Inventory
{
    public class AddInventoryItemQuantity : Command {
        public Guid InventoryItemId { get; set; }
        public int OriginalVersion { get; set; }
        public int Quantity { get; set; }
    }
}