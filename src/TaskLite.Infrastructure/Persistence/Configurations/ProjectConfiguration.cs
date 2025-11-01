using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskLite.Domain.Entities;

namespace TaskLite.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> b)
    {
        b.ToTable("Project");
        b.HasKey(x => x.Id);

        b.Property(x => x.Name).IsRequired().HasMaxLength(200);
        b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        //b.HasOne<User>()
        // .WithMany()
        // .HasForeignKey(x => x.OwnerId)
        // .OnDelete(DeleteBehavior.Restrict);

        b.HasMany(p => p.Tasks)
         .WithOne(t => t.Project)
         .HasForeignKey(t => t.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.OwnerId);
    }
}
