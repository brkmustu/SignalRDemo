using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRDemo.Orders;

namespace SignalRDemo.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.LastModifiedDate)
            .IsRequired(false)
            .HasColumnType("date");

        builder.Property(x => x.LastModifiedBy)
            .IsRequired(false);
    }
}
