using WheelsAndGoods.Application.Models.Internal;

namespace WheelsAndGoods.Application.Models.User;

public class RegisterRequest : IContainsPassword
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Phone { get; set; }
}
