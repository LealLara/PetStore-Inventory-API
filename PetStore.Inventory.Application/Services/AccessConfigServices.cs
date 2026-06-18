using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services; 

namespace PetStore.Inventory.Application.Services
{
    public class AccessConfigServices : IAccessConfigServices
    {
        private readonly IAccessConfigRepository _accessConfigRepository;
        private readonly IRoleServices _roleServices;
        private readonly ILogTypeServices _logTypeServices;
        private readonly IUserServices _userServices;
        private readonly ILoginServices _loginServices;
        private readonly IProductServices _productServices;

        public AccessConfigServices(IAccessConfigRepository accessConfigRepository, IRoleServices roleServices, ILogTypeServices logTypeServices, IUserServices userServices, ILoginServices loginServices, IProductServices productServices)
        {
            _accessConfigRepository = accessConfigRepository;
            _roleServices = roleServices;
            _logTypeServices = logTypeServices;
            _userServices = userServices;
            _loginServices = loginServices;
            _productServices = productServices;
        }

        public async Task<bool> StartApp()
        {
            bool success = false;

            success = await CreatePatternRoles();

            if (success)
            {
                await CreatePatternLogTypes();
                await CreatePatternUsers();
                await CreatePatternLogins();
                await CreatePatternProducts();
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

        private async Task<bool> CreatePatternUsers()
        {
            return await _userServices.CreatePatternUsers();
        }
        private async Task<bool> CreatePatternLogins()
        {
            return await _loginServices.CreatePatternLogin();
        }
        private async Task<bool> CreatePatternProducts()
        {
            return await _productServices.CreatePatternProducts();
        }
}
}