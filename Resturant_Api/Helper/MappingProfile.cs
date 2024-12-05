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
            CreateMap<Table, TableDto>().ForMember(x=>x.IsAvailable,opt=>opt.MapFrom(x=>x.IsAvailable.ToString()))
                .ForMember(x=>x.smoking,opt=>opt.MapFrom(x=>x.smoking.ToString()))
                .ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Reservison, ReserveDto>()
                .ForMember(x=>x.TableId,opt=>opt.MapFrom(x=>x.TableId)).ReverseMap();



        }
    }
}
