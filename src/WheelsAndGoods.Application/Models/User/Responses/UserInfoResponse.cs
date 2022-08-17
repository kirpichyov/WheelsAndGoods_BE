namespace WheelsAndGoods.Application.Models.User.Responses;

public class UserInfoResponse
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Phone { get; set; }
}
