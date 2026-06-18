using Moq;
using FluentAssertions;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Application.Services;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Tests.Services
{
    public class ProductServicesTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductServices _productServices;

        public ProductServicesTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productServices = new ProductServices(_mockProductRepository.Object);
        }

        #region CreatePatternProducts Tests

        [Fact]
        public async Task CreatePatternProducts_WhenNoProductsExist_ShouldCreatePatternProducts()
        {
            // Arrange
            _mockProductRepository
                .Setup(r => r.GetAllProducts())
                .ReturnsAsync(new List<ProductModel>());

            _mockProductRepository
                .Setup(r => r.CreatePatternProducts(It.IsAny<List<object>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _productServices.CreatePatternProducts();

            // Assert
            result.Should().BeTrue();
            _mockProductRepository.Verify(
                r => r.CreatePatternProducts(It.IsAny<List<object>>()),
                Times.Once
            );
        }

        [Fact]
        public async Task CreatePatternProducts_WhenProductsExist_ShouldReturnFalse()
        {
            // Arrange
            var existingProducts = new List<ProductModel>
            {
                new ProductModel { ProductId = 1, ProductName = "Ração" }
            };

            _mockProductRepository
                .Setup(r => r.GetAllProducts())
                .ReturnsAsync(existingProducts);

            // Act
            var result = await _productServices.CreatePatternProducts();

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region CreateProduct Tests

        [Fact]
        public async Task CreateProduct_WithValidRequest_ShouldCreateProduct()
        {
            // Arrange
            var productRequest = new ProductRequest
            {
                ProductName = "Ração Premium",
                Price = 100m,
                Description = "Alimento para cães",
                StockQuantity = 50
            };

            var createdProduct = new ProductModel
            {
                ProductId = 1,
                ProductName = "Ração Premium",
                Price = 100m,
                StockQuantity = 50
            };

            _mockProductRepository
                .Setup(r => r.CreateProduct(It.IsAny<object>()))
                .ReturnsAsync(createdProduct);

            // Act
            var result = await _productServices.CreateProduct(productRequest);

            // Assert
            result.Should().NotBeNull();
            result.ProductId.Should().Be(1);
            result.ProductName.Should().Be("Ração Premium");
            result.Price.Should().Be(100m);
        }

        #endregion

        #region GetAllProducts Tests

        [Fact]
        public async Task GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<ProductModel>
            {
                new ProductModel { ProductId = 1, ProductName = "Ração", Price = 100m },
                new ProductModel { ProductId = 2, ProductName = "Brinquedo", Price = 50m }
            };

            _mockProductRepository
                .Setup(r => r.GetAllProducts())
                .ReturnsAsync(products);

            // Act
            var result = await _productServices.GetAllProducts();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(p => p.ProductName == "Ração");
        }

        [Fact]
        public async Task GetAllProducts_WhenEmpty_ShouldReturnEmptyList()
        {
            // Arrange
            _mockProductRepository
                .Setup(r => r.GetAllProducts())
                .ReturnsAsync(new List<ProductModel>());

            // Act
            var result = await _productServices.GetAllProducts();

            // Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region GetAllProductsFilteredById Tests

        [Fact]
        public async Task GetAllProductsFilteredById_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var productId = 1;
            var product = new ProductModel
            {
                ProductId = productId,
                ProductName = "Ração Premium",
                Price = 100m
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _productServices.GetAllProductsFilteredById(productId);

            // Assert
            result.Should().NotBeNull();
            result.ProductId.Should().Be(productId);
        }

        [Fact]
        public async Task GetAllProductsFilteredById_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(999))
                .ReturnsAsync((ProductModel)null);

            // Act
            var result = await _productServices.GetAllProductsFilteredById(999);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region GetAllProductsFilteredByString Tests

        [Fact]
        public async Task GetAllProductsFilteredByString_WithValidFilter_ShouldReturnProducts()
        {
            // Arrange
            var filter = "ração";
            var products = new List<ProductModel>
            {
                new ProductModel { ProductId = 1, ProductName = "Ração Premium" },
                new ProductModel { ProductId = 2, ProductName = "Ração Standard" }
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredByString(filter))
                .ReturnsAsync(products);

            // Act
            var result = await _productServices.GetAllProductsFilteredByString(filter);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllProductsFilteredByString_WithNoMatches_ShouldReturnEmptyList()
        {
            // Arrange
            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredByString("inexistente"))
                .ReturnsAsync(new List<ProductModel>());

            // Act
            var result = await _productServices.GetAllProductsFilteredByString("inexistente");

            // Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region RemoveProduct Tests

        [Fact]
        public async Task RemoveProduct_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var productId = 1;

            _mockProductRepository
                .Setup(r => r.RemoveProduct(productId))
                .ReturnsAsync(true);

            // Act
            var result = await _productServices.RemoveProduct(productId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveProduct_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            _mockProductRepository
                .Setup(r => r.RemoveProduct(999))
                .ReturnsAsync(false);

            // Act
            var result = await _productServices.RemoveProduct(999);

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region UpdateProduct Tests

        [Fact]
        public async Task UpdateProduct_WithValidRequest_ShouldUpdateProduct()
        {
            // Arrange
            var productRequest = new ProductRequest
            {
                ProductId = 1,
                ProductName = "Ração Premium Atualizada",
                Price = 120m,
                Description = "Alimento atualizado"
            };

            var existingProduct = new ProductModel
            {
                ProductId = 1,
                ProductName = "Ração Premium"
            };

            var updatedProduct = new ProductModel
            {
                ProductId = 1,
                ProductName = "Ração Premium Atualizada",
                Price = 120m
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(1))
                .ReturnsAsync(existingProduct);

            _mockProductRepository
                .Setup(r => r.UpdateProduct(It.IsAny<object>()))
                .ReturnsAsync(updatedProduct);

            // Act
            var result = await _productServices.UpdateProduct(productRequest);

            // Assert
            result.Should().NotBeNull();
            result.ProductName.Should().Be("Ração Premium Atualizada");
        }

        [Fact]
        public async Task UpdateProduct_WithZeroId_ShouldThrowArgumentException()
        {
            // Arrange
            var productRequest = new ProductRequest
            {
                ProductId = 0,
                ProductName = "Ração",
                Price = 100m
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _productServices.UpdateProduct(productRequest)
            );

            exception.Message.Should().Contain("ID do produto é obrigatório");
        }

        [Fact]
        public async Task UpdateProduct_WithNonexistentProduct_ShouldThrowArgumentException()
        {
            // Arrange
            var productRequest = new ProductRequest
            {
                ProductId = 999,
                ProductName = "Ração",
                Price = 100m
            };

            _mockProductRepository
                .Setup(r => r.GetAllProductsFilteredById(999))
                .ReturnsAsync((ProductModel)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _productServices.UpdateProduct(productRequest)
            );

            exception.Message.Should().Contain("produto não foi encontrado");
        }

        #endregion
    }
}
