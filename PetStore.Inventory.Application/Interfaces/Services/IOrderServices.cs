using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface IOrderServices
    {
        Task<OrderModel> CreateOrderAsync(OrderCreateRequest request);
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderModel>> GetOrdersBySellerAsync(string sellerName);
    }
}