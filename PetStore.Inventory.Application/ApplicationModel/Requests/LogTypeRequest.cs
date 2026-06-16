using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Constants;

namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class LogTypeRequest
    {
        public int LogTypeId { get; private set; }
        public string LogTypeName { get; private set; }
        public string LogTypeDescription { get; private set; }
        public LogTypeRequest() { }
        public LogTypeRequest(string logTypeName, string logTypeDescription )
        {
            LogTypeName = logTypeName;
            LogTypeDescription = logTypeDescription;
        }




        public List<LogTypeModel> SetPatternLogTypes() =>
        [
            new ()
        {
            LogTypeName = PatternLogTypes.PatternLogTypeInfo,
            LogTypeDescription = PatternLogTypes.PatternLogTypeInfoDescription, 
        },
        new ()
        {
            LogTypeName = PatternLogTypes.PatternLogTypeError,
            LogTypeDescription = PatternLogTypes.PatternLogTypeErrorDescription, 
        },
        new ()
        {
            LogTypeName = PatternLogTypes.PatternLogTypeWarning,
            LogTypeDescription = PatternLogTypes.PatternLogTypeWarningDescription, 
        }
        ];

        public LogTypeModel ToModel()
        {
            return new(
                LogTypeId,
                LogTypeName,
                LogTypeDescription
            );
        }
        public LogTypeEntity ToEntity()
        {
            return new(
                LogTypeId,
                LogTypeName,
                LogTypeDescription
            );
        }
        public List<LogTypeEntity> ToEntityList(List<LogTypeModel> logTypeModels)
        {
            return logTypeModels.Select(logTypeModel => new LogTypeEntity(
                logTypeModel.LogTypeId,
                logTypeModel.LogTypeName,
                logTypeModel.LogTypeDescription
            )).ToList();
        }
    }
}