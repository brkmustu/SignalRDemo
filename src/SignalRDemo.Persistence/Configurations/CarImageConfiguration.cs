using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRDemo.Cars;

namespace SignalRDemo.Configurations;

public class CarImageConfiguration : IEntityTypeConfiguration<CarImage>
{
    public void Configure(EntityTypeBuilder<CarImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.CarImageType)
            .HasColumnType("integer");

        builder.Property(x => x.Title)
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(300);
    }
}
