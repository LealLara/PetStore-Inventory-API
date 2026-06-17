using PetStore.Inventory.Application.BusinessDTOs.Results;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class AccessRegisterRepository : IAccessRegisterRepository
    {
        private readonly AppDbContext _context;
        public AccessRegisterRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<DataRegisterResult> CreateAccessRegister(UserEntity request)
        {
            throw new NotImplementedException();
        }

        public Task<DataRegisterResult> Login(LoginEntity request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logoff(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUser(int userId)
        {
            throw new NotImplementedException();
        }

        /*  public async Task<IEnumerable<RoleModel>> GetRolesFilteredByString(string filters)
          {
              try
              {
                  IQueryable<RoleEntity> roles = _context.RolesTable.Where(r => r.RoleName.ToLower().Contains(filters.ToLower()));

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
                  IQueryable<RoleEntity> roles = _context.RolesTable.AsNoTracking().Where(r => r.RoleId == filters);

                  return ModelFactory.CreateRoles(roles);
              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }
          }
          public async Task<IEnumerable<RoleModel>> GetRolesFilteredByRoleId(int roleId)
          {
              try
              {
                  IQueryable<RoleEntity> roles = _context.RolesTable.AsNoTracking().Where(r => r.RoleId == filters);

                  return ModelFactory.CreateRoles(roles);
              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }
          }*/
    }
}