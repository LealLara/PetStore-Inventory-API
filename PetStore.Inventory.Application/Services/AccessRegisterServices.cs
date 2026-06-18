using Microsoft.AspNetCore.Identity.Data;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel; 

namespace PetStore.Inventory.Api.Controllers
{
    public class AccessRegisterServices : IAccessRegisterServices
    {
        private readonly IAccessRegisterRepository _repository;
        private readonly IUserServices _userServices;
        private readonly ILoginServices _loginServices;
        public AccessRegisterServices(IAccessRegisterRepository repository, IUserServices userServices, ILoginServices loginServices)
        {
            _repository = repository;
            _userServices = userServices;
            _loginServices = loginServices;
        }

        public async Task<UserRegisterModel> CreateAccessRegister(UserRegisterRequest request)
        {
            UserRegisterModel? createdUser = await _userServices.CreateUser(request);

            if (createdUser != null && createdUser.UserId > 0)
            {
                LoginRegisterRequest loginRequest = new();

                LoginModel createdLogin = await _loginServices.CreateLogin(loginRequest.BuildLoginCreationData(request.Nickname, request.Password, createdUser.UserId));

                if (createdLogin != null)
                {
                    return createdUser;
                }
            }
            else
            {
                throw new Exception("Falha ao criar o usuário. O registro de acesso não foi criado.");
            }

            return null;

        }

        public async Task<string> Login(LoginRegisterRequest request)
        {
           return await _loginServices.Login(request.BuildLogin(request.Nickname, request.Password));
        }
         
        public async Task<bool> RemoveUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
