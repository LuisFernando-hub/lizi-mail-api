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

            var existing = await _apiKeyRepository.getActiveByUserId(userId);
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

        public async Task<Result<ApiKeyEntity>> getActiveByUserId(string userId)
        {
            if (userId == null)
            {
                return Result<ApiKeyEntity>.error(false, "The User ID is required");
            }

            var existing = await _apiKeyRepository.getActiveByUserId(userId);
            if (existing == null)
            {
                return Result<ApiKeyEntity>.error(false, "The user does not have the API key configured.");
            }

            return Result<ApiKeyEntity>.success(existing);
        }

        public async Task<Result<ApiKeyEntity>> getByUserEmail(string email)
        {
            if (email == null)
            {
                return Result<ApiKeyEntity>.error(false, "The Email is required");
            }

            var existing = await _apiKeyRepository.getByUserEmail(email);
            if (existing == null)
            {
                return Result<ApiKeyEntity>.error(false, "The email does not have the API key configured.");
            }

            return Result<ApiKeyEntity>.success(existing);
        }

        public async Task<Result<bool>> validateAsync(string apiKey)
        {
            var entity = await _apiKeyRepository.getByKeyAsync(apiKey);
            
            if (entity == null)
            {
                return Result<bool>.error(false, "Invalid API key");
            }

            return Result<bool>.success(true);
        }
    }
}
