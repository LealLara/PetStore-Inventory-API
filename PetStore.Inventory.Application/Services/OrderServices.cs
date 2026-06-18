using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Interfaces.Repositories;

namespace PetStore.Inventory.Application.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderModel> CreateOrderAsync(OrderCreateRequest request)
        {
            // Validar dados básicos
            if (string.IsNullOrWhiteSpace(request.CustomerDocument))
                throw new ArgumentException("O documento do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(request.SellerName))
                throw new ArgumentException("O nome do vendedor é obrigatório.");

            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("O pedido deve conter pelo menos um item.");

            var orderItems = new List<OrderItemEntity>();
            decimal totalAmount = 0;

            // Validar estoque e preparar itens
            foreach (var itemRequest in request.Items)
            {
                // Buscar produto
                var product = await _productRepository.GetAllProductsFilteredById(itemRequest.ProductId);
                if (product == null)
                    throw new ArgumentException($"Produto com ID {itemRequest.ProductId} não encontrado.");

                // Validar estoque
                if (product.StockQuantity < itemRequest.Quantity)
                    throw new InvalidOperationException(
                        $"Estoque insuficiente para o produto '{product.ProductName}'. " +
                        $"Disponível: {product.StockQuantity}, Solicitado: {itemRequest.Quantity}");

                // Atualizar estoque
                var productEntity = product.ToEntity();
                productEntity.RemoveStock(itemRequest.Quantity);
                await _orderRepository.UpdateProductStock(productEntity);

                // Criar item do pedido
                var orderItem = new OrderItemEntity(
                    product.ProductId,
                    itemRequest.Quantity,
                    (decimal)product.Price
                );
                orderItems.Add(orderItem);
                totalAmount += orderItem.Subtotal;
            }

            // Criar pedido
            var orderEntity = new OrderEntity(
                request.CustomerDocument,
                request.SellerName,
                totalAmount,
                orderItems
            );

            // Salvar pedido
            var result = await _orderRepository.CreateOrder(orderEntity, orderItems);
            return result;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrders();
        }

        public async Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersBySellerAsync(string sellerName)
        {
            if (string.IsNullOrWhiteSpace(sellerName))
                throw new ArgumentException("O nome do vendedor é obrigatório.");

            return await _orderRepository.GetOrdersBySeller(sellerName);
        }
    }
}