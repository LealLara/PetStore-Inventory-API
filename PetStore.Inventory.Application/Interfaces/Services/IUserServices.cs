using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Interfaces.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<UserRegisterModel>> GetAllUsers();
        Task<bool> CreatePatternUsers();
        Task<bool> CreateUser(UserRegisterRequest userRequest);
        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredById(int userId);
        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int roleId);
        Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters);
        Task<UserRegisterModel> GetUserFilteredByEmail(string filters);

    }
}