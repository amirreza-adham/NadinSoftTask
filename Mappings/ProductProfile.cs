
using AutoMapper;
using NadinSoftTask.Domain.Entities;
using NadinSoftTask.DTOs.Products;

namespace NadinSoftTask.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>(); //  Create
            CreateMap<Product, ProductDto>();       //  Read / Return
        }
    }
}
