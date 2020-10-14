using System;

namespace JRovnyBlog.Api.Posts.Models
{
    public class CommentView
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
