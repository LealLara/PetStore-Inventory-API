using PetStore.Inventory.Api.Controllers;

namespace PetStore.Inventory.Application.Services
{
    public class SalesPortalServices : ISalesPortalServices
    {
        private readonly ISalesPortalServices _salesPortalServices;
        public SalesPortalServices(ISalesPortalServices salesPortalServices)
        {
            _salesPortalServices = salesPortalServices;
        }






    }
}