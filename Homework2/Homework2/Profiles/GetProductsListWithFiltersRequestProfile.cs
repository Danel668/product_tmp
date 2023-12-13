using AutoMapper;
using ProductGrpc;
using Domain.Models;

namespace Homework2.Profiles
{
    public class GetProductsListWithFiltersRequestProfile : Profile
    {
        public GetProductsListWithFiltersRequestProfile()
        {
            CreateMap<GetProductsListWithFiltersRequest, GetProductsListWithFiltersModel>()
             .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.HasCreationDate ? DateTime.Parse(src.CreationDate) : (DateTime?)null))
             .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.HasType ? (ProductType)src.Type : (ProductType?)null))
             .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.HasWarehouseId ? src.WarehouseId : (int?)null));
        }
    }
}
