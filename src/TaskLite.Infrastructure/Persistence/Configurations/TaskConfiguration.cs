using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskLite.Domain.Entities;
using Task = TaskLite.Domain.Entities.Task;

namespace TaskLite.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> b)
    {
        b.ToTable("Task");
        b.HasKey(x => x.Id);

        b.Property(x => x.Title).IsRequired().HasMaxLength(200);
        b.Property(x => x.Status).HasConversion<int>().IsRequired();
        b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        b.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

        //b.HasOne<Project>()
        // .WithMany()
        // .HasForeignKey(x => x.ProjectId)
        // .OnDelete(DeleteBehavior.Cascade);

        //b.HasOne<User>()
        // .WithMany()
        // .HasForeignKey(x => x.AssigneeId)
        // .OnDelete(DeleteBehavior.SetNull);

        b.HasMany(t => t.Comments)
         .WithOne(c => c.Task)
         .HasForeignKey(c => c.TaskId)
         .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => new { x.ProjectId, x.Status });
        b.HasIndex(x => x.DueDate);
    }
}
