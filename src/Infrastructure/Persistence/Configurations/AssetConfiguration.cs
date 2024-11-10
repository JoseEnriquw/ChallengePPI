using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Ticker).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.UnitPrice).IsRequired();

            builder.HasOne(a => a.AssetType).WithMany(at => at.Assets).HasForeignKey(a => a.AssetTypeId);
        }
    }

}
