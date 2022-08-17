namespace WheelsAndGoods.Application.Models.User.Responses
{
	public class UserResponse
	{
		public JwtResponse Jwt { get; set; }
		public UserInfoResponse User { get; set; }
	}
}

