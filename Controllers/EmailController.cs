using lizi_mail_api.Middleware;
using lizi_mail_api.Request.Email;
using lizi_mail_api.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi;

namespace lizi_mail_api.Controllers
{
    [ApiController]
    [Route("api/v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [ApiKeyAuth]
        [Authorize]
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromHeader(Name = "X-API-KEY")] string ApiKey, EmailRequest request)
        {
            var result = await _emailService.SendEmailAsync(request.to, request.subject, request.body);

            return Ok();
        }
    }
}
