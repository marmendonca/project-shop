using AutoMapper;
using Shop.Domain.Dtos.Request;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
