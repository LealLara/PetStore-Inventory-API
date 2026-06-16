using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class RoleEntity
    {
        [Key]
        public int RoleId { get; private set; }
        public string RoleName { get; private set; }
        public string RoleDescription { get; private set; }
        public bool IsActive { get; private set; }


        public RoleEntity() { }
        public RoleEntity(string roleName, string roleDescription)
        {
            RoleName = roleName;
            RoleDescription = roleDescription;
        }
        public RoleEntity(string roleName, string roleDescription, bool isActive)
        { 
            RoleName = roleName;
            RoleDescription = roleDescription;
            IsActive = isActive;
        }
        public RoleEntity(int roleId, string roleName, string roleDescription, bool isActive)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleDescription = roleDescription;
            IsActive = isActive;
        }
    }
}