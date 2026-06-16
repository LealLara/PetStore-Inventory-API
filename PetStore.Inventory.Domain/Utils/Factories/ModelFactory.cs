using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.Utils.Factories
{
    public class ModelFactory
    {
        public static IEnumerable<RoleModel> CreateRoles(IQueryable<RoleEntity> roleEntities)
        {
            return roleEntities?.Select(r => new RoleModel
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                RoleDescription = r.RoleDescription,
                IsActive = r.IsActive
            }) ?? Enumerable.Empty<RoleModel>();
        }
        public static IEnumerable<LogTypeModel> CreateLogTypes(IQueryable<LogTypeEntity> logTypeEntities)
        {
            return logTypeEntities?.Select(l => new LogTypeModel
            {
                LogTypeId = l.LogTypeId,
                LogTypeName = l.LogTypeName,
                LogTypeDescription = l.LogTypeDescription
            }) ?? Enumerable.Empty<LogTypeModel>();
        }







    }
}