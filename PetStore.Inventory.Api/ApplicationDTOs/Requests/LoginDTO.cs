using PetStore.Inventory.Application.BusinessDTOs.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class LoginDTO
    {
        public string Nickname { get; set; }
        public string Password { get; set; } = string.Empty;

        public LoginRegisterRequest ToBusiness()
        {
            return new LoginRegisterRequest(Nickname, Password);
        }
    }
}