using FluentValidation;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Enums;
using PetStore.Inventory.Domain.Utils.Validations;

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
            StockValidation valid = new();
            valid.ValidateAndThrow(request.ToModel());
             
            ProductModel productModel = await _productRepository.GetAllProductsFilteredById(request.ProductId);

            if (productModel == null)
                throw new ArgumentException($"Produto com ID {request.ProductId} não encontrado.");
              
            productModel.AddStock(request.Quantity);
             
            var updated = await _productRepository.UpdateProduct(productModel.ToEntity());

            if (updated == null)
                throw new Exception("Falha ao atualizar o estoque do produto.");
             
            StockMovementEntity movementEntity = new(
                request.ProductId,
                request.Quantity,
                request.InvoiceNumber,
                EStockMovementType.Inbound
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