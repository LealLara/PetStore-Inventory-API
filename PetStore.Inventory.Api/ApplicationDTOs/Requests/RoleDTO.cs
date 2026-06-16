using PetStore.Inventory.Application.ApplicationModel.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class RoleDTO
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public RoleDTO() { }
        public RoleRequest ToBusinessRequest()
        {
            return new(
                RoleName,
                RoleDescription,
                true
            );
        }
    }
}