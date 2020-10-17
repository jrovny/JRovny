﻿using JRovnyBlog.Api.Images.Data.Models;
using JRovnyBlog.Api.Posts.Data.Models;
using JRovnyBlog.Api.Tags.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JRovnyBlog
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConnectionService _connectionService;

        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<PostTag> PostTags { get; set; }

        public ApplicationDbContext(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(_connectionService.GetDefaultConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Post);
            builder.Entity<Comment>().HasQueryFilter(p => !p.Deleted);
            builder.Entity<Tag>().HasQueryFilter(t => !t.Deleted);
            builder.Entity<PostTag>().HasQueryFilter(t => !t.Deleted);
            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId)
                .IsRequired();
            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId)
                .IsRequired();
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                var createdDate = entry.Metadata.FindProperty("CreatedDate");
                var modifiedDate = entry.Metadata.FindProperty("ModifiedDate");
                var now = DateTime.UtcNow;

                if (createdDate != null)
                {
                    if (entry.State == EntityState.Added)
                        entry.Property(createdDate.Name).CurrentValue = now;
                    else if (entry.State == EntityState.Modified)
                        entry.Property(createdDate.Name).IsModified = false;
                }

                if (modifiedDate != null)
                    entry.Property(modifiedDate.Name).CurrentValue = now;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }
    }
}
