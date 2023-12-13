using DataLayer.Models;

namespace DataLayer.Contracts
{
    public interface IProductRepository
    {
        public void Create(Product product);
        public Product GetProductById(int id);
        public void UpdatePrice(int id, decimal Price);
        public List<Product> GetProductsWithFilters
            (FilteredProductListRequest model);
    }
}
