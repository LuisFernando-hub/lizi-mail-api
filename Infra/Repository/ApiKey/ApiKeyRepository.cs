using lizi_mail_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace lizi_mail_api.Infra.Repository.ApiKey
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly ApplicationDbContext _context;

        public ApiKeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task create(ApiKeyEntity apiKeyEntity)
        {
            await _context.AddAsync(apiKeyEntity);
        }

        public async Task commitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ApiKeyEntity?> getActiveByUserId(string userId)
        {
            if (!Guid.TryParse(userId, out var userGuid))
                return null;

            return await _context.ApiKey
                .Include(a => a.user)
                .Where(a => a.user_id == userGuid && a.is_active)
                .Where(a => a.user != null && a.user.is_active) // evita null
                .FirstOrDefaultAsync();
        }

        public async Task<ApiKeyEntity?> getByUserEmail(string email)
        {
            return await _context.ApiKey
                .Include(a => a.user)
                .Where(a => a.user != null && a.user.email == email && a.user.is_active)
                .Where(a => a.is_active)
                .FirstOrDefaultAsync();
        }

        public async Task<ApiKeyEntity?> getByKeyAsync(string apiKey)
        {
            return await _context.ApiKey
                .FirstOrDefaultAsync(a => a.key_hash == apiKey && a.is_active);
        }

    }
}
