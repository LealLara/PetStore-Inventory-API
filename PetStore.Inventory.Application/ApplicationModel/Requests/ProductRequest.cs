using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Constants;

namespace PetStore.Inventory.Application.ApplicationModel.Requests
{
    public class ProductRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }

        public ProductRequest() { }

        public ProductRequest(string productName, string productDescription, float price)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
        }
        public ProductRequest(string productName, string productDescription, float price, int stockQuantity)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
        }
        public ProductRequest(int productId, string productName, string productDescription, float price)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
        }
        public ProductRequest(int productId, string productName, string productDescription, float price, int stockQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public List<ProductModel> SetPatternProducts() =>
        [
            new()
            {
                ProductName = PatternProducts.PatternProduct1,
                ProductDescription = PatternProducts.ProductDescription1,
                Price = PatternProducts.Price1,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct2,
                ProductDescription = PatternProducts.ProductDescription2,
                Price = PatternProducts.Price2,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct3,
                ProductDescription = PatternProducts.ProductDescription3,
                Price = PatternProducts.Price3,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct4,
                ProductDescription = PatternProducts.ProductDescription4,
                Price = PatternProducts.Price4,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct5,
                ProductDescription = PatternProducts.ProductDescription5,
                Price = PatternProducts.Price5,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct6,
                ProductDescription = PatternProducts.ProductDescription6,
                Price = PatternProducts.Price6,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct7,
                ProductDescription = PatternProducts.ProductDescription7,
                Price = PatternProducts.Price7,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct8,
                ProductDescription = PatternProducts.ProductDescription8,
                Price = PatternProducts.Price8,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct9,
                ProductDescription = PatternProducts.ProductDescription9,
                Price = PatternProducts.Price9,
                StockQuantity = PatternProducts.PatternStockQuantity
            },
            new()
            {
                ProductName = PatternProducts.PatternProduct10,
                ProductDescription = PatternProducts.ProductDescription10,
                Price = PatternProducts.Price10,
                StockQuantity = PatternProducts.PatternStockQuantity
            }
        ];

        public ProductModel ToModel()
        {
            return new(
                ProductId,
                ProductName,
                ProductDescription,
                Price,
                StockQuantity            
            );
        }

        public ProductEntity ToEntity()
        {
            return new(
                ProductId,
                ProductName,
                ProductDescription,
                Price,
                StockQuantity
            );
        }
    }
}