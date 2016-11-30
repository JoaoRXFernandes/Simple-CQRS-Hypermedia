using System.Collections.Generic;
using System.Linq;
using Infrastructure.CQRS.Queries.Handlers;
using Infrastructure.CQRS.ReadStore.DocumentDb;
using Queries.Inventory;

namespace Queries.Handlers.Inventory
{
    public class InventoryListQueryHandler : IQueryHandler<InventoryListQuery, IEnumerable<InventoryListItemDto>>
    {
        private readonly IDocumentDb _readDocumentStore;

        public InventoryListQueryHandler(IDocumentDb readDocumentStore)
        {
            _readDocumentStore = readDocumentStore;
        }

        public IEnumerable<InventoryListItemDto> Handle(InventoryListQuery query)
        {
            return _readDocumentStore.Documents["Inventory"].Values.Cast<InventoryListItemDto>();
        }
    }
}