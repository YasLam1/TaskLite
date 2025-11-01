using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskLite.Domain.Entities;
using Task = TaskLite.Domain.Entities.Task;

namespace TaskLite.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> b)
    {
        b.ToTable("Comment");
        b.HasKey(x => x.Id);

        b.Property(x => x.Body).IsRequired();
        b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        //b.HasOne<Task>()
        // .WithMany()
        // .HasForeignKey(x => x.TaskId)
        // .OnDelete(DeleteBehavior.Cascade);

        //b.HasOne<User>()
        // .WithMany()
        // .HasForeignKey(x => x.AuthorId)
        // .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => new { x.TaskId, x.CreatedAt });
    }
}