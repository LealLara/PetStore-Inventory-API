using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IStockMovementRepository
    {
        Task<StockMovementModel> AddStockMovement(StockMovementEntity entity);
        Task<IEnumerable<StockMovementModel>> GetMovementsByProductId(int productId);
        Task<ProductModel> GetProductWithStock(int productId); 
    }
}