using FluentValidation;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel; 
using PetStore.Inventory.Domain.Utils.Validations;

namespace PetStore.Inventory.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> CreatePatternProducts()
        {
            IEnumerable<ProductModel> productList = await GetAllProducts();
            if (!productList.Any())
            {
                ProductRequest request = new();
                return await _productRepository.CreatePatternProducts(request.SetPatternProducts().Select(productModel => productModel.ToEntity()).ToList());
            }
            return false;
        }         

        public async Task<ProductModel> CreateProduct(ProductRequest request)
        {
            ProductValidation valid = new();
             
            valid.ValidateAndThrow(request.ToModel());

            ProductModel product = await _productRepository.CreateProduct(request.ToEntity());
            return product;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<ProductModel> GetAllProductsFilteredById(int filter)
        {
            return await _productRepository.GetAllProductsFilteredById(filter);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsFilteredByString(string filter)
        {
            return await _productRepository.GetAllProductsFilteredByString(filter);
        }

        public async Task<bool> RemoveProduct(int id)
        {
            return await _productRepository.RemoveProduct(id);
        }

        public async Task<ProductModel> UpdateProduct(ProductRequest request)
        {
            ProductValidation valid = new();

            valid.ValidateAndThrow(request.ToModel());

            if(request.ProductId == 0)
            {
                throw new ArgumentException("O ID do produto é obrigatório.");
            }

            ProductModel product = await GetAllProductsFilteredById(request.ProductId);

            if(product is null)
            {
                throw new ArgumentException("O produto não foi encontrado.");
            }

            return await _productRepository.UpdateProduct(request.ToEntity());
        }
    }
}