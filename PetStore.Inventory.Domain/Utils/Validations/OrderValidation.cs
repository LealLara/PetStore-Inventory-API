using FluentValidation;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Utils.Validations
{
    public class OrderValidation : AbstractValidator<OrderModel>
    {
        public OrderValidation()
        {

            RuleFor(order => order.CustomerDocument)
                .NotEmpty().WithMessage("O documento do cliente é obrigatório.")
                .Length(9, 18).WithMessage("O documento deve ter entre 9 e 18 caracteres.");

            RuleFor(order => order.SellerName)
                .NotEmpty().WithMessage("O nome do vendedor é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do vendedor deve ter no máximo 100 caracteres.");

            RuleFor(order => order.Items)
                .NotNull().WithMessage("O pedido deve conter itens.")
                .Must(items => items != null && items.Any())
                .WithMessage("O pedido deve conter pelo menos um item.");

            RuleForEach(order => order.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProductId)
                        .GreaterThan(0).WithMessage("O ID do produto deve ser maior que zero.");

                    item.RuleFor(i => i.Quantity)
                        .GreaterThan(0).WithMessage("A quantidade de cada item deve ser maior que zero.");
                });
        }
    }
}