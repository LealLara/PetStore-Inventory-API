using PetStore.Inventory.Application.ApplicationModel.Requests;

namespace PetStore.Inventory.Api.ApplicationDTOs.Requests
{
    public class LogTypeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public LogTypeRequest ToBusinessRequest()
        {
            return new(
                logTypeName: Name,
                logTypeDescription: Description
            );
        }
    }
}