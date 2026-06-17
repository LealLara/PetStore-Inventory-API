using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class LoginEntity
    {
        [Key]
        public int LoginId { get; private set; }
        public string Nickname { get; private set; }
        public string Password { get; private set; }
        public int UserId { get; private set; }


        public LoginEntity(){}
        public LoginEntity(int id, string nickname, string password) : this()
        {
            LoginId = id;
            Nickname = nickname;
            Password = password;
        }
        public LoginEntity(int id, string nickname, string password, int userId) : this()
        {
            LoginId = id;
            Nickname = nickname;
            Password = password;
            UserId = userId;
        }
        public LoginEntity( string nickname, string password, int userId):this()
        { 
            Nickname = nickname;
            Password = password;
            UserId = userId;
        }
        public LoginEntity(string nickname, string password) : this()
        {
            Nickname = nickname;
            Password = password;
        }
    }
}