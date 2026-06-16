
 using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Application.BusinessDTOs.Requests
{
    public class DataRegisterRequest
    {
        public int RegisterId { get; private set; }
        public string RegisterName { get; private set; }
        public string RegisterNickname { get; private set; }
        public string RegisterEmail { get; private set; }
        public string RegisterPassword { get; private set; }
        public UserRole RoleId { get; private set; }

        public DataRegisterRequest() { }

        public DataRegisterRequest(string name, string email, string nickname, string password, UserRole role) : this()
        {
            RegisterName = name;
            RegisterEmail = email;
            RegisterNickname = nickname;
            RegisterPassword = password;
            RoleId = role;
        }

        public DataRegisterRequest(int Id, string name, string email, string nickname, string password, UserRole role) : this()
        {
            RegisterId = Id;
            RegisterName = name;
            RegisterEmail = email;
            RegisterNickname = nickname;
            RegisterPassword = password;
            RoleId = role;
        }
    }
}