namespace PetStore.Inventory.Domain.BusinessModel
{
    public class UserDataToSendLoginEmailModel
    {
        public UserRegisterModel Register { get; set; }
        public LoginModel LoginData { get; set; }

        public string Token { get; set; }

        public UserDataToSendLoginEmailModel(){}
        public UserDataToSendLoginEmailModel(UserRegisterModel register, LoginModel loginData, string token)
        {
            Register = register;
            LoginData = loginData;
            Token = token;
        }
    }
}