using DotNetBlazor.Server.Services.ProfileService;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Common;
using DotNetBlazor.Shared.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost("UpdateProfile")]
        [ProducesResponseType(typeof(UserDetailResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateProfile(UserUpdateRequest request)
        {
            var data = await _profileService.UpdateProfile(request);
            if (data != null)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }

        [HttpPost("ChangePassword")]
        [ProducesResponseType(typeof(ChangePasswordResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var data = await _profileService.ChangePassword(request);
            if (data != null && data.IsSuccess)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Password change failed!"));
        }

        [HttpGet("GetProfile")]
        [ProducesResponseType(typeof(UserDetailResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProfile()
        {
            var data = await _profileService.GetProfile();
            if (data != null)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }
    }
}
