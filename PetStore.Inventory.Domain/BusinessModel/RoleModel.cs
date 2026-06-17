using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; } = false;

        public RoleModel() { }
        public RoleModel(string roleName, string roleDescription)
        {
            RoleName = roleName;
            RoleDescription = roleDescription;
        }
        public RoleModel(string roleName, string roleDescription, bool isActive)
        {
            RoleName = roleName;
            RoleDescription = roleDescription;
            IsActive = isActive;
        }
        public RoleModel(int roleId, string roleName, string roleDescription, bool isActive)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleDescription = roleDescription;
            IsActive = isActive;
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