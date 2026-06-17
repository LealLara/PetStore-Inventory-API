using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class UserFirstRegisterDTO
    {
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public UserFirstRegisterDTO() { }
        public UserRegisterRequest ToBusinessRequest()
        {
            return new(
                FullName,
                Nickname,
                Email,
                Password,
                (EUserRoles)RoleId
            );
        }
    }
}