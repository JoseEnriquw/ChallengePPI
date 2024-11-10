using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(50);
            builder.HasData(
                new AssetType { Id = 1, Description = "Acción" },
                new AssetType { Id = 2, Description = "Bono" },
                new AssetType { Id = 3, Description = "FCI" }
            );
        }
    }

}
