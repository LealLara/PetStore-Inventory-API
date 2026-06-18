using FluentValidation;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Utils.Validations
{
    public class ProductValidation : AbstractValidator<ProductModel>
    {
        public ProductValidation()
        {
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(50).WithMessage("O nome do produto deve ter no máximo 50 caracteres.");

            RuleFor(product => product.ProductDescription)
                .MaximumLength(200).WithMessage("A descrição do produto deve ter no máximo 200 caracteres.");

            RuleFor(product => product.Price)
                .NotEmpty().WithMessage("O preço do produto é obrigatório.")
                .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");

            RuleFor(product => product.Quantity)
                .NotEmpty().WithMessage("A quantidade do produto é obrigatória.") 
                .Must(quantity => quantity > 0).WithMessage("A quantidade do produto deve ser maior que zero.");
        }
    }
}