using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Application.Interfaces.Services
{
    public interface ILogTypeServices
    {
        Task<bool> CreatePatternLogTypes();
        Task<bool> CreateLogTypes(LogTypeRequest logTypeRequest);
        Task<IEnumerable<LogTypeModel>> GetAllLogTypes();
        Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredByString(string filters);
        Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredById(int filters);
    }
}