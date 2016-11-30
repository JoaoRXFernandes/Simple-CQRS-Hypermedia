using System.Collections.Generic;
using Nancy;
using Queries.Inventory;
using Web.Api.Infrastructure.Siren.Actions;
using Web.Api.Infrastructure.Siren.Links;
using Web.Api.Infrastructure.Siren.Siren;
using Web.Api.Modules.Inventory.Items.AddQuantity;
using Web.Api.Modules.Inventory.Items.RemoveQuantity;
using Web.Api.Modules.Inventory.Items.Rename;
using Web.Api.Modules.Inventory.List;

namespace Web.Api.Modules.Inventory.Items.Get
{
    public class InventoryItem : Entity
    {
        public InventoryItem(NancyContext context, InventoryItemDto item) 
            : base(context.Request.Url.ToString(), new [] { "item", "detail" })
        {
            Properties = new Dictionary<string, object>
            {
                { "Name", item.Name },
                { "Quantity", item.Quantity }
            };

            Links = new LinksFactory(context)
                            .With(new GetInventoryLink(), WithLink<GetInventoryLink>.Property(x => x.Title = "Back to inventory"))
                            .Build();

            Actions = new ActionsFactory(context)
                            .With(new AddInventoryItemQuantityAction(item.Id, item.Version),
                                        WithAction<AddInventoryItemQuantityAction>
                                            .Field(x => x.Version)
                                                .Having(x => x.Type = "hidden"))
                            .With(new RemoveInventoryItemQuantityAction(item.Id, item.Version),
                                        WithAction<RemoveInventoryItemQuantityAction>
                                            .Field(x => x.Version)
                                                .Having(x => x.Type = "hidden"))
                            .With(new RenameInventoryItemAction(item.Id, item.Version, item.Name), 
                                        WithAction<RenameInventoryItemAction>
                                            .Field(x => x.Version)
                                                .Having(x => x.Type = "hidden"))
                            .Build();
        }
    }
}