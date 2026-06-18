using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IStockServices
    {
        Task<StockMovementModel> AddStockAsync(StockAddRequest request);
        Task<IEnumerable<StockMovementModel>> GetStockMovementsByProductId(int productId);
        Task<ProductModel> GetProductStock(int productId);
    }
}