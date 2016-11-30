using System;
using Infrastructure.CQRS.Events;

namespace Events.Inventory
{
    public class InventoryItemQuantityAdded : Event
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
 
        public InventoryItemQuantityAdded(Guid id, int quantity) {
            Id = id;
            Quantity = quantity;
        }
    }
}