using DotNetBlazor.Server.Helpers.Attributes;
using DotNetBlazor.Server.Services.AccountService;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _registrationService;
        public AccountController(IAccountService registrationService)
        {
            _registrationService = registrationService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            var data = await _registrationService.RegisterUser(request);
            if (data != null)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Login(LoginRequest request)
        {
            var data = await _registrationService.Login(request);
            if (data != null && !string.IsNullOrEmpty(data.Token))
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Logged in successfully!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Invalid username or password!"));
        }

        //[AllowAnonymous]
        //[HttpPost("Logout")]
        //[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]

        //public async Task<IActionResult> Logout(LoginRequest request)
        //{
        //    var data = await _registrationService.Logout(request);
        //    if (data != null)
        //    {
        //        return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Logout successfully!"));
        //    }
        //    return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        //}
    }
}
