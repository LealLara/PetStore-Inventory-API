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
        public DbSet<StockMovementEntity> StockMovementTable => Set<StockMovementEntity>();
        public DbSet<OrderEntity> OrderTable => Set<OrderEntity>();
        public DbSet<OrderItemEntity> OrderItemTable => Set<OrderItemEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            modelBuilder.Entity<OrderItemEntity>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItemEntity>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovementEntity>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}