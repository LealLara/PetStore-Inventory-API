using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Constants;

namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; } = false;

        public RoleRequest() { }


        public RoleRequest(string roleName, string roleDescription, bool isActive)
        {
            RoleName = roleName;
            RoleDescription = roleDescription;
            IsActive = isActive;
        }
        public RoleRequest(int roleId, string roleName, string roleDescription, bool isActive)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleDescription = roleDescription;
            IsActive = isActive;
        }

        public List<RoleModel> SetPatternRoles() =>
        [
            new ()
        {
            RoleName = PatternRoles.PatterRoleAdmin,
            RoleDescription = PatternRoles.PatterRoleAdminDescription,
            IsActive = true
        },
        new ()
        {
            RoleName = PatternRoles.PatterRoleSeller,
            RoleDescription = PatternRoles.PatterRoleSellerDescription,
            IsActive = true
        }
        ];

        public RoleModel ToModel()
        {
            return new(
                RoleId,
                RoleName,
                RoleDescription,
                IsActive
            );
        }
        public RoleEntity ToEntity()
        {
            return new(
                RoleId,
                RoleName,
                RoleDescription,
                IsActive
            );
        }
        public List<RoleEntity> ToEntityList(List<RoleModel> roleModels)
        {
            return roleModels.Select(roleModel => roleModel.ToEntity()).ToList();
        }
    }
}