namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class LogTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LogTypeRequest() { }
        public LogTypeRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}