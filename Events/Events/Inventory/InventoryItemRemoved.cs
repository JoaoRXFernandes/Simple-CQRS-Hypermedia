using System;
using Infrastructure.CQRS.Events;

namespace Events.Inventory
{
    public class InventoryItemRemoved : Event {
        public Guid Id { get; set; }

        public InventoryItemRemoved(Guid id)
        {
            Id = id;
        }
    }
}