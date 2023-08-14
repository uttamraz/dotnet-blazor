using DotNetBlazor.Server.Services.ProfileService;
using DotNetBlazor.Shared.Models.Profile;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("UpdateUser")]
        [ProducesResponseType(typeof(UserDetailResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            var data = await _profileService.UpdateUser(request);
            if (data != null)
            {
                return Ok(new UserDetailResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new UserDetailResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }

        [HttpPost("ChangePassword")]
        [ProducesResponseType(typeof(ChangePasswordResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var data = await _profileService.ChangePassword(request);
            if (data != null && data.IsSuccess)
            {
                return Ok(new ChangePasswordResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new ChangePasswordResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }

        [HttpGet("UserDetail")]
        [ProducesResponseType(typeof(UserDetailResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> UserDetail()
        {
            var data = await _profileService.UserDetail();
            if (data != null)
            {
                return Ok(new UserDetailResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new UserDetailResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }
    }
}
