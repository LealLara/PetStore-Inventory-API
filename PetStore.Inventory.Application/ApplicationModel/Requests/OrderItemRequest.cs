namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public OrderItemRequest() { }

        public OrderItemRequest(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }

    public class OrderCreateRequest
    {
        public string CustomerDocument { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public List<OrderItemRequest> Items { get; set; } = new();

        public OrderCreateRequest() { }

        public OrderCreateRequest(string customerDocument, string sellerName, List<OrderItemRequest> items)
        {
            CustomerDocument = customerDocument;
            SellerName = sellerName;
            Items = items;
        }
    }
}