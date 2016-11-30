using System.Collections.Generic;
using Infrastructure.CQRS.Queries;

namespace Queries.Inventory
{
    public class InventoryListQuery : IQuery<IEnumerable<InventoryListItemDto>>
    {
    }
}