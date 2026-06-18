using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public int ProductId{ get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; } 
        public float Price { get; private set; }
        public int StockQuantity { get; private set; }  

        public ProductEntity(){}
        public ProductEntity(int productId, string productName, string productDescription, float price, int stockQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
        }
        public ProductEntity( string productName, string productDescription, float price, int stockQuantity )
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}