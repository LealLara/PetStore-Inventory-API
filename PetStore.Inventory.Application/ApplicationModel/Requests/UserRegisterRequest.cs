using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Constants;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class UserRegisterRequest
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EUserRoles RoleId { get; set; }


        public UserRegisterRequest() { }
        public UserRegisterRequest(string fullName, string nickname, string email, string password, EUserRoles roleId)
        {
            FullName = fullName;
            Nickname = nickname;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
        public UserRegisterRequest(int userId, string fullName, string nickname, string email, string password, EUserRoles roleId)
        {
            UserId = userId;
            FullName = fullName;
            Nickname = nickname;
            Email = email;
            Password = password;
            RoleId = roleId;
        }

        public List<UserRegisterModel> SetPatternUsers() =>
        [
         new UserRegisterModel()
         {
             FullName = PatternUsersConfig.PatterAdminFullName,
             Nickname = PatternUsersConfig.PatterAdminNickname,
             Email = PatternUsersConfig.PatterAdminEmail,
             Password = PatternUsersConfig.PatterAdminPassword,
             RoleId = EUserRoles.ADMIN
         },
         new UserRegisterModel()
         {
             FullName = PatternUsersConfig.PatterSellerFullName,
             Nickname = PatternUsersConfig.PatterSellerNickname,
             Email = PatternUsersConfig.PatterSellerEmail,
             Password = PatternUsersConfig.PatterSellerPassword,
             RoleId = EUserRoles.SELLER
         },
         new UserRegisterModel()
         {
             FullName = PatternUsersConfig.PatterSystemOperatorFullName,
             Nickname = PatternUsersConfig.PatterSystemOperatorNickname,
             Email = PatternUsersConfig.PatterSystemOperatorEmail,
             Password = PatternUsersConfig.PatterSystemOperatorPassword,
             RoleId = EUserRoles.SYSTEM_OPERATOR
         }
        ];

        public UserRegisterModel ToModel()
        {
            return new(
                UserId,
                FullName,
                Nickname,
                Email,
                Password,
                RoleId
            );
        }
        public UserEntity ToEntity()
        {
            return new(
                id: UserId,
                name: FullName,
                nickname: Nickname,
                email: Email,
                passwordHash: Password,
                roleId: (int)RoleId
            );
        }

        public List<UserEntity> ToEntityList(List<UserRegisterModel> userModels)
        {
            return userModels.Select(userModel => userModel.ToEntity()).ToList();
        }
     
         
    }
}