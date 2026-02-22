using lizi_mail_api.Response;

namespace lizi_mail_api.Services.Email
{
    public interface IEmailService
    {
        Task<Result<bool>> SendEmailAsync(string to, string subject, string body);
    }
}
