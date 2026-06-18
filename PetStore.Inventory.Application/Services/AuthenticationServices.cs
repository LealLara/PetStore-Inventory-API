using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel; 
using PetStore.Inventory.Domain.Utils.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetStore.Inventory.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {

        private readonly IUserServices _userServices;
        private readonly string _key;
        public AuthenticationServices(IConfiguration configuration, IUserServices userServices)
        {
            _key = configuration["Jwt:Key"];
            _userServices = userServices;
        }

        public async Task<UserDataToSendLoginEmailModel> GenerateJwt(LoginModel login)
        {
            JwtSecurityTokenHandler handler = new();

            byte[] key = Encoding.ASCII.GetBytes(_key);

            UserRegisterModel user = await _userServices.GetUsersFilteredById(login.UserId);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, login.UserId.ToString()),
            new Claim(ClaimTypes.Name, login.Nickname),
            new Claim(
                ClaimTypes.Role,
                ((EUserRoles)user.RoleId).ToString())
                ]),

                Expires = DateTime.UtcNow.AddHours(8),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);

            string createdToken = handler.WriteToken(token);

            return new UserDataToSendLoginEmailModel(
               register: user,
               loginData: login,
               token: createdToken
            );
        }
    }
}
