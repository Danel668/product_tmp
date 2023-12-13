using Domain.Contracts;
using Domain.Models;
using DataLayer.Contracts;
using AutoMapper;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private IMapper _mapper;
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public void CreateProduct(Product product)
        {
            var dataLayesproduct = _mapper.Map<DataLayer.Models.Product>(product);
            _productRepository.Create(dataLayesproduct);
        }

        public List<Product> GetProductsWithFilters
            (GetProductsListWithFiltersModel model)
        {
            var dataLayersmodel = _mapper.Map<DataLayer.Models.FilteredProductListRequest>(model);
            var tmp = _productRepository.GetProductsWithFilters(dataLayersmodel);
            return _mapper.Map<List<Product>>(tmp);
        }

        public Product GetProductById(int id)
        {
            return _mapper.Map<Product>(_productRepository.GetProductById(id));
        }

        public void UpdateProductPrice(int id, decimal Price)
        {
            _productRepository.UpdatePrice(id, Price);
        }
    }
}
