using WheelsAndGoods.Application.Models.User.Responses;

namespace WheelsAndGoods.Application.Contracts.Services;

public interface IUsersService
{
    Task<UserInfoResponse> GetUserInfo();
}
