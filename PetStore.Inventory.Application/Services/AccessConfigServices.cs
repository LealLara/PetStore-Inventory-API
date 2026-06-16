using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.Interfaces.Services;

namespace PetStore.Inventory.Application.Services
{
    public class AccessConfigServices : IAccessConfigServices
    {
        private readonly IAccessConfigRepository _accessConfigRepository;
        private readonly IRoleServices _roleServices;

        public AccessConfigServices(IAccessConfigRepository accessConfigRepository)
        {
            _accessConfigRepository = accessConfigRepository;
        }

        public async Task<bool> StartAppAsync()
        {
            bool success = false;
            
           await CreatePatternRoles(); 

             

            return success;
        }

        public async Task<bool> CreatePatternRoles()
        {
            //GetUserRoles


            return await _roleServices.CreatePatternRoles();

        }
    }
}