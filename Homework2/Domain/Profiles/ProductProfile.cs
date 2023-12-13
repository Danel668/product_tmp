using AutoMapper;
using Domain.Models;

namespace Domain.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, DataLayer.Models.Product>().ReverseMap();

        }
    }
}
