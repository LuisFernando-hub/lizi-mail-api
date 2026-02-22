using lizi_mail_api.Response;
using MailKit.Security;
using MimeKit;
using System.Net.Mail;

namespace lizi_mail_api.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Result<bool>> SendEmailAsync(string to, string subject, string body)
        {
            

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config["Email:From"]));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                email.Body = new TextPart("html") { Text = body };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                await smtp.ConnectAsync(
                    _config["Email:SmtpHost"],
                    int.Parse(_config["Email:SmtpPort"]),
                    MailKit.Security.SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    _config["Email:Username"],
                    _config["Email:Password"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return Result<bool>.success(true);
            }
            catch (Exception ex)
            {
               return Result<bool>.error(false, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
