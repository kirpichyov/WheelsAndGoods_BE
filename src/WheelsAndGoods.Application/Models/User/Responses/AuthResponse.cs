namespace WheelsAndGoods.Application.Models.User.Responses
{
	public class AuthResponse
	{
		public JwtResponse Jwt { get; set; }
        public RefreshTokenRespone RefreshToken { get; set; }
    }
}
