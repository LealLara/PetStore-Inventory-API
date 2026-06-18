using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class SalesPortalRepository : ISalesPortalRepository
    {
        private readonly AppDbContext _appDbContext;
        public SalesPortalRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }








    }
}