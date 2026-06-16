using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Application.Interfaces.Repositories
{
    public interface ILogTypeRepository
    {
        Task<bool> CreatePatternLogTypes(List<LogTypeEntity> logTypeEntities);
        Task<bool> CreateLogType(LogTypeEntity logTypeRequest);
        Task<IEnumerable<LogTypeModel>> GetAllLogTypes();
        Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredByString(string filters);
        Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredById(int filters);
    }
}