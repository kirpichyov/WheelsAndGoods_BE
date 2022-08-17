using WheelsAndGoods.Application.Models.Auth;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;

namespace WheelsAndGoods.Application.Contracts.Services;

public interface IAuthService
{
	Task<string> CreateUserSession(SignInRequest request);
	Task<UserInfoResponse> CreateUser(RegisterRequest request);
}
