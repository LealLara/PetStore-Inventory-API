using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class LogTypeRepository : ILogTypeRepository
    {
        private readonly AppDbContext _context;

        public LogTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateLogType(LogTypeEntity logTypeRequest)
        {
            _context.LogTypesTable.Add(logTypeRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreatePatternLogTypes(List<LogTypeEntity> logTypeEntities)
        {
            try
            {
                _context.LogTypesTable.AddRange(logTypeEntities);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<LogTypeModel>> GetAllLogTypes()
        {
            try
            {
                IQueryable<LogTypeEntity> logTypes = _context.LogTypesTable.AsNoTracking();

                return ModelFactory.CreateLogTypes(logTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredById(int filters)
        {
            try
            {
                IQueryable<LogTypeEntity> logTypes = _context.LogTypesTable.Where(l => l.LogTypeId == filters);

                return ModelFactory.CreateLogTypes(logTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<LogTypeModel>> GetLogTypesFilteredByString(string filters)
        {
            try
            {
                IQueryable<LogTypeEntity> logTypes = _context.LogTypesTable.Where(l => l.LogTypeName.ToLower().Contains(filters.ToLower()));

                return ModelFactory.CreateLogTypes(logTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}