using lizi_mail_api.Entities;
using lizi_mail_api.Request.Auth;
using lizi_mail_api.Response;

namespace lizi_mail_api.Services.ApiKey
{
    public interface IApiKeyService
    {
        Task<Result<bool>> createForUser(string userId);
        Task<Result<ApiKeyEntity>> getActiveByUserId(string userId);
        Task<Result<ApiKeyEntity>> getByUserEmail(string email);
        Task<Result<bool>> validateAsync(string apiKey);
    }
}
