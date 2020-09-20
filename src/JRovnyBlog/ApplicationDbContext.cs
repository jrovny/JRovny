using JRovnyBlog.Api.Posts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JRovnyBlog
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConnectionService _connectionService;

        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(_connectionService.GetDefaultConnectionString());
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
