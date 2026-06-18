using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly AppDbContext _context;

        public StockMovementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StockMovementModel> AddStockMovement(StockMovementEntity entity)
        {
            try
            {
                _context.StockMovementTable.Add(entity);
                await _context.SaveChangesAsync();
                return ModelFactory.CreateStockMovement(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao adicionar movimento de estoque: {ex.Message}");
            }
        }

        public async Task<IEnumerable<StockMovementModel>> GetMovementsByProductId(int productId)
        {
            try
            {
                var movements = _context.StockMovementTable
                    .Include(s => s.Product)
                    .Where(s => s.ProductId == productId)
                    .OrderByDescending(s => s.MovementDate);

                return ModelFactory.CreateStockMovements(movements);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao obter movimentos de estoque: {ex.Message}");
            }
        }

        public async Task<ProductModel> GetProductWithStock(int productId)
        {
            try
            {
                var product = await _context.ProductTable
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                return ModelFactory.CreateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao obter produto com estoque: {ex.Message}");
            }
        }
    }
}