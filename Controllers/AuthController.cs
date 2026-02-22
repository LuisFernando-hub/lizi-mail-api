using lizi_mail_api.Request.Auth;
using lizi_mail_api.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lizi_mail_api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> create(CreateUserRequest request)
        {
            var result = await _userService.create(request);

            if (!result.status)
            {
                return BadRequest(result);
            }

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var result = await _userService.login(request);

            if (!result.status)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

    }
}
