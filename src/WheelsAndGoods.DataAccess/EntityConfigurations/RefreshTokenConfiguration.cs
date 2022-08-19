using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.EntityConfigurations;

public class RefreshTokenConfiguration : EntityConfigurationBase<RefreshToken, Guid>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(entity => entity.AccessTokenId).IsRequired();
        builder.Property(entity => entity.CreatedAtUtc).IsRequired();
        builder.Property(entity => entity.UserId);
        builder.Property(entity => entity.IsInvalidated);
        
        builder.HasOne(entity => entity.User)
            .WithMany()
            .HasForeignKey(entity => entity.UserId);
    }
}
