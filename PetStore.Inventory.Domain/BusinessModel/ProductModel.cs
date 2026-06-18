using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }

        public ProductModel() { }
        public ProductModel(int productId, string productName, string productDescription, float price, int stockQuantity = 0)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
        }
        public ProductModel(string productName, string productDescription, float price, int stockQuantity )
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public ProductEntity ToEntity()
        {
            return new(ProductId, ProductName, ProductDescription, Price, StockQuantity);
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