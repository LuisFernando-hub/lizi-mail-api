using lizi_mail_api.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace lizi_mail_api.Infra.Repository.User
{
    public interface IUserRepository
    {
        Task create(UserEntity user);

        Task commitAsync();

        Task<UserEntity?> getByEmail(string email);
        Task<UserEntity?> getById(string id);
    }
}
