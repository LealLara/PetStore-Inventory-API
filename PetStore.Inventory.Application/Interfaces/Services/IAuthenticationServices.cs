using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IAuthenticationServices
    {
        Task<UserDataToSendLoginEmailModel> GenerateJwt(LoginModel login);

    }
}