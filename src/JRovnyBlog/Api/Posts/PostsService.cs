using AutoMapper;
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
        private readonly IMapper mapper;

        public PostsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Data.Models.Post>> GetAllAsync()
        {
            return await _context.Posts
                .AsNoTracking()
                .Where(p => p.Published == true)
                .OrderByDescending(p => p.PostId)
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
                .AsNoTracking()
                .Where(p => p.Slug == slug)
                .FirstOrDefaultAsync();
        }

        //public async Task<Data.Models.PostView> GetBySlugAsync(string slug)
        //{
        //    var list =_context.Posts
        //        .Include(p => p.Comments)
        //        .Include(p => p.PostTags)
        //            .ThenInclude(pt => pt.Tag)
        //        .AsNoTracking()
        //        .Where(p => p.Slug == slug)
        //        .Select(p => new Data.Models.PostView
        //        {
        //            PostId = p.PostId,
        //            Title = p.Title,
        //            Content = p.Content,
        //            Image = p.Image,
        //            Slug = p.Slug,
        //            ViewCount = p.ViewCount,
        //            CommentCount = p.CommentCount,
        //            UpvoteCount = p.UpvoteCount,
        //            Published = p.Published,
        //            CreatedDate = p.CreatedDate,
        //            ModifiedDate = p.ModifiedDate,
        //            UserId = p.UserId,
        //            Comments = this.mapper.Map<ICollection<CommentView>>(p.Comments),
        //            Tags = p.PostTags
        //                .Select(pt => new TagView
        //                {
        //                    TagId = pt.Tag.TagId, 
        //                    Name = pt.Tag.Name 
        //                })
        //                .ToList()
        //        });

        //    return await list.SingleOrDefaultAsync();
        //}

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

        public async Task<PostUpvoteResponse> UpvoteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            post.UpvoteCount += 1;

            await _context.SaveChangesAsync();

            return new PostUpvoteResponse { UpvoteCount = post.UpvoteCount };
        }

        public async Task<PostUpvoteResponse> DownvoteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            post.UpvoteCount -= 1;

            if (post.UpvoteCount < 0)
                post.UpvoteCount = 0;

            await _context.SaveChangesAsync();

            return new PostUpvoteResponse { UpvoteCount = post.UpvoteCount };
        }
    }
}
