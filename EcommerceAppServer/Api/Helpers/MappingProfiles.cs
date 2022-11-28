using Api.Dtos;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.Identity;
using Domain.Entity.OrderAggregate;

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
            
            CreateMap<Domain.Entity.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, Domain.Entity.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}
