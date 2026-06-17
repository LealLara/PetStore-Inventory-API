using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Results;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Interfaces.Services;

namespace PetStore.Inventory.Api.Controllers
{
    public class AccessRegisterServices : IAccessRegisterServices
    {
        private readonly IAccessRegisterRepository _repository;
        private readonly IUserServices _userServices;
        public AccessRegisterServices(IAccessRegisterRepository repository, IUserServices userServices)
        {
            _repository = repository;
            _userServices = userServices;
        }

        public async Task<bool> CreateAccessRegister(UserRegisterRequest request)
        {
            return await _userServices.CreateUser(request);
        } 

        public async Task<DataRegisterResult> Login(LoginRegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Logoff(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
