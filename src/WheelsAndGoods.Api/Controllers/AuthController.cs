using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelsAndGoods.Api.Configuration.Swagger.Models;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Auth;
using WheelsAndGoods.Application.Models.User;
using WheelsAndGoods.Application.Models.User.Responses;

namespace WheelsAndGoods.Api.Controllers;

public class AuthController : ApiControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("register")]
	[AllowAnonymous]
	[ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request)
	{
		var result = await _authService.CreateUser(request);
		return StatusCode(StatusCodes.Status201Created, result);
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
	{
		await _authService.CreateUserSession(request);

		return NoContent();
	}
}
