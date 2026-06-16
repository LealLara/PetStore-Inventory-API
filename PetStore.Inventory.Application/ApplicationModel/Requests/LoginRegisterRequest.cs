namespace PetStore.Inventory.Application.BusinessDTOs.Requests
{
    public class LoginRegisterRequest
    {
        public int LoginId { get; private set; }
        public string Nickname { get; private set; }
        public string Password { get; private set; }
        public LoginRegisterRequest() { }
        public LoginRegisterRequest(string nickname, string password) : this()
        {
            Nickname = nickname;
            Password = password;
        }
        public LoginRegisterRequest(int id, string nickname, string password) : this()
        {
            LoginId = id;
            Nickname = nickname;
            Password = password;
        }
    }
}