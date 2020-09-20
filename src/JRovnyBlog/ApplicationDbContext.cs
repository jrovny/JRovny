using JRovnyBlog.Api.Posts.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JRovnyBlog
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
