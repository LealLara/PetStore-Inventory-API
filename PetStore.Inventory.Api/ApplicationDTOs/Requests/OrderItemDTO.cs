using PetStore.Inventory.Application.ApplicationModel.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public OrderItemDTO() { }

        public OrderItemRequest ToBusinessRequest()
        {
            return new(ProductId, Quantity);
        }
    }

    public class OrderCreateDTO
    {
        public string CustomerDocument { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public List<OrderItemDTO> Items { get; set; } = new();

        public OrderCreateDTO() { }

        public OrderCreateRequest ToBusinessRequest()
        {
            var items = Items?.Select(i => i.ToBusinessRequest()).ToList() ?? new();
            return new(CustomerDocument, SellerName, items);
        }
    }
}