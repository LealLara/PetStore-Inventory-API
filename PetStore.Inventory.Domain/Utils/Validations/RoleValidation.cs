using FluentValidation;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Utils.Validations
{
    public class RoleValidation : AbstractValidator<RoleModel>
    {

        public RoleValidation()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Nome do cargo é obrigatório.")
                .MinimumLength(4).WithMessage("Nome do cargo deve ter no mínimo 4 caracteres.");

            RuleFor(x => x.RoleDescription)
                .NotEmpty().WithMessage("Descrição do cargo é obrigatória.")
                .MinimumLength(10).WithMessage("Descrição do cargo deve ter no mínimo 10 caracteres.");

        }
    }
}