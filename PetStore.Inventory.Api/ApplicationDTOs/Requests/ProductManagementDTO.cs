using PetStore.Inventory.Application.ApplicationModel.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class ProductManagementDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }

        public ProductManagementDTO() { }
        public ProductRequest ToBusinessRequest()
        {
            return new(ProductId, ProductName, ProductDescription, Price, StockQuantity);
        }
    }
}