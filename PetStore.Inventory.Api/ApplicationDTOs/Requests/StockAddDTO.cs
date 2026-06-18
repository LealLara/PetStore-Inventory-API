using PetStore.Inventory.Application.ApplicationModel.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class StockAddDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;

        public StockAddDTO() { }

        public StockAddRequest ToBusinessRequest()
        {
            return new(ProductId, Quantity, InvoiceNumber);
        }
    }
}