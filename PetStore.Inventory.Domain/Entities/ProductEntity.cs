using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public int ProductId{ get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int Quantity { get; private set; }
        public float Price { get; private set; }


        public ProductEntity(){}
        public ProductEntity(int productId, string productName, string productDescription, float price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Quantity = quantity;
        }
        public ProductEntity( string productName, string productDescription, float price, int quantity)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Quantity = quantity;
        }
    }
}