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

        public async Task<UserRegisterModel> CreateUser(UserRegisterRequest userRequest)
        {
            IEnumerable<RoleModel> roleList = await _roleServices.GetAllRoles();
            if (!roleList.Any())
            {
                throw new Exception("Inicie a aplicação antes de criar usuários.");
            }

            UserValidation valid = new();

            valid.ValidateAndThrow(userRequest.ToModel());

            UserRegisterModel emailAreadyExists = await GetUserFilteredByEmail(userRequest.Email);

            if (emailAreadyExists is not null)
            {
                if (emailAreadyExists.Email == userRequest.Email)
                {
                    throw new Exception("O email informado já está em uso.");
                }
            }
            UserRegisterModel nicknameAreadyExists = await GetUserFilteredByNickname(userRequest.Nickname);

            if (nicknameAreadyExists is not null)
            {
                if (nicknameAreadyExists.Nickname == userRequest.Nickname)
                {
                    throw new Exception("O apelido informado já está em uso.");
                }

            }

            return await _repository.CreateUser(userRequest.ToEntity());
        }

        public async Task<UserRegisterModel> GetUsersFilteredById(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("O ID do usuário deve ser informado.");

            return await _repository.GetUsersFilteredById(userId);
        }
        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int roleId)
        {
            if (roleId <= 0)
                throw new ArgumentException("O ID de papel do usuário deve ser informado.");

            return await _repository.GetUsersFilteredByRoleId(roleId);
        }

        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters)
        {
            if (string.IsNullOrEmpty(filters))
                throw new ArgumentException("Devem ser informada uma string de filtro.");

            return await _repository.GetUsersFilteredByString(filters);
        }

        public async Task<UserRegisterModel> GetUserFilteredByEmail(string filters)
        {
            if (string.IsNullOrEmpty(filters))
                throw new ArgumentException("Email deve ser informado.");

            return await _repository.GetUserFilteredByEmail(filters);
        }

        public async Task<UserRegisterModel> GetUserFilteredByNickname(string filters)
        {
            if (string.IsNullOrEmpty(filters))
                throw new ArgumentException("Devem ser informada uma string de filtro.");

            return await _repository.GetUserFilteredByNickname(filters);
        }
    }
}