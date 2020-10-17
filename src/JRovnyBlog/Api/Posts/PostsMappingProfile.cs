using System.Linq;
using AutoMapper;

namespace JRovnyBlog.Api.Posts
{
    public class PostsMappingProfile : Profile
    {
        public PostsMappingProfile()
        {
            CreateMap<Models.PostSaveRequest, Data.Models.Post>();
            CreateMap<Data.Models.Post, Models.PostSaveRequest>();
            CreateMap<Data.Models.Post, Models.PostView>()
                .ForMember(post => post.Tags, opt =>
                    opt.MapFrom(p => p.PostTags.Select(
                        t => new Tags.Models.Tag
                        {
                            TagId = t.Tag.TagId,
                            Name = t.Tag.Name
                        })));
            CreateMap<Data.Models.Post, Models.PostSummary>();
            CreateMap<Data.Models.PostUpvoteResponse, Models.PostUpvoteResponse>();
            CreateMap<Data.Models.Comment, Models.CommentView>();
            CreateMap<Tags.Data.Models.Tag, Tags.Models.Tag>();
        }
    }
}
