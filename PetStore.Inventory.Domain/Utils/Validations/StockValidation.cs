using FluentValidation;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Utils.Validations
{
    public class StockValidation : AbstractValidator<StockMovementModel>
    {
        public StockValidation()
        {
            RuleFor(x => x.InvoiceNumber)
                .NotEmpty().WithMessage("O número da nota fiscal é obrigatório.");


            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("A quantidade do produto é obrigatória.")
                .Must(quantity => quantity > 0).WithMessage("A quantidade do produto deve ser maior que zero.");

            RuleFor(x => x.ProductId)
                  .NotEmpty().WithMessage("O ID do produto é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do produto é obrigatório é obrigatório.");
        }
    }
}