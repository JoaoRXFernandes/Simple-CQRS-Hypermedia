using Commands.Inventory;
using Domain.Inventory;
using Infrastructure.CQRS.Commands.Handlers;
using Infrastructure.CQRS.Commands.Repository;

namespace Commands.Handlers.Inventory
{
    public class InventoryCommandHandlers : ICommandHandler<CreateInventoryItem>, ICommandHandler<RemoveInventoryItem>, ICommandHandler<RemoveInventoryItemQuantity>, ICommandHandler<AddInventoryItemQuantity>, ICommandHandler<RenameInventoryItem>
    {
        private readonly IRepository<InventoryItem> _repository;

        public InventoryCommandHandlers(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateInventoryItem message)
        {
            var item = new InventoryItem(message.InventoryItemId, message.Name);
            _repository.Save(item, -1);
        }

        public void Handle(RemoveInventoryItem message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, message.OriginalVersion);
        }

        public void Handle(RemoveInventoryItemQuantity message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.RemoveQuantity(message.Quantity);
            _repository.Save(item, message.OriginalVersion);
        }

        public void Handle(AddInventoryItemQuantity message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.AddQuantity(message.Quantity);
            _repository.Save(item, message.OriginalVersion);
        }

        public void Handle(RenameInventoryItem message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _repository.Save(item, message.OriginalVersion);
        }
    }
}
