namespace WheelsAndGoods.Application.Models.User;

public class RefreshTokenRequest
{
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
}
