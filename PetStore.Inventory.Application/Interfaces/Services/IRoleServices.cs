using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IRoleServices
    { 
        Task<bool> CreatePatternRoles();
        Task<bool> CreateRole(RoleRequest roleRequest);
        Task<IEnumerable<RoleModel>> GetAllRoles();
        Task<IEnumerable<RoleModel>> GetRolesFilteredByString(string filters);
        Task<IEnumerable<RoleModel>> GetRolesFilteredById(int filters);
    }
}