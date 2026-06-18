using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> CreatePatternProducts(IEnumerable<ProductEntity> entities);
        Task<ProductModel> CreateProduct(ProductEntity entity);
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<IEnumerable<ProductModel>> GetAllProductsFilteredByString(string filter);
        Task<ProductModel> GetAllProductsFilteredById(int filter);
        Task<bool> RemoveProduct(int id);
        Task<ProductModel> UpdateProduct(ProductEntity entity);
    }
}