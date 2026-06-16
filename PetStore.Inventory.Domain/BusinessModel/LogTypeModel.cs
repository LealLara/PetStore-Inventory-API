using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Domain.BusinessModel
{
    public class LogTypeModel
    {
        public int LogTypeId { get; set; }
        public string LogTypeName { get; set; }
        public string LogTypeDescription { get; set; }


        public LogTypeModel() { }
        public LogTypeModel(int logTypeId, string logTypeName, string logTypeDescription)
        {
            LogTypeId = logTypeId;
            LogTypeName = logTypeName;
            LogTypeDescription = logTypeDescription;
        }
        public LogTypeModel(string logTypeName, string logTypeDescription)
        {
            LogTypeName = logTypeName;
            LogTypeDescription = logTypeDescription ;
        }
        public LogTypeEntity ToEntity()
        {
            return new(
                LogTypeId,
                LogTypeName,
                LogTypeDescription
            );
        }
    }
}