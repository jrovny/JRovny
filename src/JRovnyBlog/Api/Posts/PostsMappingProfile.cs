using AutoMapper;

namespace JRovnyBlog.Api.Posts
{
    public class PostsMappingProfile : Profile
    {
        public PostsMappingProfile()
        {
            CreateMap<Models.Post, Data.Models.Post>();
            CreateMap<Data.Models.Post, Models.Post>();
        }
    }
}
