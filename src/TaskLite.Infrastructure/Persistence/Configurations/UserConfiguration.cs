using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskLite.Domain.Entities;

namespace TaskLite.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("User");
        b.HasKey(x => x.Id);

        b.Property(x => x.Email).IsRequired().HasMaxLength(256);
        b.Property(x => x.FullName).IsRequired().HasMaxLength(128);

        b.HasMany(u => u.ProjectsOwned)
         .WithOne(p => p.Owner)
         .HasForeignKey(p => p.OwnerId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasMany(u => u.ParticipatingProjects).WithMany(p => p.Participants);

        b.HasMany(u => u.CommentsAuthored)
         .WithOne(c => c.Author)
         .HasForeignKey(c => c.AuthorId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => x.Email).IsUnique();
    }
}
