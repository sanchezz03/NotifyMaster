using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Core.EntityConfiguration;

public class UserReminderConfiguration : IEntityTypeConfiguration<UserReminder>
{
    public void Configure(EntityTypeBuilder<UserReminder> builder)
    {
        builder
            .ToTable("UserReminders")
            .HasKey(r => r.Id);

        builder
            .Property(r => r.UserId)
            .IsRequired();

        builder
            .Property(r => r.JobId)
            .IsRequired();

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.UserReminders)
            .HasForeignKey(r => r.UserId);
    }
}
