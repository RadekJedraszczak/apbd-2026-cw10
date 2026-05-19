using apbd_2026_cw10.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_2026_cw10.Configurations;

public class PcConfigurations : IEntityTypeConfiguration<PCs>
{
    public void Configure(EntityTypeBuilder<PCs> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(50);

        builder.ToTable("PCs");
    }
    
}