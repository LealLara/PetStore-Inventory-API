using FluentValidation;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Domain.Utils.Validations
{
    public class UserValidation : AbstractValidator<UserRegisterModel>
    {
        public UserValidation()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("O nome completo é obrigatório.")
                .MaximumLength(30).WithMessage("O nome completo não pode exceder 30 caracteres.")
                .MinimumLength(3).WithMessage("O nome completo deve ter pelo menos 3 caracteres.");

            RuleFor(user => user.Nickname)
                .NotEmpty().WithMessage("O apelido é obrigatório.")
                .MaximumLength(20).WithMessage("O apelido não pode exceder 20 caracteres.")
                .MinimumLength(3).WithMessage("O apelido deve ter pelo menos 3 caracteres.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("O Email é obrigatório.")
                .EmailAddress().WithMessage("O formato de Email é inválido.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

            RuleFor(user => user.RoleId)
                  .NotEqual(EUserRoles.NONE)
                  .WithMessage("O papel de usuário deve ser informado.");

            RuleFor(user => user.RoleId)
                .Must(role => role != EUserRoles.SYSTEM_OPERATOR)
                .WithMessage("O papel de usuário não pode ser do tipo Operador do Sistema.");

        }
    }
}