using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> UsersTable => Set<UserEntity>();
        public DbSet<RoleEntity> RolesTable => Set<RoleEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {

        }
    }
}