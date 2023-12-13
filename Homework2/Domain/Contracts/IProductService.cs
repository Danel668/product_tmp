using Domain.Models;

namespace Domain.Contracts
{
    public interface IProductService
    {
        public void CreateProduct(Product product);
        public List<Product> GetProductsWithFilters(GetProductsListWithFiltersModel model);
        public Product GetProductById(int id);
        public void UpdateProductPrice(int id, decimal Price);
    }
}
