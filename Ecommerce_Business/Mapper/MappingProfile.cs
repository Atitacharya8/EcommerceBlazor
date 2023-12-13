using AutoMapper;
using Ecommerce_DataAccess;
using Ecommerce_DataAccess.ViewModels;
using Ecommerce_Models;

namespace Ecommerce_Business.Mapper
{
    public class MappingProfile : Profile
    {
        //CategoryDTO to Category and again vice-versa through constructor
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap(); //Method 1
            // Method 2: CreateMap<CategoryDTO, Category>();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
