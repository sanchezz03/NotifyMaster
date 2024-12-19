using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotifyMaster.Core.Entities;

namespace NotifyMaster.Core.EntityConfiguration;

public class ButtonConfiguration : IEntityTypeConfiguration<Button>
{
    public void Configure(EntityTypeBuilder<Button> builder)
    {
        builder
            .ToTable("Buttons")
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Name)
            .IsRequired();
    }
}
