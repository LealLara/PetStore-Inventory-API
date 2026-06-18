using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class StockMovementEntity
    {
        [Key]
        public int StockMovementId { get; private set; }
        public int ProductId { get; private set; }
        public virtual ProductEntity Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public string InvoiceNumber { get; private set; } = string.Empty;
        public DateTime MovementDate { get; private set; }
        public StockMovementType MovementType { get; private set; }

        public StockMovementEntity() { }

        public StockMovementEntity(int productId, int quantity, string invoiceNumber, StockMovementType movementType)
        {
            ProductId = productId;
            Quantity = quantity;
            InvoiceNumber = invoiceNumber;
            MovementDate = DateTime.UtcNow;
            MovementType = movementType;
        }

        public StockMovementEntity(int stockMovementId, int productId, int quantity, string invoiceNumber, StockMovementType movementType)
        {
            StockMovementId = stockMovementId;
            ProductId = productId;
            Quantity = quantity;
            InvoiceNumber = invoiceNumber;
            MovementDate = DateTime.UtcNow;
            MovementType = movementType;
        }
    }

    public enum StockMovementType
    {
        Inbound = 1,
        Outbound = 2
    }
}