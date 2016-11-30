using Web.Api.Infrastructure.Requests.Actions;

namespace Web.Api.Modules.Inventory.Add
{
    public class AddInventoryItemAction : ApiAction
    {
        public string Name { get; set; }

        public AddInventoryItemAction() : base("Add Item", "POST", "/api/inventory")
        {
        }
    }
}