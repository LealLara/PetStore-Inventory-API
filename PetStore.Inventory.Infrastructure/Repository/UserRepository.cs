using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(UserEntity userData)
        {
            try
            {
                _context.UsersTable.Add(userData);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreatePatternUsers(List<UserEntity> userEntities)
        {
            try
            {
                _context.UsersTable.AddRange(userEntities);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserRegisterModel>> GetAllUsers()
        {
            try
            {
                IQueryable<UserEntity> users = _context.UsersTable.AsNoTracking();

                return ModelFactory.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByString(string filters)
        {
            try
            {
                IQueryable<UserEntity> users = _context.UsersTable.Where(r => r.FullName.ToLower().Contains(filters.ToLower()) || r.Nickname.ToLower().Contains(filters.ToLower()));

                return ModelFactory.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredById(int filters)
        {
            try
            {
                IQueryable<UserEntity> users = _context.UsersTable.AsNoTracking().Where(r => r.UserId == filters);

                return ModelFactory.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserRegisterModel>> GetUsersFilteredByRoleId(int filters)
        {
            try
            {
                IQueryable<UserEntity> users = _context.UsersTable.AsNoTracking().Where(r => r.RoleId == filters);

                return ModelFactory.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 

    }
}