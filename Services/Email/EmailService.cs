using lizi_mail_api.Entities;
using lizi_mail_api.Entities.Enuns;
using lizi_mail_api.HttpContext;
using lizi_mail_api.Infra.Repository.Email;
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
        private readonly IEmailRepository _emailRepository;

        public EmailService(IConfiguration config, IUserContext userContext, MimeMessageService mimeMessageService, IApiKeyService apiKeyService, IEmailRepository emailRepository)
        {
            _config = config;
            _userContext = userContext;
            _mimeMessageService = mimeMessageService;
            _apiKeyService = apiKeyService;
            _emailRepository = emailRepository;
        }

        public async Task<Result<object>> SendEmailAsync(EmailRequest request)
        {

            var userId = _userContext.UserId;

            if (userId == null)
                return Result<object>.error(false, "User not authenticated");



            var apiKey = await _apiKeyService.getActiveByUserId(userId.ToString()!);

            if (apiKey == null)
                return Result<object>.error(false, "API Key not found");

            if (apiKey.data.user == null)
                return Result<object>.error(false, "Invalid API Key user reference");


            var email = new EmailEntity(apiKey.data.user.id, apiKey.data.id, request.to, request.subject, request.body);

            try
            {
                var response = await _mimeMessageService._invoke(_config, request.to, request.subject, request.body);

                email.status = response.status ? StatusEmail.sent : StatusEmail.failed;
                await _emailRepository.create(email);

                if (!response.status)
                {
                   return Result<object>.error(false, $"Failed to send email: {response.message}");
                }

                await _emailRepository.commitAsync();
                return Result<object>.success(new {
                        message = response.message,
                        status = response.status
                });
            }
            catch (Exception ex)
            {
                email.status = StatusEmail.failed;
                await _emailRepository.create(email);
                await _emailRepository.commitAsync();
                return Result<object>.error(false, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
