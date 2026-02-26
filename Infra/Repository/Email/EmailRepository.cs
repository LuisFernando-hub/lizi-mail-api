using lizi_mail_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace lizi_mail_api.Infra.Repository.Email
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task commitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task create(EmailEntity emailEntity)
        {
            await _context.AddAsync(emailEntity);
        }

        public Task<EmailEntity?> getByUser(string email)
        {
            throw new NotImplementedException();
        }

        public Task<EmailEntity?> getEmailByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
