namespace WheelsAndGoods.Application.Models.User.Responses
{
	public class JwtResponse
	{
		public string AccessToken { get; set; }
		public DateTime? ExpiresAtUtc { get; set; }
	}
}
