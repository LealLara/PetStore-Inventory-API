using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<UserRegisterModel>> GetAllUsers();
        Task<bool> CreatePatternUsers();
        Task<UserRegisterModel> CreateUser(UserRegisterRequest userRequest);
        Task<UserRegisterModel> GetUsersFilteredById(int userId);
        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int roleId);
        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters);
        Task<UserRegisterModel> GetUserFilteredByEmail(string filters);
        Task<UserRegisterModel> GetUserFilteredByNickname(string filters);
        Task<bool> RemoveUser(int userId);
    }
}