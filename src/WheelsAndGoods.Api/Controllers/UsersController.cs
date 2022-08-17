using Microsoft.AspNetCore.Mvc;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User.Responses;

namespace WheelsAndGoods.Api.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status200OK)]
    public async Task<UserInfoResponse> GetCurrentUserInfo()
    {
        return await _usersService.GetUserInfo();
    }
}
