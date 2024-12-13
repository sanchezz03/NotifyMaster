using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Core.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(u => u.Id);

        builder
            .Property(u => u.UserId)
            .IsRequired();

        builder
            .Property(u => u.UserName)
            .IsRequired(false);

        builder
            .Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder
            .Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder
            .Property(u => u.GroupStatus)
            .IsRequired();

        builder
            .HasMany(u => u.UserReminders)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);
    }
}
