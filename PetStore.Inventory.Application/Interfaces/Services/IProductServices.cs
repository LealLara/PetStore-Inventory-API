using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Domain.Interfaces.Services
{
    public interface IProductServices
    {
        Task<bool> CreatePatternProducts();
        Task<ProductModel> CreateProduct(ProductRequest request);
        Task<ProductModel> UpdateProduct(ProductRequest request);
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<IEnumerable<ProductModel>> GetAllProductsFilteredByString(string filter);
        Task<ProductModel> GetAllProductsFilteredById(int filter);
        Task<bool> RemoveProduct(int id);

    }
}