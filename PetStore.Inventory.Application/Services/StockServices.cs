using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;  

namespace PetStore.Inventory.Application.Services
{
    public class StockServices : IStockServices
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IProductRepository _productRepository;

        public StockServices(
            IStockMovementRepository stockMovementRepository,
            IProductRepository productRepository)
        {
            _stockMovementRepository = stockMovementRepository;
            _productRepository = productRepository;
        }

        public async Task<StockMovementModel> AddStockAsync(StockAddRequest request)
        {
            // Validar quantidade
            if (request.Quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            // Validar nota fiscal
            if (string.IsNullOrWhiteSpace(request.InvoiceNumber))
                throw new ArgumentException("O número da nota fiscal é obrigatório.");

            // Buscar produto
            var product = await _productRepository.GetAllProductsFilteredById(request.ProductId);
            if (product == null)
                throw new ArgumentException($"Produto com ID {request.ProductId} não encontrado.");

            // Criar entidade do produto para atualizar estoque
            var productEntity = product.ToEntity();
            productEntity.AddStock(request.Quantity);

            // Atualizar estoque
            var updated = await _stockMovementRepository.UpdateProductStock(productEntity);
            if (!updated)
                throw new Exception("Falha ao atualizar o estoque do produto.");

            // Registrar movimento
            var movementEntity = new StockMovementEntity(
                request.ProductId,
                request.Quantity,
                request.InvoiceNumber,
                StockMovementType.Inbound
            );

            var movement = await _stockMovementRepository.AddStockMovement(movementEntity);

            return movement;
        }

        public async Task<IEnumerable<StockMovementModel>> GetStockMovementsByProductId(int productId)
        {
            return await _stockMovementRepository.GetMovementsByProductId(productId);
        }

        public async Task<ProductModel> GetProductStock(int productId)
        {
            return await _stockMovementRepository.GetProductWithStock(productId);
        }
    }
}