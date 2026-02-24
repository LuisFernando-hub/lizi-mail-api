namespace lizi_mail_api.DTOs
{
    public record AuthLoginDTO(string token, string name, string email, string xApiKey);
}
