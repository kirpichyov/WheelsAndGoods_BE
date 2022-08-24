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
    private readonly IResetPasswordCodesService _resetPasswordCodesService;

	public AuthController(
        IAuthService authService, 
        IResetPasswordCodesService resetPasswordCodesService)
    {
        _authService = authService;
        _resetPasswordCodesService = resetPasswordCodesService;
    }

	[HttpPost("register")]
	[AllowAnonymous]
	[ProducesResponseType(typeof(UserCreatedResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request)
	{
		var result = await _authService.CreateUser(request);
		return StatusCode(StatusCodes.Status201Created, result);
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
	{
		var result = await _authService.CreateUserSession(request);
		return Ok(result);
	}

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshToken(request);
        return Ok(result);
    }
    
    [HttpPost("reset-password/request-email")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _resetPasswordCodesService.SendResetPasswordCodeEmail(request);
        return NoContent();
    }
}
