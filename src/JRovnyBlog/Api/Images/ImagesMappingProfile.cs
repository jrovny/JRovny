using AutoMapper;

namespace JRovnyBlog.Api.Images
{
    public class ImagesMappingProfile : Profile
    {
        public ImagesMappingProfile()
        {
            CreateMap<Data.Models.Image, Models.Image>();
        }
    }
}
