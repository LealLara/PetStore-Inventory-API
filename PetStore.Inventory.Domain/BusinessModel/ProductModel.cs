using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class ProductModel
    {
        public int ProductId{ get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; } 

        public ProductModel(){}
        public ProductModel(int productId, string productName, string productDescription, float price )
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price; 
        }
        public ProductModel(string productName, string productDescription, float price )
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price; 
        }

        public ProductEntity ToEntity()
        {
            return new(ProductId, ProductName, ProductDescription, Price);
        }
    }
}