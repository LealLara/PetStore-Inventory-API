using Microsoft.AspNetCore.Identity;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _repository;
        private readonly IEmailServices _emailServices;
        private readonly IAuthenticationServices _authenticationServices; // por enquanto, vou gerar o token sem email para nao disparar toda vez que for testar o login pois isso vai bloquear a minha conta disparadora

        public LoginServices(ILoginRepository loginRepository, IEmailServices emailServices, IAuthenticationServices authenticationServices)
        {
            _repository = loginRepository;
            _emailServices = emailServices;
            _authenticationServices = authenticationServices;
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

            return await _repository.Login(loginModel.ToEntity());
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

            // return await _emailServices.SetLoginEmail(login); // por enquanto vou bloquear o envio de email para evitar bloqueio da minha conta disparadora

            UserDataToSendLoginEmailModel dataToken = await _authenticationServices.GenerateJwt(login);
            if (dataToken is not null)
            {
                return $"Token gerado: {dataToken.Token}";
            }

            return string.Empty;
        }

        public async Task<IEnumerable<LoginModel>> GetAllLogins()
        {
            return await _repository.GetAllLogins();
        }
        public async Task<bool> RemoveAccount(int userId)
        {
            return await _repository.RemoveAccount(userId);
        }
    }
}