using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.EntityConfigurations;

public class UserConfiguration : EntityConfigurationBase<User, Guid>
{
	public override void Configure(EntityTypeBuilder<User> builder)
	{
		base.Configure(builder);

		builder.Property(entity => entity.Email).IsRequired();
		builder.Property(entity => entity.PasswordHash).IsRequired();
	}
}
