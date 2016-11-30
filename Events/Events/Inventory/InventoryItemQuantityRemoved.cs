using System;
using Infrastructure.CQRS.Events;

namespace Events.Inventory
{
    public class InventoryItemQuantityRemoved : Event
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
 
        public InventoryItemQuantityRemoved(Guid id, int quantity) {
            Id = id;
            Quantity = quantity;
        }
    }
}

