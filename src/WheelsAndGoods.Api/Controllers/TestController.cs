using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WheelsAndGoods.Api.Controllers;

// NOTE: Will be removed after configs testing completion.
[AllowAnonymous]
public class TestController : ApiControllerBase
{
	private readonly IConfiguration _configuration;

	public TestController(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	[HttpGet]
	public string GetConfig()
	{
		return _configuration["TestConfig"];
	}
}
