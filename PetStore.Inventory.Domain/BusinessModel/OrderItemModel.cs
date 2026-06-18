namespace PetStore.Inventory.Domain.BusinessModel
{
    public class OrderItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        public OrderItemModel() { }

        public OrderItemModel(int productId, string productName, int quantity, decimal unitPrice, decimal subtotal)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Subtotal = subtotal;
        }
    }

    public class OrderModel
    {
        public int OrderId { get; set; }
        public string CustomerDocument { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemModel> Items { get; set; } = new();

        public OrderModel() { }

        public OrderModel(int orderId, string customerDocument, string sellerName, decimal totalAmount, DateTime createdAt, List<OrderItemModel> items)
        {
            OrderId = orderId;
            CustomerDocument = customerDocument;
            SellerName = sellerName;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
            Items = items;
        }
    }
}