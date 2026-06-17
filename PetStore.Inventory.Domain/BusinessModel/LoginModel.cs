using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class LoginModel
    {
        public int LoginId { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }


        public LoginModel() { }
        public LoginModel(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
        }
        public LoginModel(int id, string nickname, string password)
        {
            LoginId = id;
            Nickname = nickname;
            Password = password;
        }
        public LoginModel(int loginId, string nickname, string password, int userId)
        {
            LoginId = loginId;
            Nickname = nickname;
            Password = password;
            UserId = userId;
        }


        public LoginEntity ToEntity()
        {
            return new(
                LoginId,
                Nickname,
                Password,
                UserId
            );
        }
        public List<LoginEntity> ToEntityList(List<LoginModel> loginModels)
        {
            return loginModels.Select(loginModel => loginModel.ToEntity()).ToList();
        }
    }
}