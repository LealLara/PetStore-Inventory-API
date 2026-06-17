using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface ILoginServices
    {
        Task<bool> CreatePatternLogin();
        Task<LoginModel> CreateLogin(LoginRegisterRequest data);
        Task<string> Login(LoginRegisterRequest data); 
        Task<IEnumerable<LoginModel>> GetAllLogins(); 
    }
}