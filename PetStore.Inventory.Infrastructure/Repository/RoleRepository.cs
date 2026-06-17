using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePatternRoles(List<RoleEntity> roleEntities)
        {
            try
            {
                _context.RoleTable.AddRange(roleEntities);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateRole(RoleEntity roleRequest)
        {
            try
            {
                _context.RoleTable.Add(roleRequest);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetAllRoles()
        {
            try
            {
                IQueryable<RoleEntity> roles = _context.RoleTable.AsNoTracking();

                return ModelFactory.CreateRoles(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<RoleModel>> GetRolesFilteredByString(string filters)
        {
            try
            {
                IQueryable<RoleEntity> roles = _context.RoleTable.Where(r => r.RoleName.ToLower().Contains(filters.ToLower())); 

                return ModelFactory.CreateRoles(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<RoleModel>> GetRolesFilteredById(int filters)
        {
            try
            {
                IQueryable<RoleEntity> roles = _context.RoleTable.AsNoTracking().Where(r => r.RoleId == filters);

                return ModelFactory.CreateRoles(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}