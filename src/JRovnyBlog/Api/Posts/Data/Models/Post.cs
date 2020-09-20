using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JRovnyBlog.Api.Posts.Data.Models
{
    [Table("post")]
    public class Post
    {
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("body")]
        public string Body { get; set; }
        [Column("slug")]
        public string Slug { get; set; }
        [Column("likes_count")]
        public int LikesCount { get; set; }
        [Column("view_count")]
        public int ViewCount { get; set; }
        [Column("comment_count")]
        public int CommentCount { get; set; }
        [Column("published")]
        public bool Published { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }
}
