using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Interfaces.Repositories;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderModel> CreateOrder(OrderEntity entity, List<OrderItemEntity> items)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                // Salvar pedido
                _context.OrderTable.Add(entity);
                await _context.SaveChangesAsync();

                // Salvar itens
                foreach (var item in items)
                {
                    // Atualizar ProductId e OrderId
                    var itemEntity = new OrderItemEntity(item.ProductId, item.Quantity, item.UnitPrice);
                    // Usar reflexão ou construtor para definir OrderId
                    // Como a classe tem propriedades private, vamos usar um método ou ajustar
                }

                // Alternativa: Usar o entity com os itens já relacionados
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ModelFactory.CreateOrder(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao criar pedido: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            try
            {
                var orders = _context.OrderTable
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .OrderByDescending(o => o.CreatedAt);

                return ModelFactory.CreateOrders(orders);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao obter pedidos: {ex.Message}");
            }
        }

        public async Task<OrderModel> GetOrderById(int orderId)
        {
            try
            {
                var order = await _context.OrderTable
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                return ModelFactory.CreateOrder(order);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao obter pedido: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersBySeller(string sellerName)
        {
            try
            {
                var orders = _context.OrderTable
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Where(o => o.SellerName.Contains(sellerName))
                    .OrderByDescending(o => o.CreatedAt);

                return ModelFactory.CreateOrders(orders);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao obter pedidos do vendedor: {ex.Message}");
            }
        }

        public async Task<bool> UpdateProductStock(ProductEntity entity)
        {
            try
            {
                _context.ProductTable.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao atualizar estoque do produto: {ex.Message}");
            }
        }

        public async Task<ProductModel> GetProductWithStock(int productId)
        {
            try
            {
                var product = await _context.ProductTable
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                return ModelFactory.CreateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao obter produto com estoque: {ex.Message}");
            }
        }
    }
}