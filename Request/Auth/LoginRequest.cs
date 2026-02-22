using System.ComponentModel.DataAnnotations;

namespace lizi_mail_api.Request.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }
    }
}
