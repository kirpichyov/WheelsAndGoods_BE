namespace WheelsAndGoods.Core.Models.Entities;

public class User : EntityBase<Guid>
{
	public User(string email, string passwordHash)
		: base(Guid.NewGuid())
	{
		Email = email;
		PasswordHash = passwordHash;
	}

	private User()
	{
	}

	public string Email { get; }
	public string PasswordHash { get; }
}
