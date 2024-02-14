using AutoMapper;
using Infrastructure.Extensions;
using Shared.DTOs.Product;

namespace Product.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Product, ProductDto>();
            //CreateMap<CreateProductDto, Domain.Entities.Product>();
            CreateMap<CreateUpdateProductCategoryDto, Domain.Entities.Product>()
            .IgnoreAllNonExisting();
        }
    }
}