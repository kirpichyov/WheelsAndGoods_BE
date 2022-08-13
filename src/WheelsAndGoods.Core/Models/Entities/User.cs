using WheelsAndGoods.Core.Models.Enums;

namespace WheelsAndGoods.Core.Models.Entities;

public class User : EntityBase<Guid>
{
	private User(string email, string passwordHash, Role role)
		: base(Guid.NewGuid())
	{
		Email = email;
		PasswordHash = passwordHash;
		Role = role;
	}

	private User()
	{
	}

	public string Email { get; }
	public string PasswordHash { get; }
	public Role Role { get; }
	public string? Firstname { get; private set; }
	public string? Lastname { get; private set; }
	public string? Phone { get; private set; }

	public static User CreateUser(
		string email,
		string passwordHash,
		string firstName,
		string lastName,
		string phone)
	{
		return new User(email, passwordHash, Role.User)
		{
			Firstname = firstName,
			Lastname = lastName,
			Phone = phone
		};
	}

	public static User CreateAdmin(string email, string passwordHash)
	{
		return new User(email, passwordHash, Role.Admin);
	}
}
