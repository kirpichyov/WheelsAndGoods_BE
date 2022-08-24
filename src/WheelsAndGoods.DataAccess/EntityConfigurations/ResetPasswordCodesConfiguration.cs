using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.EntityConfigurations;

public class ResetPasswordCodesConfiguration : EntityConfigurationBase<ResetPasswordCode, Guid>
{
    public override void Configure(EntityTypeBuilder<ResetPasswordCode> builder)
    {
        base.Configure(builder);

        builder.Property(entity => entity.Email).IsRequired();
        builder.Property(entity => entity.CreatedAtUtc).IsRequired();
        builder.Property(entity => entity.Code).IsRequired();
    }
}
