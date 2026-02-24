using lizi_mail_api.Middleware;
using lizi_mail_api.Request.Email;
using lizi_mail_api.Services.ApiKey;
using lizi_mail_api.Services.Email;
using lizi_mail_api.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lizi_mail_api.Controllers
{
    [ApiController]
    [Route("api/v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IApiKeyService _apiKeyService;

        public EmailController(IEmailService emailService, IApiKeyService apiKeyService)
        {
            _emailService = emailService;
            _apiKeyService = apiKeyService;
        }

        [Authorize]
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromHeader(Name = "X-API-KEY")] string ApiKey, EmailRequest request)
        {
            var result = await _emailService.SendEmailAsync(request);

            if (!result.status)
                return Problem(result.message);

            return Ok(result.data);
        }
    }
}
