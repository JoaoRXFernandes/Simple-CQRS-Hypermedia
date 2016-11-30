using System;

namespace Queries.Inventory
{
    public class InventoryListItemDto
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }

        public InventoryListItemDto(Guid id, int version, string name)
        {
            Id = id;
            Version = version;
            Name = name;
        }
    }
}