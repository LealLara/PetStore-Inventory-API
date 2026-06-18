using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class OrderEntity
    {
        [Key]
        public int OrderId { get; private set; }
        public string CustomerDocument { get; private set; } = string.Empty;
        public string SellerName { get; private set; } = string.Empty;
        public decimal TotalAmount { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public virtual List<OrderItemEntity> Items { get; private set; } = new();

        public OrderEntity() { }

        public OrderEntity(string customerDocument, string sellerName, decimal totalAmount, List<OrderItemEntity> items)
        {
            CustomerDocument = customerDocument;
            SellerName = sellerName;
            TotalAmount = totalAmount;
            CreatedAt = DateTime.UtcNow;
            Items = items;
        }
    }

    public class OrderItemEntity
    {
        [Key]
        public int OrderItemId { get; private set; }
        public int OrderId { get; private set; }
        public virtual OrderEntity Order { get; private set; } = null!;
        public int ProductId { get; private set; }
        public virtual ProductEntity Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal { get; private set; }

        public OrderItemEntity() { }

        public OrderItemEntity(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Subtotal = unitPrice * quantity;
        }
    }
}