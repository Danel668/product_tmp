using AutoMapper;
using Domain.Contracts;
using FluentValidation;
using Grpc.Core;
using Homework2.Exceptions;
using Homework2.Models;
using ProductGrpc;

namespace Homework2.GrpcServices
{
    public class ProductGrpcService : ProductGrpc.ProductGrpcService.ProductGrpcServiceBase
    {
        private IMapper _mapper;
        private IProductService _productService;
        private IValidator<CreateProductRequest> _createProductRequestValidator;
        private IValidator<GetProductByIdRequest> _getProductByIdRequestValidator;
        private IValidator<UpdateProductPriceRequest> _updateProductPriceRequestValidator;
        private IValidator<GetProductsListWithFiltersRequest> _getProductsListWithFiltersRequest;

        public ProductGrpcService(
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
        public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
           var validationResult = _createProductRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                throw new ValidateException(new Status(StatusCode.InvalidArgument, "Validation failed"), validationResult.ToString());

            var product = _mapper.Map<Domain.Models.Product>(request);

            _productService.CreateProduct(product);

            await Task.CompletedTask;
            return new CreateProductResponse { Result = true};
        }

        public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            var validationResult = _getProductByIdRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                throw new ValidateException(new Status(StatusCode.InvalidArgument, "Validation failed"), validationResult.ToString());

            var product = _productService.GetProductById(request.Id);

            var response = _mapper.Map<GetProductByIdResponse>(_mapper.Map<Product>(product));

            await Task.CompletedTask;
            return response;
        }

        public override async Task<GetProductsListWithFiltersResponse> GetProductsListWithFilters(GetProductsListWithFiltersRequest request, ServerCallContext context)
        {
            var validationResult = _getProductsListWithFiltersRequest.Validate(request);
            if (!validationResult.IsValid)
                throw new ValidateException(new Status(StatusCode.InvalidArgument, "Validation failed"), validationResult.ToString());

            var productModel = _mapper.Map<Domain.Models.GetProductsListWithFiltersModel>(request);

            var products = _mapper.Map<List<Product>>(_productService.GetProductsWithFilters(productModel));

            var response = new GetProductsListWithFiltersResponse();
            response.Products.AddRange(products.Select(product => _mapper.Map<Product, ProductInfo>(product)));

;           await Task.CompletedTask;
            return response;
        }

        public override async Task<UpdateProductPriceResponse> UpdateProductPrice(UpdateProductPriceRequest request, ServerCallContext context)
        {
            var validationResult = _updateProductPriceRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                throw new ValidateException(new Status(StatusCode.InvalidArgument, "Validation failed"), validationResult.ToString());

            _productService.UpdateProductPrice(request.Id, (decimal)request.Price);

            await Task.CompletedTask;
            return new UpdateProductPriceResponse { Result = true};
        }
    }
}
