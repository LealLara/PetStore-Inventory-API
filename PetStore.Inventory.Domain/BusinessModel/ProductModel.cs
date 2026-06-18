using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class ProductModel
    {
        public int ProductId{ get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public ProductModel(){}
        public ProductModel(int productId, string productName, string productDescription, float price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Quantity = quantity;
        }
        public ProductModel(string productName, string productDescription, float price, int quantity)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Quantity = quantity;
        }

        public ProductEntity ToEntity()
        {
            return new(ProductId, ProductName, ProductDescription, Price, Quantity);
        }
    }
}