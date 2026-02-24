using lizi_mail_api.Request.Email;
using lizi_mail_api.Response;

namespace lizi_mail_api.Services.Email
{
    public interface IEmailService
    {
        Task<Result<object>> SendEmailAsync(EmailRequest request);
    }
}
