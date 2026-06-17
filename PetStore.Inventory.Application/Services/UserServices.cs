using FluentValidation;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Interfaces.Services;
using PetStore.Inventory.Domain.Utils.Validations;

namespace PetStore.Inventory.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IRoleServices _roleServices;

        public UserServices(IUserRepository userRepository, IRoleServices roleServices)
        {
            _repository = userRepository;
            _roleServices = roleServices;
        }

        public async Task<IEnumerable<UserRegisterModel>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }

        public async Task<bool> CreatePatternUsers()
        {
            IEnumerable<UserRegisterModel> userList = await GetAllUsers();
            if (!userList.Any())
            {
                UserRegisterRequest request = new();
                return await _repository.CreatePatternUsers(request.SetPatternUsers().Select(userModel => userModel.ToEntity()).ToList());
            }
            return false;
        }

        public async Task<bool> CreateUser(UserRegisterRequest userRequest)
        {
            IEnumerable<RoleModel> roleList = await _roleServices.GetAllRoles();
            if (!roleList.Any())
            {
                throw new Exception("Inicie a aplicação antes de criar usuários.");
            }

            UserValidation valid = new();

            valid.ValidateAndThrow(userRequest.ToModel());

            return await _repository.CreateUser(userRequest.ToEntity());
        }

        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredById(int userId)
        {
            return await _repository.GetUsersFilteredById(userId);
        }
        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int roleId)
        {
            return await _repository.GetUsersFilteredByRoleId(roleId);
        }

        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters)
        {
            return await _repository.GetUsersFilteredByString(filters);
        }
         
    }
}