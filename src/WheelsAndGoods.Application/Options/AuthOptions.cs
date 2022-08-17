namespace WheelsAndGoods.Application.Options
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int AccessTokenTTLMinutes { get; set; }
		public string Secret { get; set; }
		public string[] CorsAllowedList { get; set; }
	}
}
