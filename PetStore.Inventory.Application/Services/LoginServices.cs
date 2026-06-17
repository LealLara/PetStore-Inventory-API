using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Interfaces.Services;
using PetStore.Inventory.Domain.Utils.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetStore.Inventory.Application.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _repository;
        private readonly IUserServices _userServices;
        private readonly string _key;
        public LoginServices(ILoginRepository loginRepository, IConfiguration configuration, IUserServices userServices)
        {
            _repository = loginRepository;
            _key = configuration["Jwt:Key"];
            _userServices = userServices;
        }
        public async Task<bool> CreatePatternLogin()
        {
            LoginRegisterRequest request = new();

            bool success = await _repository.CreatePatternLogin(request.SetPatternLogin().Select(loginModel => loginModel.ToEntity()).ToList());

            return success ? success : throw new Exception("Falha ao criar os registros iniciais de login.");
        }
        public async Task<LoginModel> CreateLogin(LoginRegisterRequest request)
        {
            PasswordHasher<LoginModel> hasher = new();

            LoginModel loginModel = request.BuildLoginCreationData(request.Nickname, request.Password, request.UserId).ToModel();

            loginModel.Password = hasher.HashPassword(loginModel, request.Password);

            return await _repository.CreateLogin(loginModel.ToEntity());
        }
        public async Task<string?> Login(LoginRegisterRequest request)
        {
            PasswordHasher<LoginModel> hasher = new();

            if (request is null ||
                string.IsNullOrWhiteSpace(request.Nickname) ||
                string.IsNullOrWhiteSpace(request.Password))
                return null;

            LoginModel? login = await _repository.GetByNickname(request.Nickname.Trim());

            if (login is null)
                return null;

            PasswordVerificationResult result = hasher.VerifyHashedPassword(
             login,
             login.Password,
             request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return null;

            return await GenerateJwt(login);
        }
        private async Task<string> GenerateJwt(LoginModel login)
        {
            JwtSecurityTokenHandler handler = new();

            byte[] key = Encoding.ASCII.GetBytes(_key);

            UserRegisterModel user =  await _userServices.GetUsersFilteredById(login.UserId);

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

            return  handler.WriteToken(token);
        }

        public async Task<IEnumerable<LoginModel>> GetAllLogins()
        {
            return await _repository.GetAllLogins();
        }
    }
}