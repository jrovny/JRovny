using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext _context;

        public PostsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Data.Models.Post> UpdateAsync(Data.Models.Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();

            return post;
        }
    }
}
