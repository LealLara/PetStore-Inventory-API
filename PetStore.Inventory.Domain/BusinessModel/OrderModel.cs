namespace PetStore.Inventory.Domain.BusinessModel
{
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
        public OrderModel(string customerDocument, string sellerName, List<OrderItemModel> items)
        {
            CustomerDocument = customerDocument;
            SellerName = sellerName;
            Items = items;
        }
    }
}