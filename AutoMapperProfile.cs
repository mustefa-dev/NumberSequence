using Auth.Models;
using AutoMapper;
using OrderNumberSequence.DATA.DTOs;
using OrderNumberSequence.DATA.DTOs.OrderProduct;


namespace Auth
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, Order>();
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<OrderProductForm,OrderProduct>();
            CreateMap<OrderProductUpdate,OrderProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));
            CreateMap<OrderForm,Order>();
            CreateMap<OrderUpdate,Order>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, ProductDto>();
            CreateMap<ProductForm,Product>();
            CreateMap<ProductUpdate,Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}