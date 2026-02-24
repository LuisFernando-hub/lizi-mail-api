namespace lizi_mail_api.HttpContext
{
    public interface IUserContext
    {
        Guid? UserId { get; }
        string? Email { get; }
    }
}
