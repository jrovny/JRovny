using System;

namespace JRovnyBlog.Api.Posts.Models
{
    public class PostSummary
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int CommentCount { get; set; }
        public int UpvoteCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
