using Microsoft.AspNetCore.Mvc;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.User;

namespace WheelsAndGoods.Api.Controllers
{
    public class ProfileController : ApiControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _profileService.ChangePassword(request);
            return NoContent();
        }
    }
}
