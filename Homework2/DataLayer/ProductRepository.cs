using DataLayer.Contracts;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<int, Product> _products;

        public ProductRepository()
        {
            _products = new Dictionary<int, Product>();

            for (int i = 1; i <= 40; i++)
            {
                _products[i] = new Product
                {
                    Id = i,
                    Name = $"Product{i}",
                    Price = i * 10.0m,
                    Weight = i * 0.5,
                    WarehouseId = i % 10,
                    CreationDate = DateTime.Now.AddDays(-i),
                    Type = (ProductType)((i - 1) % Enum.GetValues(typeof(ProductType)).Length)
                };
            }
        }

        public void Create(Product product)
        {
            _products.Add(product.Id, product);
        }

        public Product GetProductById(int id)
        {
            return _products[id];
        }

        public void UpdatePrice(int id, decimal Price)
        {
            var product = _products[id];
            product.Price = Price;
        }

        public List<Product> GetProductsWithFilters
            (FilteredProductListRequest model)
        {
            return _products.Values
                .Where(p =>
                    (!model.CreationDate.HasValue ||
                    p.CreationDate.Date == model.CreationDate.Value.Date) &&
                    (!model.Type.HasValue ||
                    p.Type == model.Type.Value) &&
                    (!model.WarehouseId.HasValue || 
                    p.WarehouseId == model.WarehouseId.Value))
                .Skip((model.Pagination - 1) *
                    model.PageSize)
                .Take(model.PageSize).ToList();
        }
    }
}
