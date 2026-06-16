using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.Interfaces.Services;

namespace PetStore.Inventory.Application.Services
{
    public class AccessConfigServices : IAccessConfigServices
    {
        private readonly IAccessConfigRepository _accessConfigRepository;
        private readonly IRoleServices _roleServices;
        private readonly ILogTypeServices _logTypeServices;

        public AccessConfigServices(IAccessConfigRepository accessConfigRepository, IRoleServices roleServices, ILogTypeServices logTypeServices)
        {
            _accessConfigRepository = accessConfigRepository;
            _roleServices = roleServices;
            _logTypeServices = logTypeServices;
        }

        public async Task<bool> StartApp()
        {
            bool success = false;

            success = await CreatePatternRoles();

            if (success)
            {
                await CreatePatternLogTypes();
            }
            else
            {
                throw new Exception("Registros iniciais já estão criados. Prossiga a partir da criação de usuários.");
            }
            return success;
        }

        private async Task<bool> CreatePatternRoles()
        {
            return await _roleServices.CreatePatternRoles();
        }
        private async Task<bool> CreatePatternLogTypes()
        {
            return await _logTypeServices.CreatePatternLogTypes();
        }
    }
}