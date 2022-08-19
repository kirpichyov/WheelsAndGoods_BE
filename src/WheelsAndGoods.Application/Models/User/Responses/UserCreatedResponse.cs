namespace WheelsAndGoods.Application.Models.User.Responses;

public class UserCreatedResponse : AuthResponse
{
    public UserInfoResponse User { get; set; }
}
