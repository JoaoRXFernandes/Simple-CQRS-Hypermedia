using System.Collections.Generic;
using Nancy;
using Queries.Inventory;
using Web.Api.Infrastructure.Siren.Actions;
using Web.Api.Infrastructure.Siren.Links;
using Web.Api.Infrastructure.Siren.Siren;
using Web.Api.Modules.Inventory.Items.Get;
using Web.Api.Modules.Inventory.Remove;

namespace Web.Api.Modules.Inventory.List
{
    public class InventoryCollectionItem : Entity
    {
        public InventoryCollectionItem(NancyContext context, InventoryListItemDto item)
            : base($"{context.Request.Url.SiteBase}{context.Request.Url.BasePath}{context.Request.Url.Path}/{item.Id}", "inventory-item")
        {
            Properties = new Dictionary<string, object>{ { "Name", item.Name } };
            Links = new LinksFactory(context).With(new GetInventoryItemLink(item.Id)).Build();

            Actions = new ActionsFactory(context)
                            .With(new RemoveInventoryItemAction(item.Id, item.Version),
                                        WithAction<RemoveInventoryItemAction>.Field(x => x.Version)
                                                        .Having(x => x.Type = "hidden"))
                            .Build();
        }
    }
}