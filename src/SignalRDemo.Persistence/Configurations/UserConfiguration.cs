using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.Authorization.Users;

namespace SignalRDemo.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmailAddress)
            .IsRequired()
            .HasMaxLength(70);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.PasswordSalt)
            .IsRequired()
            .HasMaxLength(200);

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

        builder.Property(x => x.RoleIds)
            .IsRequired(false);
    }
}
