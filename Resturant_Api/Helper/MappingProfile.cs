using AutoMapper;
using Resturant_Api.Dtos;
using Resturant_Api_Core.Entites;

namespace Resturant_Api.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meals, MealDto>()
                .ForMember(X => X.PictureUrl, opt => opt.MapFrom<MealsPictureResolver>())
                .ForMember(x=>x.CategoryId ,opt=>opt.MapFrom(x=>x.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ReverseMap();
            CreateMap<Table, TableDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Reservison, ReserveDto>().ReverseMap();



        }
    }
}
