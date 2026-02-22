using lizi_mail_api.Entities;
using lizi_mail_api.Infra;
using lizi_mail_api.Infra.Repository.ApiKey;
using lizi_mail_api.Response;

namespace lizi_mail_api.Services.ApiKey
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<Result<bool>> createForUser(string userId)
        {
            if (userId == null)
            {
                return Result<bool>.error(false, "The User ID is required");
            }

            var existing = await _apiKeyRepository.getByUserId(userId);
            if (existing != null)
            {
                return Result<bool>.error(false, "An API key already exists for this user");
            }

            try
            {
                var apiKey = new ApiKeyEntity(userId);
                await _apiKeyRepository.create(apiKey);
                await _apiKeyRepository.commitAsync();
            }
            catch (Exception ex) 
            {
                return Result<bool>.error(false, $"An error occurred while creating the API key: {ex.Message}");
            }


            return Result<bool>.success(true);
        }
    }
}
