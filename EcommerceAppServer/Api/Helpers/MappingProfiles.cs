using Api.Dtos;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.Identity;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dto => dto.Brand, o => o.MapFrom(entity => entity.Brand.Name))
                .ForMember(dto => dto.ProductType, o => o.MapFrom(entity => entity.ProductType.Name))
                .ForMember(dto => dto.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
