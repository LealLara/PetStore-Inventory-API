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

        public OrderItemModel(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity; 
        }
        public OrderItemModel(int productId, string productName, int quantity, decimal unitPrice, decimal subtotal)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Subtotal = subtotal;
        }
    }
}