using lizi_mail_api.Response;
using MimeKit;

namespace lizi_mail_api.Services.MimeMassage
{
    public class MimeMessageService
    {
        public async Task<Result<bool>> _invoke(IConfiguration config, string to, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(config["Email:From"]));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart("html") { Text = body };
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(
                    config["Email:SmtpHost"],
                    int.Parse(config["Email:SmtpPort"]),
                    MailKit.Security.SecureSocketOptions.StartTls
                );
                smtp.Authenticate(
                    config["Email:Username"],
                    config["Email:Password"]
                );
                smtp.Send(email);
                smtp.Disconnect(true);
                return Result<bool>.success(true, "Email send with success");
            }
            catch (Exception ex)
            {
               return Result<bool>.error(false, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
