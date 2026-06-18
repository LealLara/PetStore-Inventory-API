using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<bool> CreatePatternLogin(IEnumerable<LoginEntity> data);
        Task<LoginModel> Login(LoginEntity data); 
        Task<bool> RemoveAccount(int userId);
        Task<LoginModel?> GetByNickname(string nickname);
        Task<IEnumerable<LoginModel>> GetAllLogins();
        Task<LoginModel> GetLoginById(int userId);

    }
}