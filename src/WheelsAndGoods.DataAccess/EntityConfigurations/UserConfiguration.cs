using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Enums;

namespace WheelsAndGoods.DataAccess.EntityConfigurations;

public class UserConfiguration : EntityConfigurationBase<User, Guid>
{
	private const string UserRoleString = nameof(Role.User);
	private const string AdminRoleString = nameof(Role.Admin);
	
	public override void Configure(EntityTypeBuilder<User> builder)
	{
		base.Configure(builder);

		builder.Property(entity => entity.Email).IsRequired();
		builder.Property(entity => entity.PasswordHash).IsRequired();
		builder.Property(entity => entity.Firstname);
		builder.Property(entity => entity.Lastname);
		builder.Property(entity => entity.Phone);
		
		builder.Property(entity => entity.Role)
			.HasConversion(@enum => ConvertToString(@enum), @string => ConvertToEnum(@string))
			.IsRequired();
	}

	private static string ConvertToString(Role role)
	{
		return role switch
		{
			Role.User => UserRoleString,
			Role.Admin => AdminRoleString,
			_ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
		};
	}

	private static Role ConvertToEnum(string role)
	{
		return role switch
		{
			UserRoleString => Role.User,
			AdminRoleString => Role.Admin,
			_ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
		};
	}
}
