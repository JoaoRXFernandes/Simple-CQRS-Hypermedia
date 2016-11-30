using FluentValidation;

namespace Web.Api.Modules.Inventory.Items.AddQuantity
{
    public class AddInventoryItemQuantityActionValidator : AbstractValidator<AddInventoryItemQuantityAction>
    {
        public AddInventoryItemQuantityActionValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Version).NotNull();
        }
    }
}