using AutoMapper;
using TheListener.Models;
using TheListener.Models.Dto;



namespace TheListener
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            
            
                CreateMap<Category, CategoryDto>();
                CreateMap<CategoryDto, Category>();

                CreateMap<Category, CategoryCreateDto>().ReverseMap();
                CreateMap<Category, CategoryUpdateDto>().ReverseMap();

                CreateMap<Product, ProductDto>().ReverseMap();
                CreateMap<Product, ProductUpdateDto>().ReverseMap();
                CreateMap<Product, ProductCreateDto>().ReverseMap();

                            
        }

    }
}
