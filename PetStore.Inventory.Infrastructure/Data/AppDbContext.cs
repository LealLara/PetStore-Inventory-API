using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Domain.Entities;

namespace PetStore.Inventory.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> UserTable => Set<UserEntity>();
        public DbSet<RoleEntity> RoleTable => Set<RoleEntity>();
        public DbSet<LogTypeEntity> LogTypeTable => Set<LogTypeEntity>();
        public DbSet<LoginEntity> LoginTable => Set<LoginEntity>();
        public DbSet<ProductEntity> ProductTable => Set<ProductEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {

        }
    }
}