using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Queries.Inventory;
using Web.Api.Infrastructure.Siren.Actions;
using Web.Api.Infrastructure.Siren.Links;
using Web.Api.Infrastructure.Siren.Siren;
using Web.Api.Modules.Inventory.Add;

namespace Web.Api.Modules.Inventory.List
{
    public class InventoryCollection : Entity
    {
        public InventoryCollection(NancyContext context, ICollection<InventoryListItemDto> items) 
            : base(context.Request.Url.ToString(), new[] { "inventory", "collection" })
        {
            int pageNumber = context.Request.Query.PageNumber;
            int pageSize = context.Request.Query.PageSize;
            int totalEntries = items.Count;

            var booksPage = items.Skip(pageNumber * pageSize).Take(pageSize);

            Properties = new Dictionary<string, object> { { "Page Details", PagedProperties.GetPageDetails(pageNumber, pageSize, totalEntries) } };
            Entities = new List<Entity>(booksPage.Select(x => new InventoryCollectionItem(context, x)));
            Links = new LinksFactory(context).WithPage<GetInventoryLink>(pageNumber, pageSize, totalEntries).Build();
            Actions = new ActionsFactory(context).With(new AddInventoryItemAction()).Build();
        }
    }
}