using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Core.EntityConfiguration;

public class MessageReminderConfiguration : IEntityTypeConfiguration<MessageReminder>
{
    public void Configure(EntityTypeBuilder<MessageReminder> builder)
    {
        builder
            .ToTable("MessageReminders")
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Message)
            .IsRequired();

        builder
            .Property(r => r.VideoUrl)
            .IsRequired(false);

        builder
            .Property(r => r.Delay)
            .IsRequired();

        builder
            .Property(r => r.NotificationPhase)
            .IsRequired();

        builder
           .HasOne(r => r.Button)
           .WithMany()
           .HasForeignKey(r => r.ButtonId);
    }
}
