using AutoMapper;

namespace JRovnyBlog.Api.Posts
{
    public class PostsMappingProfile : Profile
    {
        public PostsMappingProfile()
        {
            CreateMap<Models.PostSaveRequest, Data.Models.Post>();
            CreateMap<Data.Models.Post, Models.PostSaveRequest>();
            CreateMap<Data.Models.Post, Models.PostView>();
            CreateMap<Data.Models.Post, Models.PostSummary>();
            CreateMap<Data.Models.PostUpvoteResponse, Models.PostUpvoteResponse>();
        }
    }
}
