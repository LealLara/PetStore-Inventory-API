using Moq;
using FluentAssertions;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Application.Services;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Tests.Services
{
    public class OrderServicesTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly OrderServices _orderServices;

        public OrderServicesTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _orderServices = new OrderServices(_mockOrderRepository.Object, _mockProductRepository.Object);
        }

        #region CreateOrderAsync Tests

        [Fact]
        public async Task CreateOrderAsync_WithValidRequest_ShouldReturnOrderModel()
        {
            // Arrange
            var productId = 1;
            var quantity = 2;
            var orderId = 1;

            var productModel = new ProductModel
            {
                ProductId = productId,
                ProductName = "Ração Premium",
                Price = 100m,
                StockQuantity = 5
            };

            var orderCreateRequest = new OrderCreateRequest
            {
                CustomerDocument = "12345678900",
                SellerName = "João Silva",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductId = productId, Quantity = quantity }
                }
            };

            var expectedOrder = new OrderModel
            {
                OrderId = orderId,
                CustomerDocument = "12345678900",
                SellerName = "João Silva",
                TotalAmount = 200m,
                CreatedDate = DateTime.UtcNow
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(productId))
                .ReturnsAsync(productModel);

            _mockProductRepository
                .Setup(r => r.UpdateProduct(It.IsAny<ProductEntity>()))
                .ReturnsAsync(true);

            _mockOrderRepository
                .Setup(r => r.CreateOrder(It.IsAny<OrderEntity>(), It.IsAny<List<OrderItemEntity>>()))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _orderServices.CreateOrderAsync(orderCreateRequest);

            // Assert
            result.Should().NotBeNull();
            result.OrderId.Should().Be(orderId);
            result.CustomerDocument.Should().Be("12345678900");
            result.SellerName.Should().Be("João Silva");
            result.TotalAmount.Should().Be(200m);
        }

        [Fact]
        public async Task CreateOrderAsync_WithInsufficientStock_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var productId = 1;
            var productModel = new ProductModel
            {
                ProductId = productId,
                ProductName = "Ração Premium",
                Price = 100m,
                StockQuantity = 1
            };

            var orderCreateRequest = new OrderCreateRequest
            {
                CustomerDocument = "12345678900",
                SellerName = "João Silva",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductId = productId, Quantity = 5 }
                }
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(productId))
                .ReturnsAsync(productModel);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _orderServices.CreateOrderAsync(orderCreateRequest)
            );

            exception.Message.Should().Contain("Estoque insuficiente");
        }

        [Fact]
        public async Task CreateOrderAsync_WithNonexistentProduct_ShouldThrowArgumentException()
        {
            // Arrange
            var productId = 999;
            var orderCreateRequest = new OrderCreateRequest
            {
                CustomerDocument = "12345678900",
                SellerName = "João Silva",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductId = productId, Quantity = 1 }
                }
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(productId))
                .ReturnsAsync((ProductModel)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _orderServices.CreateOrderAsync(orderCreateRequest)
            );

            exception.Message.Should().Contain($"Produto com ID {productId} não encontrado");
        }

        [Fact]
        public async Task CreateOrderAsync_WithMultipleItems_ShouldCreateOrderWithTotalAmount()
        {
            // Arrange
            var product1 = new ProductModel { ProductId = 1, ProductName = "Ração", Price = 100m, StockQuantity = 10 };
            var product2 = new ProductModel { ProductId = 2, ProductName = "Brinquedo", Price = 50m, StockQuantity = 10 };

            var orderCreateRequest = new OrderCreateRequest
            {
                CustomerDocument = "12345678900",
                SellerName = "João Silva",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductId = 1, Quantity = 2 },
                    new OrderItemDTO { ProductId = 2, Quantity = 3 }
                }
            };

            var expectedOrder = new OrderModel
            {
                OrderId = 1,
                CustomerDocument = "12345678900",
                SellerName = "João Silva",
                TotalAmount = 350m
            };

            _mockProductRepository
                .SetupSequence(r => r.GetAllProductsFilteredById(It.IsAny<int>()))
                .ReturnsAsync(product1)
                .ReturnsAsync(product2);

            _mockProductRepository
                .Setup(r => r.UpdateProduct(It.IsAny<ProductEntity>()))
                .ReturnsAsync(true);

            _mockOrderRepository
                .Setup(r => r.CreateOrder(It.IsAny<OrderEntity>(), It.IsAny<List<OrderItemEntity>>()))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _orderServices.CreateOrderAsync(orderCreateRequest);

            // Assert
            result.TotalAmount.Should().Be(350m);
            _mockProductRepository.Verify(r => r.UpdateProduct(It.IsAny<ProductEntity>()), Times.Exactly(2));
        }

        #endregion

        #region GetAllOrdersAsync Tests

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<OrderModel>
            {
                new OrderModel { OrderId = 1, SellerName = "João", TotalAmount = 100m },
                new OrderModel { OrderId = 2, SellerName = "Maria", TotalAmount = 200m }
            };

            _mockOrderRepository
                .Setup(r => r.GetAllOrders())
                .ReturnsAsync(orders);

            // Act
            var result = await _orderServices.GetAllOrdersAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(o => o.OrderId == 1);
            result.Should().Contain(o => o.OrderId == 2);
        }

        [Fact]
        public async Task GetAllOrdersAsync_WhenEmpty_ShouldReturnEmptyList()
        {
            // Arrange
            _mockOrderRepository
                .Setup(r => r.GetAllOrders())
                .ReturnsAsync(new List<OrderModel>());

            // Act
            var result = await _orderServices.GetAllOrdersAsync();

            // Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region GetOrderByIdAsync Tests

        [Fact]
        public async Task GetOrderByIdAsync_WithValidId_ShouldReturnOrder()
        {
            // Arrange
            var orderId = 1;
            var expectedOrder = new OrderModel
            {
                OrderId = orderId,
                SellerName = "João Silva",
                TotalAmount = 100m
            };

            _mockOrderRepository
                .Setup(r => r.GetOrderById(orderId))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _orderServices.GetOrderByIdAsync(orderId);

            // Assert
            result.Should().NotBeNull();
            result.OrderId.Should().Be(orderId);
            result.SellerName.Should().Be("João Silva");
        }

        [Fact]
        public async Task GetOrderByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockOrderRepository
                .Setup(r => r.GetOrderById(999))
                .ReturnsAsync((OrderModel)null);

            // Act
            var result = await _orderServices.GetOrderByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region GetOrdersBySellerAsync Tests

        [Fact]
        public async Task GetOrdersBySellerAsync_WithValidSellerName_ShouldReturnOrders()
        {
            // Arrange
            var sellerName = "João Silva";
            var orders = new List<OrderModel>
            {
                new OrderModel { OrderId = 1, SellerName = sellerName, TotalAmount = 100m },
                new OrderModel { OrderId = 2, SellerName = sellerName, TotalAmount = 200m }
            };

            _mockOrderRepository
                .Setup(r => r.GetOrdersBySeller(sellerName))
                .ReturnsAsync(orders);

            // Act
            var result = await _orderServices.GetOrdersBySellerAsync(sellerName);

            // Assert
            result.Should().HaveCount(2);
            result.All(o => o.SellerName == sellerName).Should().BeTrue();
        }

        [Fact]
        public async Task GetOrdersBySellerAsync_WithNullSellerName_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _orderServices.GetOrdersBySellerAsync(null)
            );

            exception.Message.Should().Contain("obrigatório");
        }

        [Fact]
        public async Task GetOrdersBySellerAsync_WithEmptySellerName_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _orderServices.GetOrdersBySellerAsync("")
            );

            exception.Message.Should().Contain("obrigatório");
        }

        [Fact]
        public async Task GetOrdersBySellerAsync_WithWhitespaceSellerName_ShouldThrowArgumentException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _orderServices.GetOrdersBySellerAsync("   ")
            );

            exception.Message.Should().Contain("obrigatório");
        }

        [Fact]
        public async Task GetOrdersBySellerAsync_WithNonexistentSeller_ShouldReturnEmptyList()
        {
            // Arrange
            _mockOrderRepository
                .Setup(r => r.GetOrdersBySeller("Vendedor Inexistente"))
                .ReturnsAsync(new List<OrderModel>());

            // Act
            var result = await _orderServices.GetOrdersBySellerAsync("Vendedor Inexistente");

            // Assert
            result.Should().BeEmpty();
        }

        #endregion
    }
}
