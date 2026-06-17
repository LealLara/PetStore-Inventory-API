using PetStore.Inventory.Domain.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class UserEntity
    { 
        [Key]
        public int UserId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Nickname { get; private set; }
        public string Password { get; private set; } 
        public int RoleId { get; private set; } 

        public UserEntity(){}

        public UserEntity(int id, string name, string email, string passwordHash, int roleId):this()
        {
            UserId = id;
            FullName = name;
            Nickname = name;
            Email = email;
            Password = passwordHash;
            RoleId = roleId;
        }
        public UserEntity(string name, string email, string passwordHash, int roleId):this()
        { 
            FullName = name;
            Nickname = name;
            Email = email;
            Password = passwordHash;
            RoleId = roleId;
        }
    }
}