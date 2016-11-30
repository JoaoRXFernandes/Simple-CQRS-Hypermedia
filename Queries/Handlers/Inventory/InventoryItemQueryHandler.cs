using Infrastructure.CQRS.Queries.Handlers;
using Infrastructure.CQRS.ReadStore.DocumentDb;
using Queries.Inventory;

namespace Queries.Handlers.Inventory
{
    public class InventoryItemQueryHandler : IQueryHandler<InventoryItemQuery, InventoryItemDto>
    {
        private readonly IDocumentDb _readDocumentStore;

        public InventoryItemQueryHandler(IDocumentDb readDocumentStore)
        {
            _readDocumentStore = readDocumentStore;
        }
        public InventoryItemDto Handle(InventoryItemQuery request)
        {
            return _readDocumentStore.Documents["InventoryItem"][request.Id];
        }
    }
}