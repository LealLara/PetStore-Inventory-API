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
        public ProductEntity(int productId, string productName, string productDescription, float price )
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = 0;
        }
        public ProductEntity( string productName, string productDescription, float price )
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = 0;
        }
        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");
            StockQuantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");
            if (StockQuantity < quantity)
                throw new InvalidOperationException("Estoque insuficiente.");
            StockQuantity -= quantity;
        }
    }
}