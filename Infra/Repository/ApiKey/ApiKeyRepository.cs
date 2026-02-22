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

        public async Task<ApiKeyEntity?> getByUserId(string userId)
        {
            return await _context.ApiKey.FirstOrDefaultAsync(a => a.user_id == Guid.Parse(userId));
        }
    }
}
