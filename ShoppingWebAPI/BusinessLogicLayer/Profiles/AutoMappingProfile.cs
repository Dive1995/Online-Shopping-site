using System.Collections.Generic;
using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Models.Dtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Profile
{
    public class AutoMappingProfile : AutoMapper.Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Customer, CustomerCreationDto>().ReverseMap();
            CreateMap<Customer, CustomerLoginDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<OrderItems, OrderItemsCreationDto>().ReverseMap();
            CreateMap<Shipping, ShippingCreationDto>().ReverseMap();
            CreateMap<Invoice, InvoiceCreationDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreationDto>().ReverseMap();
            CreateMap<Shipping, ShippingDto>().ReverseMap();
            CreateMap<OrderItems, OrderItemsDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Product, OrderItemsDto>().ReverseMap();
            //CreateMap<Product, ProductInvoiceDto>().ReverseMap();       // rather than converting Product -> ProductInvoiceDto then
            //CreateMap<OrderItemsDto, ProductInvoiceDto>().ReverseMap(); // ProductInvoceDto -> OrderItemsDto make it as one step (remove ProductInvoiceDto)
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreationDto>().ReverseMap();
        }
    }
}
