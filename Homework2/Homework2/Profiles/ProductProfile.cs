using AutoMapper;
using Homework2.Models;
using ProductGrpc;

namespace Homework2.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequest, Domain.Models.Product>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (decimal)src.Price))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ProductType)src.Type))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Parse(src.CreationDate)));

            CreateMap<Product, GetProductByIdResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (double)src.Price))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ProductType)src.Type))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("yyyy-MM-dd")));

            CreateMap<Product, ProductInfo>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (double)src.Price))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ProductType)src.Type))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("yyyy-MM-dd")));

            CreateMap<Product, Domain.Models.Product>().ReverseMap();

        }
    }
}
