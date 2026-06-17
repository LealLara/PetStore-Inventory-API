using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Results;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IAccessRegisterServices
    {
        Task<bool> CreateAccessRegister(UserRegisterRequest request);
        Task<DataRegisterResult> Login(LoginRegisterRequest request);
        Task<bool> Logoff(int userId);
        Task<bool> RemoveUser(int userId);
    }
}