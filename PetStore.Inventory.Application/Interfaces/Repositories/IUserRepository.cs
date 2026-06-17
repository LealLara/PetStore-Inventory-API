using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserRegisterModel>> GetAllUsers();

        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters);

        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredById(int id);

        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int roleId);

        Task<bool> CreateUser(UserEntity userData);

        Task<bool> CreatePatternUsers(List<UserEntity> userEntities);
    }
}