using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderModel> CreateOrder(OrderEntity entity, List<OrderItemEntity> items);
        Task<IEnumerable<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderById(int orderId);
        Task<IEnumerable<OrderModel>> GetOrdersBySeller(string sellerName); 
    }
}