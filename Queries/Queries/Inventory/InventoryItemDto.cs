using System;

namespace Queries.Inventory
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public InventoryItemDto(Guid id, int version, string name, int quantity = 0)
        {
            Id = id;
            Version = version;
            Name = name;
            Quantity = quantity;
        }
    }
}