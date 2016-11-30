using System;
using Infrastructure.CQRS.Queries;

namespace Queries.Inventory
{
    public class InventoryItemQuery : IQuery<InventoryItemDto>
    {
        public Guid Id { get; set; }
    }
}