using Events.Inventory;
using Infrastructure.CQRS.Events.Subscribers;
using Infrastructure.CQRS.ReadStore.DocumentDb;
using Queries.Inventory;

namespace Subscribers.Inventory
{
    public class InventoryItemSubscriber : ISubscriber<InventoryItemCreated>, ISubscriber<InventoryItemRemoved>, ISubscriber<InventoryItemRenamed>, ISubscriber<InventoryItemQuantityRemoved>, ISubscriber<InventoryItemQuantityAdded>
    {
        private readonly IDocumentDb _readDocumentStore;

        public InventoryItemSubscriber(IDocumentDb readDocumentStore)
        {
            _readDocumentStore = readDocumentStore;
        }

        public void Handle(InventoryItemCreated @event)
        {
            _readDocumentStore.Documents["InventoryItem"].Add(@event.Id, new InventoryItemDto(@event.Id, 0, @event.Name, 0));
        }

        public void Handle(InventoryItemRenamed @event)
        {
            InventoryItemDto d = _readDocumentStore.Documents["InventoryItem"][@event.Id];
            d.Name = @event.NewName;
            d.Version = @event.Version;
        }

        public void Handle(InventoryItemQuantityRemoved @event)
        {
            InventoryItemDto d = _readDocumentStore.Documents["InventoryItem"][@event.Id];
            d.Quantity -= @event.Quantity;
            d.Version = @event.Version;
        }

        public void Handle(InventoryItemQuantityAdded @event)
        {
            InventoryItemDto d = _readDocumentStore.Documents["InventoryItem"][@event.Id];
            d.Quantity += @event.Quantity;
            d.Version = @event.Version;
        }

        public void Handle(InventoryItemRemoved @event)
        {
            _readDocumentStore.Documents["InventoryItem"].Remove(@event.Id);
        }
    }
}