using Microsoft.EntityFrameworkCore;
using PetStore.Inventory.Application.Interfaces.Repositories;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Factories;
using PetStore.Inventory.Infrastructure.Data;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePatternProducts(IEnumerable<ProductEntity> entities)
        {
            try
            {
                _context.ProductTable.AddRange(entities);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao cadastrar os produtos padrão.");
            }
        }

        public async Task<ProductModel> CreateProduct(ProductEntity entity)
        {
            try
            {
                _context.ProductTable.Add(entity);
                await _context.SaveChangesAsync();
                return ModelFactory.CreateProduct(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao cadastrar o produto.");
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            try
            {
                IQueryable<ProductEntity> products = _context.ProductTable;

                return ModelFactory.CreateProducts(products);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao obter os produtos");
            }
        }

        public async Task<ProductModel> GetAllProductsFilteredById(int filter)
        {
            try
            {
                ProductEntity? productEntity = await _context.ProductTable.FirstOrDefaultAsync(p => p.ProductId == filter);
                return ModelFactory.CreateProduct(productEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao obter produto");
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsFilteredByString(string filter)
        {
            try
            {
                IQueryable<ProductEntity> products = _context.ProductTable.Where(p => p.ProductName.Contains(filter));

                return ModelFactory.CreateProducts(products);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao obter os produtos");
            }
        }

        public async Task<bool> RemoveProduct(int id)
        {
            try
            {
                ProductModel product = await GetAllProductsFilteredById(id);
                if (product == null)
                {
                    throw new Exception("Produto não encontrado");
                }

                _context.ProductTable.Remove(product.ToEntity());
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao remover produto");
            }
        }

        public async Task<ProductModel> UpdateProduct(ProductEntity entity)
        {
            try
            {
                ProductEntity existing = await _context.ProductTable.FindAsync(entity.ProductId);
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(entity);
                }
                else
                {
                    _context.ProductTable.Update(entity);
                }

                await _context.SaveChangesAsync();
                return ModelFactory.CreateProduct(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao atualizar o produto.");
            }
        }
    }
}