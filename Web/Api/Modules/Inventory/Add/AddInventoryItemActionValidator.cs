using FluentValidation;

namespace Web.Api.Modules.Inventory.Add
{
    public class AddInventoryItemActionValidator : AbstractValidator<AddInventoryItemAction>
    {
        public AddInventoryItemActionValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}