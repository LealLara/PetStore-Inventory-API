using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _appDbContext;
        public LoginRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<LoginModel> Login(LoginEntity data)
        {
            try
            {
                _appDbContext.LoginTable.Add(data);
                await _appDbContext.SaveChangesAsync();

                return ModelFactory.CreateLogin(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreatePatternLogin(IEnumerable<LoginEntity> data)
        {
            try
            {
                _appDbContext.LoginTable.AddRange(data);
                return await _appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
         
        public async Task<bool> RemoveAccount(int userId)
        {
            LoginEntity? entity = await _appDbContext.LoginTable.FirstOrDefaultAsync(x => x.UserId == userId);

            if (entity is null)
            {
                return false;
            }
            _appDbContext.LoginTable.Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }


        public async Task<LoginModel?> GetByNickname(string nickname)
        {
            LoginEntity? entity = await _appDbContext.LoginTable
                .FirstOrDefaultAsync(x => x.Nickname == nickname);

            return entity == null
                ? null
                : ModelFactory.CreateLogin(entity);
        }
    
        public async Task<LoginModel> GetLoginById(int userId)
        {
            LoginEntity? entity = await _appDbContext.LoginTable.FirstOrDefaultAsync(x => x.UserId == userId);
            return ModelFactory.CreateLogin(entity);
        }
        public async Task<IEnumerable<LoginModel>> GetAllLogins()
        {
            IQueryable<LoginEntity> entities = _appDbContext.LoginTable;
            return ModelFactory.CreateLogins(entities);
        }
    }
}