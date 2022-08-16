using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;

namespace WheelsAndGoods.Application.Contracts.Services;

public interface IAuthService
{
	Task<UserInfoResponse> CreateUser(RegisterRequest request);
}
