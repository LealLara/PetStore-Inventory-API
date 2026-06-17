using PetStore.Inventory.Application.BusinessDTOs.Requests;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface ILoginServices
    {
        Task<string> Login(LoginRegisterRequest data);
        Task<bool> Logoff(int userId);
    }
}
