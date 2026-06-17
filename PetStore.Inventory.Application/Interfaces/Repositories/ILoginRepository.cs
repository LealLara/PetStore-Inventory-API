using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<bool> CreatePatternLogin(IEnumerable<LoginEntity> data);
        Task<LoginModel> CreateLogin(LoginEntity data);
        Task<string> Login(LoginEntity data);
        Task<bool> Logoff(int userId);
        Task<LoginModel?> GetByNickname(string nickname);
        Task<IEnumerable<LoginModel>> GetAllLogins();
    }
}