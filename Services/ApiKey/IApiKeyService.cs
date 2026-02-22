using lizi_mail_api.Request.Auth;
using lizi_mail_api.Response;

namespace lizi_mail_api.Services.ApiKey
{
    public interface IApiKeyService
    {
        Task<Result<bool>> createForUser(string userId);
    }
}
