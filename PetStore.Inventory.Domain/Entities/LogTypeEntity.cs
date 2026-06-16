using System.ComponentModel.DataAnnotations;

namespace PetStore.Inventory.Domain.Entities
{
    public class LogTypeEntity
    {
        [Key]
        public int LogTypeId { get; private set; }
        public string LogTypeName { get; private set; }
        public string LogTypeDescription { get; private set; }

        public LogTypeEntity(){}
        public LogTypeEntity(string logTypeName, string logTypeDescription)
        { 
            LogTypeName = logTypeName;
            LogTypeDescription = logTypeDescription;
        }
        public LogTypeEntity(int logTypeId, string logTypeName, string logTypeDescription)
        {
            LogTypeId = logTypeId;
            LogTypeName = logTypeName;
            LogTypeDescription = logTypeDescription;
        }
    }
}