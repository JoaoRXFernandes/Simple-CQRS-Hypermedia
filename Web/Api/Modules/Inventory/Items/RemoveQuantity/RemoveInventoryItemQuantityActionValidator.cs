using FluentValidation;

namespace Web.Api.Modules.Inventory.Items.RemoveQuantity
{
    public class RemoveInventoryItemQuantityActionValidator : AbstractValidator<RemoveInventoryItemQuantityAction>
    {
        public RemoveInventoryItemQuantityActionValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Version).NotNull();
        }
    }
}