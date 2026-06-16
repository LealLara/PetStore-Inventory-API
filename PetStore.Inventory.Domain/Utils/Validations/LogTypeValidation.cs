using FluentValidation;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Utils.Validations
{
    public class LogTypeValidation : AbstractValidator<LogTypeModel>
    {
        public LogTypeValidation()
        {
            RuleFor(x => x.LogTypeName)
                .NotEmpty().WithMessage("Nome do tipo de log é obrigatório.")
                .MinimumLength(4).WithMessage("Nome do tipo de log deve ter no mínimo 4 caracteres.");

            RuleFor(x => x.LogTypeDescription)
                .NotEmpty().WithMessage("Descrição do tipo de log é obrigatória.")
                .MinimumLength(10).WithMessage("Descrição do tipo de log deve ter no mínimo 10 caracteres.");
        }
    }
}