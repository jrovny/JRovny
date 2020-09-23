using System;

namespace JRovnyBlog.Api.Posts.Models
{
    public class PostSummary
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Comments { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
