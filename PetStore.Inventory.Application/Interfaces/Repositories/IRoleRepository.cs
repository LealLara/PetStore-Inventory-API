using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> CreatePatternRoles(List<RoleEntity> roleEntities);
        Task<bool> CreateRole(RoleEntity roleRequest);
        Task<IEnumerable<RoleModel>> GetAllRoles();
        Task<IEnumerable<RoleModel>> GetRolesFilteredByString(string filters);
        Task<IEnumerable<RoleModel>> GetRolesFilteredById(int filters);
    }
}