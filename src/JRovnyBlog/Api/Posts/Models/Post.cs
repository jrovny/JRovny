using System;

namespace JRovnyBlog.Api.Posts.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public int ViewCount { get; set; }
        public int LikesCount { get; set; }
        public int CommentCount { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserId { get; set; }
    }
}
