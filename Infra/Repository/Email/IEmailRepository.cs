using lizi_mail_api.Entities;

namespace lizi_mail_api.Infra.Repository.Email
{
    public interface IEmailRepository
    {
        Task create(EmailEntity emailEntity);

        Task commitAsync();

        Task<EmailEntity?> getEmailByUserId(string userId);
        Task<EmailEntity?> getByUser(string email);
    }
}
