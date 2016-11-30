using FluentValidation;

namespace Web.Api.Modules.Inventory.Remove
{
    public class RemoveInventoryItemActionValidator : AbstractValidator<RemoveInventoryItemAction>
    {
        public RemoveInventoryItemActionValidator()
        {
            RuleFor(x => x.Version).NotNull();
        }
    }
}