using System;
using Events.Inventory;
using Infrastructure.CQRS.Domain;
using Infrastructure.CQRS.Domain.Exceptions;

namespace Domain.Inventory
{
    public class InventoryItem : AggregateRoot
    {
        public Guid _id;
        private bool _activated;
        private int _quantity;

        public override Guid Id => _id;


        public InventoryItem()
        {
        }

        public InventoryItem(Guid id, string name)
        {
            ApplyChange(new InventoryItemCreated(id, name));
        }


        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new DomainException("Name can't be empty");

            ApplyChange(new InventoryItemRenamed(_id, newName));
        }

        public void AddQuantity(int quantity)
        {
            if(quantity <= 0)
                throw new DomainException("Can't add negative quantities...");

            ApplyChange(new InventoryItemQuantityAdded(_id, quantity));
        }

        public void RemoveQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Can't remove negative quantities...");

            if (_quantity - quantity < 0)
                throw new DomainException("Can't remove more quantity than on inventory...");

            ApplyChange(new InventoryItemQuantityRemoved(_id, quantity));
        }

        public void Deactivate()
        {
            if(!_activated)
                throw new DomainException("Item is already deactivated...");

            ApplyChange(new InventoryItemRemoved(_id));
        }


        private void Apply(InventoryItemCreated e)
        {
            _id = e.Id;
            _activated = true;
        }

        private void Apply(InventoryItemQuantityAdded e)
        {
            _quantity += e.Quantity;
        }

        private void Apply(InventoryItemQuantityRemoved e)
        {
            _quantity -= e.Quantity;
        }

        private void Apply(InventoryItemRemoved e)
        {
            _activated = false;
        }
    }
}