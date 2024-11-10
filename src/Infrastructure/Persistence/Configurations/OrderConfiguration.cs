using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.Quantity).IsRequired().HasDefaultValue(1);
            builder.Property(o => o.Price).HasDefaultValue(0.0m);
            builder.Property(o => o.TotalAmount).HasDefaultValue(0.0m);
            builder.Property(o => o.Operation).IsRequired();
            builder.HasOne(o => o.Asset).WithMany(a => a.Orders).HasForeignKey(o => o.AssetId);
        }
    }

}
