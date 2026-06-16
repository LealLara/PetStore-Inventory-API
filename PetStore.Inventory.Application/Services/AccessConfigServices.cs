using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.Interfaces.Services;

namespace PetStore.Inventory.Application.Services
{
    public class AccessConfigServices : IAccessConfigServices
    {
        private readonly IAccessConfigRepository _accessConfigRepository;
        private readonly IRoleServices _roleServices;
        private readonly ILogTypeServices _logTypeServices  ;

        public AccessConfigServices(IAccessConfigRepository accessConfigRepository)
        {
            _accessConfigRepository = accessConfigRepository;
        }

        public async Task<bool> StartApp()
        {
            bool success = false;
            
           await CreatePatternRoles(); 

             

            return success;
        }

        public async Task<bool> CreatePatternRoles()
        { 
            return await _roleServices.CreatePatternRoles();
        }
        public async Task<bool> CreatePatternLogTypes()
        { 
            return await _logTypeServices.CreatePatternLogTypes();
        }
    }
}