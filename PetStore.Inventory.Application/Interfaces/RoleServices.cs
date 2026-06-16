using FluentValidation;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Utils.Validations;

namespace PetStore.Inventory.Application.Interfaces
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<bool> CreatePatternRoles()
        {
            IEnumerable<RoleModel> roleList = await GetAllRoles();
            if (!roleList.Any())
            {
                RoleRequest request = new();
                return await _roleRepository.CreatePatternRoles(request.SetPatternRoles().Select(roleModel => roleModel.ToEntity()).ToList());
            }
            return false;
        }
        public async Task<bool> CreateRole(RoleRequest roleRequest)
        { 
            RoleValidation valid = new(); 

            valid.ValidateAndThrow(roleRequest.ToModel());

            return await _roleRepository.CreateRole(roleRequest.ToEntity());
        }
        public async Task<IEnumerable<RoleModel>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task<IEnumerable<RoleModel>> GetRolesFilteredById(int filters)
        {
            return await _roleRepository.GetRolesFilteredById(filters);
        }

        public async Task<IEnumerable<RoleModel>> GetRolesFilteredByString(string filters)
        {
            return await _roleRepository.GetRolesFilteredByString(filters);
        }
    }
}