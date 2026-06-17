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

        public async Task<UserRegisterModel> CreateUser(UserEntity userData)
        {
            try
            {
                _context.UserTable.Add(userData);
                await _context.SaveChangesAsync();
                return ModelFactory.CreateUser(userData);
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
                _context.UserTable.AddRange(userEntities);
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
                IQueryable<UserEntity> users = _context.UserTable.AsNoTracking();

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
                IQueryable<UserEntity> users = _context.UserTable.Where(r => r.FullName.ToLower().Contains(filters.ToLower()) || r.Nickname.ToLower().Contains(filters.ToLower()));

                return ModelFactory.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserRegisterModel> GetUsersFilteredById(int userId)
        {
            try
            {
            UserEntity? user = await _context.UserTable.AsNoTracking().FirstOrDefaultAsync(r => r.UserId == userId);

                return ModelFactory.CreateUser(user);
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
                IQueryable<UserEntity> users = _context.UserTable.AsNoTracking().Where(r => r.RoleId == filters);

                return ModelFactory.CreateUsers(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserRegisterModel> GetUserFilteredByEmail(string filters)
        {
            try
            {
                UserEntity? users = await _context.UserTable.FirstOrDefaultAsync(r => r.Email.Contains(filters));
                 
                return ModelFactory.CreateUser(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}