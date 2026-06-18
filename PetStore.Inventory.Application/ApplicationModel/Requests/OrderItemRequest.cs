using PetStore.Inventory.Domain.BusinessModel;

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

        public OrderItemModel ToModel()
        {
            return new OrderItemModel
            {
                ProductId = ProductId,
                Quantity = Quantity
            };
        }
    }
}