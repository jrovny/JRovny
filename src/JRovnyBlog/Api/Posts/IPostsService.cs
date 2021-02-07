using JRovnyBlog.Api.Posts.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    public interface IPostsService
    {
        Task<IEnumerable<Data.Models.PostSummary>> GetAllAsync();
        Task<Data.Models.Post> GetByIdAsync(int postId);
        Task<Data.Models.Post> GetBySlugAsync(string slug);
        Task<Data.Models.Post> CreateAsync(Data.Models.Post post);
        Task<Post> UpdateAsync(Post post);
        Task<PostVoteResponse> UpvoteAsync(int id);
        Task<PostVoteResponse> DownvoteAsync(int id);
        Task<Comment> CreateInitialCommentAsync(Comment comment);
        Task<IEnumerable<Data.Models.PostSummary>> GetAllByTagAsync(int tagId);
    }
}