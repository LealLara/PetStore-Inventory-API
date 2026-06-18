namespace PetStore.Inventory.Domain.BusinessModel
{
    public class StockMovementModel
    {
        public int StockMovementId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } = string.Empty;

        public StockMovementModel() { }

        public StockMovementModel(int stockMovementId, int productId, string productName, int quantity, string invoiceNumber, DateTime movementDate, string movementType)
        {
            StockMovementId = stockMovementId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            InvoiceNumber = invoiceNumber;
            MovementDate = movementDate;
            MovementType = movementType;
        }
    }
}