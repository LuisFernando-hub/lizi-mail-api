using lizi_mail_api.Entities;

namespace lizi_mail_api.Infra.Repository.ApiKey
{
    public interface IApiKeyRepository
    {
        Task create(ApiKeyEntity apiKeyEntity);

        Task commitAsync();

        Task<ApiKeyEntity?> getByUserId(string userId);
    }
}
