using Events.Inventory;
using Infrastructure.CQRS.Events.Subscribers;
using Infrastructure.CQRS.ReadStore.DocumentDb;
using Queries.Inventory;

namespace Subscribers.Inventory
{
    public class InventoryListSubscriber : ISubscriber<InventoryItemCreated>, ISubscriber<InventoryItemRemoved>, ISubscriber<InventoryItemRenamed>, ISubscriber<InventoryItemQuantityRemoved>, ISubscriber<InventoryItemQuantityAdded>
    {
        private readonly IDocumentDb _readDocumentStore;

        public InventoryListSubscriber(IDocumentDb readDocumentStore)
        {
            _readDocumentStore = readDocumentStore;
        }

        public void Handle(InventoryItemCreated @event)
        {
            _readDocumentStore.Documents["Inventory"].Add(@event.Id, new InventoryListItemDto(@event.Id, 0, @event.Name));
        }

        public void Handle(InventoryItemRenamed @event)
        {
            var inventoryItem = _readDocumentStore.Documents["Inventory"][@event.Id];

            inventoryItem.Name = @event.NewName;
            inventoryItem.Version = @event.Version;
        }

        public void Handle(InventoryItemQuantityRemoved @event)
        {
            _readDocumentStore.Documents["Inventory"][@event.Id].Version = @event.Version;
        }

        public void Handle(InventoryItemQuantityAdded @event)
        {
            _readDocumentStore.Documents["Inventory"][@event.Id].Version = @event.Version;
        }

        public void Handle(InventoryItemRemoved @event)
        {
            _readDocumentStore.Documents["Inventory"].Remove(@event.Id);
        }
    }
}