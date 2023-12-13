using AutoMapper;
using DataLayer.Enums;
using DataLayer.Models;
using Domain.Models;

namespace Domain.Profiles
{
    public class GetProductsListWithFiltersModelProfile : Profile
    {
        public GetProductsListWithFiltersModelProfile()
        {
            CreateMap<GetProductsListWithFiltersModel, FilteredProductListRequest>()
             .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate ?? (DateTime?)null))
             .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (ProductType?)src.Type ?? (ProductType?)null))
             .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId ?? (int?)null));
        }
    }
}
