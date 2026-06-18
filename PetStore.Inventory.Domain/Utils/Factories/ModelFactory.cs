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
        public static IEnumerable<LoginModel> CreateLogins(IQueryable<LoginEntity> loginEntities)
        {
            return loginEntities?.Select(l => new LoginModel
            {
                LoginId = l.LoginId,
                Nickname = l.Nickname,
                Password = l.Password,
                UserId = l.UserId
            }) ?? Enumerable.Empty<LoginModel>();
        }
        public static IEnumerable<ProductModel> CreateProducts(IQueryable<ProductEntity> productEntities)
        {
            return productEntities?.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price
            }) ?? Enumerable.Empty<ProductModel>();
        }
        public static ProductModel CreateProduct(ProductEntity productEntity)
        {
            if (productEntity == null)
                return null;

            return new ProductModel
            {
                ProductId = productEntity.ProductId,
                ProductName = productEntity.ProductName,
                ProductDescription = productEntity.ProductDescription,
                Price = productEntity.Price
            };
        }

         
        public static LoginModel CreateLogin(LoginEntity loginEntity)
        {
            if (loginEntity == null)
                return null;

            return new ()
            {
                LoginId = loginEntity.LoginId,
                Nickname = loginEntity.Nickname,
                Password = loginEntity.Password,
                UserId = loginEntity.UserId
            };
        }

        public static UserRegisterModel CreateUser(UserEntity userEntity)
        {
            if (userEntity == null)
                return null;

            return new ()
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