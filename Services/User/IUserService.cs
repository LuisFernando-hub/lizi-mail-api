using lizi_mail_api.DTOs;
using lizi_mail_api.Entities;
using lizi_mail_api.Request.Auth;
using lizi_mail_api.Response;

namespace lizi_mail_api.Services.User
{
    public interface IUserService
    {
        Task<Result<bool>> create(CreateUserRequest request);
        Task<Result<AuthLoginDTO>> login(LoginRequest request);
        Task<Result<bool>> getByEmail(string email);
    }
}
