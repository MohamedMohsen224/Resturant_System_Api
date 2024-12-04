using AutoMapper;
using Resturant_Api.Dtos;
using Resturant_Api_Core.Entites;

namespace Resturant_Api.Helper
{
    public class MealsPictureResolver : IValueResolver<Meals, MealDto, string>
    {
        private readonly IConfiguration configuration;

        public MealsPictureResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Meals source, MealDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiUrl"]}{source.PictureUrl}";
            return string.Empty ;

            
        }
    }
}
