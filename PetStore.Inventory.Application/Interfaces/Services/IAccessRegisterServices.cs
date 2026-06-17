using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IAccessRegisterServices
    {
        Task<UserRegisterModel> CreateAccessRegister(UserRegisterRequest request);
        Task<string> Login(LoginRegisterRequest request); 
        Task<bool> RemoveUser(int userId);
    }
}