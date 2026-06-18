namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class StockAddRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;

        public StockAddRequest() { }

        public StockAddRequest(int productId, int quantity, string invoiceNumber)
        {
            ProductId = productId;
            Quantity = quantity;
            InvoiceNumber = invoiceNumber;
        }
    }
}