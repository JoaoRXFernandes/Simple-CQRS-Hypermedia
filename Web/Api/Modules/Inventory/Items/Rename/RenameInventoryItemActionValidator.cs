using FluentValidation;

namespace Web.Api.Modules.Inventory.Items.Rename
{
    public class RenameInventoryItemActionValidator : AbstractValidator<RenameInventoryItemAction>
    {
        public RenameInventoryItemActionValidator()
        {
            RuleFor(x => x.NewName).NotNull().NotEmpty();
            RuleFor(x => x.Version).NotNull();
        }
    }
}