using System.Linq;
using Commands.Inventory;
using Infrastructure.CQRS.Commands.Bus;
using Infrastructure.CQRS.Queries.Processor;
using Nancy;
using Nancy.ModelBinding;
using Queries.Inventory;
using Web.Api.Infrastructure.Siren.Responses;
using Web.Api.Modules.Inventory.Add;
using Web.Api.Modules.Inventory.Items.AddQuantity;
using Web.Api.Modules.Inventory.Items.Get;
using Web.Api.Modules.Inventory.Items.RemoveQuantity;
using Web.Api.Modules.Inventory.List;
using Web.Api.Modules.Inventory.Remove;

namespace Web.Api.Modules.Inventory
{
    public class InventoryModule : NancyModule
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public InventoryModule(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;

            Get["/api/inventory"] = _ => GetInventory(this.BindAndValidate<GetInventoryLink>());
            Get["/api/inventory/{id}"] = _ => GetInventoryItem(this.BindAndValidate<GetInventoryItemLink>());

            Post["/api/inventory"] = _ => AddInventoryItem(this.BindAndValidate<AddInventoryItemAction>());
            Delete["/api/inventory/{id}/{version}"] = _ => RemoveInventoryItem(this.BindAndValidate<RemoveInventoryItemAction>());

            Post["/api/inventory/{id}/name"] = _ => RenameInventoryItem(this.BindAndValidate<Items.Rename.RenameInventoryItemAction>());

            Post["/api/inventory/{id}/quantity/add"] = _ => AddInventoryItemQuantity(this.BindAndValidate<AddInventoryItemQuantityAction>());
            Post["/api/inventory/{id}/quantity/remove"] = _ => RemoveInventoryItemQuantity(this.BindAndValidate<RemoveInventoryItemQuantityAction>());
        }

        private dynamic GetInventory(GetInventoryLink request)
        {
            return new InventoryCollection(Context, _queryProcessor.Process(new InventoryListQuery()).ToList());
        }

        private dynamic GetInventoryItem(GetInventoryItemLink request)
        {
            return new InventoryItem(Context, _queryProcessor.Process(new InventoryItemQuery { Id = request.Id }));
        }

        private dynamic AddInventoryItem(AddInventoryItemAction request)
        {
            _commandBus.Send(new CreateInventoryItem { Name = request.Name });
            return new SuccessResponse(Context);
        }

        private dynamic RemoveInventoryItem(RemoveInventoryItemAction request)
        {
            _commandBus.Send(new RemoveInventoryItem { InventoryItemId = request.Id, OriginalVersion = request.Version });
            return new SuccessResponse(Context);
        }

        private dynamic RenameInventoryItem(Items.Rename.RenameInventoryItemAction request)
        {
            _commandBus.Send(new RenameInventoryItem { InventoryItemId = request.Id, OriginalVersion = request.Version, NewName = request.NewName });
            return new SuccessResponse(Context);
        }

        private dynamic AddInventoryItemQuantity(AddInventoryItemQuantityAction request)
        {
            _commandBus.Send(new AddInventoryItemQuantity { InventoryItemId = request.Id, OriginalVersion = request.Version, Quantity = request.Quantity});
            return new SuccessResponse(Context);
        }

        private dynamic RemoveInventoryItemQuantity(RemoveInventoryItemQuantityAction request)
        {
            _commandBus.Send(new RemoveInventoryItemQuantity { InventoryItemId = request.Id, OriginalVersion = request.Version, Quantity = request.Quantity });
            return new SuccessResponse(Context);
        }
    }
}
