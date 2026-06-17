using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class UserRegisterModel
    {
        public int UserId{ get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EUserRoles RoleId { get; set; }
        public UserRegisterModel(){}
        public UserRegisterModel(string fullName, string nickname, string email, string password, EUserRoles roleId)
        {
            FullName = fullName;
            Nickname = nickname;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
        public UserRegisterModel(int userId, string fullName, string nickname, string email, string password, EUserRoles roleId)
        {
            UserId = userId;
            FullName = fullName;
            Nickname = nickname;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
        public UserEntity ToEntity()
        {
            return new(
                id: UserId,
                name: FullName,
                email: Email,
                passwordHash: Password,
                roleId: (int)RoleId
            );
        }



    }
}