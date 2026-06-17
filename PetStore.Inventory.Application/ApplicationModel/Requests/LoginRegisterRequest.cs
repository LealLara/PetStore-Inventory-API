using Microsoft.AspNetCore.Identity;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Constants;
using PetStore.Inventory.Domain.Utils.Enums;

namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class LoginRegisterRequest
    {
        public int LoginId { get; private set; }
        public string Nickname { get; private set; }
        public string Password { get; private set; }
        public int UserId { get; private set; }
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
        public LoginRegisterRequest(string nickname, string password, int userId) : this()
        {
            Nickname = nickname;
            Password = password;
            UserId = userId;

        }
        public LoginRegisterRequest(int id, string nickname, string password, int userId) : this()
        {
            LoginId = id;
            Nickname = nickname;
            Password = password;
            UserId = userId;

        }

        public List<LoginRegisterRequest> SetPatternLogin()
        {
            var hasher = new PasswordHasher<LoginRegisterRequest>();

            return
            [
                new()
        {
            Nickname = PatternUsersConfig.PatterAdminNickname,
            Password = hasher.HashPassword(null, PatternUsersConfig.PatterAdminPassword),
            UserId = (int)EPatternUserIds.AdminId
        },
        new()
        {
            Nickname = PatternUsersConfig.PatterSellerNickname,
            Password = hasher.HashPassword(null, PatternUsersConfig.PatterSellerPassword),
            UserId = (int)EPatternUserIds.SellerId
        },
        new()
        {
            Nickname = PatternUsersConfig.PatterSystemOperatorNickname,
            Password = hasher.HashPassword(null, PatternUsersConfig.PatterSystemOperatorPassword),
            UserId = (int)EPatternUserIds.SystemOperatorId
        }
            ];
        }

        public LoginRegisterRequest BuildLogin(string nickname, string password)
        {
            return new()
            {
                Nickname = nickname,
                Password = password
            };
        }

        public LoginRegisterRequest BuildLoginCreationData(string nickname, string password, int userId)
        {
            return new()
            {
                Nickname = nickname,
                Password = password,
                UserId = userId,
            };
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

        public LoginModel ToModel()
        {
            return new(
                LoginId,
                Nickname,
                Password,
                UserId
            );
        }

    /*    public string GenerateHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("A senha não pode ser nula ou vazia.", nameof(password));

            string? hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }
        public string SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            Password = GenerateHash(password);
            return Password;
        }*/
    }
}