using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserRegisterModel>> GetAllUsers();

        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters);

        Task<UserRegisterModel> GetUsersFilteredById(int id);

        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int roleId);

        Task<UserRegisterModel> CreateUser(UserEntity userData);

        Task<bool> CreatePatternUsers(List<UserEntity> userEntities);
        Task<UserRegisterModel> GetUserFilteredByEmail(string filters);
        Task<UserRegisterModel> GetUserFilteredByNickname(string filters);
        Task<bool> RemoveUser(int userId);
    }
}