using FluentValidation;
using PetStore.Inventory.Application.ApplicationModel.Requests;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Utils.Validations;

namespace PetStore.Inventory.Application.Services
{
    public class LogTypeServices : ILogTypeServices
    {
        private readonly ILogTypeRepository _logTypeRepository;
        public LogTypeServices(ILogTypeRepository logTypeRepository)
        {
            _logTypeRepository = logTypeRepository;
        }
        public async Task<bool> CreateLogTypes(LogTypeRequest logTypeRequest)
        {
            LogTypeValidation valid = new();

            valid.ValidateAndThrow(logTypeRequest.ToModel());

            return await _logTypeRepository.CreateLogType(logTypeRequest.ToEntity());
        }

        public async Task<bool> CreatePatternLogTypes()
        {
            IEnumerable<LogTypeModel> logTypeList = await GetAllLogTypes();
            if (!logTypeList.Any())
            {
                LogTypeRequest request = new();
                return await _logTypeRepository.CreatePatternLogTypes(request.SetPatternLogTypes().Select(x => x.ToEntity()).ToList());
            }
            return false;
        }

        public async Task<IEnumerable<LogTypeModel>> GetAllLogTypes()
        {
            return await _logTypeRepository.GetAllLogTypes();
        }

        public async Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredById(int filters)
        {
            return await _logTypeRepository.GetLogTypesFilteredById(filters);
        }

        public async Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredByString(string filters)
        {
            return await _logTypeRepository.GetLogTypesFilteredByString(filters);
        }
    }
}