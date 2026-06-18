using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IEmailServices
    {
        Task<string> SetLoginEmail(LoginModel login);
    }
}