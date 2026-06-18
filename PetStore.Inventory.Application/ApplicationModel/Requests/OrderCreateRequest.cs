using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
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

        public List<OrderItemModel> ToModelList()
        {
            return Items.Select(item => item.ToModel()).ToList();
        }

        public OrderModel ToModel()
        {
            return new OrderModel(
                CustomerDocument,
                SellerName,
                ToModelList()
            );
        }
    }
}