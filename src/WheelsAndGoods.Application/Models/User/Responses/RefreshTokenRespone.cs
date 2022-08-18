namespace WheelsAndGoods.Application.Models.User.Responses;

public class RefreshTokenRespone
{
    public string Token { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
}
