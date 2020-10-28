using JRovnyBlog.Api.Posts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext _context;
        private enum Vote
        {
            Upvote = 1,
            Downvote = 2
        }

        public PostsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Models.PostSummary>> GetAllAsync()
        {
            return await _context.Posts
                .AsNoTracking()
                .Where(p => p.Published == true)
                .Select(p => new PostSummary
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Slug = p.Slug,
                    Image = p.Image,
                    CommentCount = p.CommentCount,
                    UpvoteCount = p.UpvoteCount,
                    CreatedDate = p.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<Data.Models.Post> GetByIdAsync(int postId)
        {
            return await _context.Posts
                .AsNoTracking()
                .Where(p => p.PostId == postId)
                .FirstOrDefaultAsync();
        }

        public async Task<Data.Models.Post> GetBySlugAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .AsNoTracking()
                .Where(p => p.Slug == slug)
                .FirstOrDefaultAsync();
        }

        public async Task<Data.Models.Post> CreateAsync(Data.Models.Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Data.Models.Post> UpdateAsync(Data.Models.Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<PostVoteResponse> UpvoteAsync(int id)
        {
            return await VoteAsync(id, Vote.Upvote);
        }

        public async Task<PostVoteResponse> DownvoteAsync(int id)
        {
            return await VoteAsync(id, Vote.Downvote);
        }

        public async Task<Comment> CreateInitialCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private async Task<PostVoteResponse> VoteAsync(int id, Vote vote)
        {
            var post = await _context.Posts.FindAsync(id);

            if (vote == Vote.Upvote)
                post.UpvoteCount += 1;
            else
                post.DownvoteCount += 1;

            await _context.SaveChangesAsync();

            return new PostVoteResponse
            {
                UpvoteCount = post.UpvoteCount,
                DownvoteCount = post.DownvoteCount
            };
        }
    }
}
