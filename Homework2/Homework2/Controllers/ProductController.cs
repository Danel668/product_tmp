using AutoMapper;
using Domain.Contracts;
using FluentValidation;
using Homework2.Models;
using Microsoft.AspNetCore.Mvc;
using ProductGrpc;

namespace Homework2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMapper _mapper;
        private IProductService _productService;
        private IValidator<CreateProductRequest> _createProductRequestValidator;
        private IValidator<GetProductByIdRequest> _getProductByIdRequestValidator;
        private IValidator<UpdateProductPriceRequest> _updateProductPriceRequestValidator;
        private IValidator<GetProductsListWithFiltersRequest> _getProductsListWithFiltersRequest;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            IValidator<CreateProductRequest> createProductRequestValidator,
            IValidator<GetProductByIdRequest> getProductByIdRequestValidator,
            IValidator<UpdateProductPriceRequest> updateProductPriceRequestValidator,
            IValidator<GetProductsListWithFiltersRequest> getProductsListWithFiltersRequest
            )
        {
            _createProductRequestValidator = createProductRequestValidator;
            _getProductByIdRequestValidator = getProductByIdRequestValidator;
            _updateProductPriceRequestValidator = updateProductPriceRequestValidator;
            _getProductsListWithFiltersRequest = getProductsListWithFiltersRequest;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductRequest request)
        {
            var validationResult = _createProductRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            var product = _mapper.Map<Domain.Models.Product>(request);
            _productService.CreateProduct(product);
            return Ok(new CreateProductResponse { Result = true });
        }

        [HttpPost]
        public IActionResult GetProductById(GetProductByIdRequest request)
        {
            var validationResult = _getProductByIdRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            var product = _mapper.Map<Product>(_productService.GetProductById(request.Id));
            var response = _mapper.Map<GetProductByIdResponse>(product);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetProductsListWithFilters(GetProductsListWithFiltersRequest request)
        {
            var validationResult = _getProductsListWithFiltersRequest.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            var productModel = _mapper.Map<Domain.Models.GetProductsListWithFiltersModel>(request);

            var products = _mapper.Map<List<Product>>(_productService.GetProductsWithFilters(productModel));

            var response = new GetProductsListWithFiltersResponse();
            response.Products.AddRange(products.Select(product => _mapper.Map<Product, ProductInfo>(_mapper.Map<Product>(product))));
            return Ok(response);
        }

        [HttpPost]
        public IActionResult UpdateProductPrice(UpdateProductPriceRequest request)
        {
            var validationResult = _updateProductPriceRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            _productService.UpdateProductPrice(request.Id, (decimal)request.Price);

            return Ok(new UpdateProductPriceResponse { Result = true });
        }
    }
}
