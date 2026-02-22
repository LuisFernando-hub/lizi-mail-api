using lizi_mail_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace lizi_mail_api.Infra.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task commitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task create(UserEntity user)
        {
            await _context.User.AddAsync(user);
        }

        public async Task<UserEntity?> getByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<UserEntity?> getById(string id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.id.ToString() == id);
        }
    }
}
