using PetStore.Inventory.Application.ApplicationModel.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }


        public ProductDTO(){ }
        public ProductRequest ToBusinessRequest()
        {
            return new (ProductName, ProductDescription, Price, StockQuantity);
        }
    }
}