using System;
using Infrastructure.CQRS.Events;

namespace Events.Inventory
{
    public class InventoryItemRenamed : Event
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
 
        public InventoryItemRenamed(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}