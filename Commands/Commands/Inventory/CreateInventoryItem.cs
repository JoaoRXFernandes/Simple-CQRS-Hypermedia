using System;
using Infrastructure.CQRS.Commands;

namespace Commands.Inventory
{
    public class CreateInventoryItem : Command {
        public Guid InventoryItemId { get; set; }
        public string Name { get; set; }

        public CreateInventoryItem()
        {
            InventoryItemId = Guid.NewGuid();
        }
    }
}