using PetStore.Inventory.Application.BusinessDTOs.Results;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IAccessRegisterRepository
    {
        Task<DataRegisterResult> CreateAccessRegister(UserEntity request);
        Task<DataRegisterResult> Login(LoginEntity request);
        Task<bool> Logoff(int userId);
        Task<bool> RemoveUser(int userId);

    }
}