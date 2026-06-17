using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Enums;

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
        public static IEnumerable<UserRegisterModel> CreateUsers(IQueryable<UserEntity> userEntities)
        {
            return userEntities?.Select(u => new UserRegisterModel
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Nickname = u.Nickname,
                Email = u.Email,
                Password = u.Password,
                RoleId = (EUserRoles)u.RoleId
            }) ?? Enumerable.Empty<UserRegisterModel>();
        }
        public static UserRegisterModel CreateUser(UserEntity userEntity)
        {
            if (userEntity == null)
                return null;

            return new UserRegisterModel
            {
                UserId = userEntity.UserId,
                FullName = userEntity.FullName,
                Nickname = userEntity.Nickname,
                Email = userEntity.Email,
                Password = userEntity.Password,
                RoleId = (EUserRoles)userEntity.RoleId
            };
        }
    }
}