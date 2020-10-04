using JRovnyBlog.Api.Posts.Data.Models;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    public interface IPostsService
    {
        Task<Post> UpdateAsync(Post post);
    }
}