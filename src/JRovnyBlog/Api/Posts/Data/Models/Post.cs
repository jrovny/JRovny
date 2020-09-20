﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JRovnyBlog.Api.Posts.Data.Models
{
    [Table("post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("slug")]
        public string Slug { get; set; }
        [Column("likes")]
        public int Likes { get; set; }
        [Column("views")]
        public int Views { get; set; }
        [Column("comments")]
        public int Comments { get; set; }
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
