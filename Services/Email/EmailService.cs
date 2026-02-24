using lizi_mail_api.HttpContext;
using lizi_mail_api.Request.Email;
using lizi_mail_api.Response;
using lizi_mail_api.Services.ApiKey;
using lizi_mail_api.Services.MimeMassage;

namespace lizi_mail_api.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IUserContext _userContext;
        private readonly MimeMessageService _mimeMessageService;
        private readonly IApiKeyService _apiKeyService;

        public EmailService(IConfiguration config, IUserContext userContext, MimeMessageService mimeMessageService, IApiKeyService apiKeyService)
        {
            _config = config;
            _userContext = userContext;
            _mimeMessageService = mimeMessageService;
            _apiKeyService = apiKeyService;
        }

        public async Task<Result<object>> SendEmailAsync(EmailRequest request)
        {

            var userId = _userContext.UserId;

            var apiKey = await _apiKeyService.getActiveByUserId(userId.ToString()!);

            if (apiKey == null)
            {
                return Result<object>.error(false, "API Key not found");
            }

            try
            {
                var response = await _mimeMessageService._invoke(_config, request.to, request.subject, request.body);

                if (!response.status)
                {
                   return Result<object>.error(false, $"Failed to send email: {response.message}");
                }

                return Result<object>.success(new {
                        message = response.message,
                        status = response.status
                });
            }
            catch (Exception ex)
            {
               return Result<object>.error(false, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
