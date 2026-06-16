namespace PetStore.Inventory.Domain.BusinessModel
{
    public class LogTypeModel
    {
        public int LogTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public LogTypeModel() { }
        public LogTypeModel(int logTypeId, string name, string description)
        {
            LogTypeId = logTypeId;
            Name = name;
            Description = description;
        }
        public LogTypeModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}